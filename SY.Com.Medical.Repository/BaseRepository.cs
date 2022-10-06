using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Infrastructure;

namespace SY.Com.Medical.Repository
{
    /// <summary>
    /// 数据层基类
    /// </summary>
    public class BaseRepository<T> where T : BaseEntity
    {
        public static object obj = new object();
        protected IDbConnection _db;
        protected IDbConnection _dbid;
        private string strconnection;


        /// <summary>
        /// 根据业务层反射类获取数据库连接
        /// 反射不到就直接抛出异常
        /// </summary>
        protected BaseRepository()
        {
            Type t = this.GetType();
            if (t.Namespace == "SY.Com.Medical.Repository.Clinic")
            {
                strconnection = ReadConfig.GetConfigSection("Medical_Clinic");
            }
            else if (t.Namespace == "SY.Com.Medical.Repository.Platform")
            {
                strconnection = ReadConfig.GetConfigSection("Medical_Platform"); 
            }
            else
            {
                throw new DllNotFoundException("无法找到数据库");
            }
            _db = new SqlConnection(strconnection);
            _dbid = new SqlConnection(ReadConfig.GetConfigSection("Medical_Platform"));
        }


        //制定数据库
        public BaseRepository(string strconnection)
        {
            _db = new SqlConnection(strconnection);
            _dbid = new SqlConnection(ReadConfig.GetConfigSection("Medical_Platform"));

        }

        /// <summary>
        /// 单表单记录查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            Type t = typeof(T);
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t).ToString();//获取表名
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t).ToString();
            string sql = $" Select * From {tableName} Where {tablekey} = @Id And IsDelete = 1 ";
            return _db.QueryFirstOrDefault<T>(sql, new { Id = id });
        }

        /// <summary>
        /// 单表单记录查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual T GetDelete(int id)
        {
            Type t = typeof(T);
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t).ToString();//获取表名
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t).ToString();
            string sql = $" Select * From {tableName} Where {tablekey} = @Id  ";
            return _db.QueryFirstOrDefault<T>(sql, new { Id = id });
        }

        /// <summary>
        /// 单表单记录查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual T Get(int id,string tableName,string tablekey)
        {
            Type t = typeof(T);            
            if (tableName == "")
            {
                tableName = t.Name.Replace("Entity", "");
            }
            string sql = $" Select * From {tableName} Where {tablekey} = @Id And IsDelete = 1 ";
            return _db.QueryFirstOrDefault<T>(sql, new {  Id = id });
        }

        /// <summary>
        /// 获取表的单条记录及导航记录(只有单个导航属性)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get<D>(int id,Func<T,D,T> func,string OnSplit = "")
        {
            Type t = typeof(T);
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t).ToString();//获取表名
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t).ToString();
            Type d = typeof(D);
            string tableName2 = ReadAttribute<DB_TableAttribute>.getKey(d).ToString();//获取表名
            string tablekey2 = ReadAttribute<DB_KeyAttribute>.getKey(d).ToString();
            string sql = @$" Select {tableName}.*,{tableName2}.* 
                             From  {tableName}
                             Inner Join {tableName2} On {tableName}.{OnSplit} = {tableName2}.{OnSplit}
                             Where {tableName}.{tablekey} = @id And {tableName}.IsDelete = 1 ";
            if (OnSplit == "")
            {
                OnSplit = tablekey2;
            }                
            return _db.Query<T, D, T>(sql.ToString(), func, new { Id = id }, splitOn: OnSplit).Distinct().FirstOrDefault();
        }

        /// <summary>
        /// 新增单条记录,并返回新增记录唯一id
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual int Create(T t)
        {
            int ireturn = 0;
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t.GetType()).ToString();//获取表名
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t.GetType()).ToString();
            List<string> columns = new List<string>();
            List<string> param = new List<string>();
            if (tableName == "")
            {
                tableName = t.GetType().Name.Replace("Entity", "");//如果反射没获取到用实体名称去掉Entity试试
            }            
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach(PropertyInfo prop in properties)
            {
                //主键可能字段属性名称和Attribute不一致
                if (prop.IsDefined(typeof(DB_KeyAttribute), false))
                {
                    ireturn = getID(tableName);
                    columns.Add(tablekey);
                    param.Add($"@{tablekey}");
                    prop.SetValue(t, ireturn);
                }
                else if (prop.IsDefined(typeof(DB_NotColumAttribute), false)) {

                } else{
                    if ( prop.IsDefined(typeof(DB_DefaultAttribute), false))
                    {
                        int outint;
                        if (prop.GetValue(t) == null ||  prop.Name == "IsEnable" || prop.Name == "IsDelete" || (int.TryParse(prop.GetValue(t).ToString(), out outint) && (int)prop.GetValue(t) == 0))
                        {
                            var attr = (DB_DefaultAttribute)prop.GetCustomAttribute(typeof(DB_DefaultAttribute));
                            var defaultvalue = attr.getDefault();
                            prop.SetValue(t, defaultvalue);
                        }
                    }
                    columns.Add(prop.Name);
                    param.Add($"@{prop.Name}");
                }
            }
            string strcolum = string.Join(',', columns);
            string strparam = string.Join(',', param);
            string sql = $" Insert Into {tableName}({strcolum}) Values({strparam}) ";
            _db.Execute(sql, t);
            return ireturn;
        }

        /// <summary>
        /// 修改单条记录
        /// 主键为条件
        /// 值类型默认值,则不更新.引用类型空不更新
        /// </summary>
        /// <param name="t"></param>
        public virtual void Update(T t)
        {            
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t.GetType()).ToString();//获取表名
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t.GetType()).ToString();
            string entityPrimkey = "";
            string entityPrimvalue = "";
            List<string> updateColumns = new List<string>();
            if (tableName == "")
            {
                tableName = t.GetType().Name.Replace("Entity", "");//如果反射没获取到用实体名称去掉Entity试试
            }
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                if (prop.IsDefined(typeof(DB_KeyAttribute), false))
                {
                    entityPrimkey = prop.Name;
                    entityPrimvalue = prop.GetValue(t).ToString();
                }
                else if (prop.IsDefined(typeof(DB_NotColumAttribute), false) || prop.GetValue(t) == null)
                {

                }
                else
                {    
                    if (prop.IsDefined(typeof(DB_DefaultAttribute), false))
                    {
                        if(prop.Name == "IsEnable" || prop.Name == "IsDelete")
                        {
                            if((int)prop.GetValue(t) == 0)
                            {
                                var attr = (DB_DefaultAttribute)prop.GetCustomAttribute(typeof(DB_DefaultAttribute));
                                var defaultvalue = attr.getDefault();
                                prop.SetValue(t, defaultvalue);
                            }
                        }
                        else
                        {
                            int outint;
                            if (prop.GetValue(t) == null || (int.TryParse(prop.GetValue(t).ToString(), out outint) && (int)prop.GetValue(t) == 0))
                            {
                                var attr = (DB_DefaultAttribute)prop.GetCustomAttribute(typeof(DB_DefaultAttribute));
                                var defaultvalue = attr.getDefault();
                                prop.SetValue(t, defaultvalue);
                            }
                        }
                    }
                    updateColumns.Add($"{prop.Name}=@{prop.Name}");                                        
                }
                //else if (prop.IsDefined(typeof(DB_NotColumAttribute), false))
                //{

                //}
                //else if (!DefaultValue.IsDefaultValue(prop.PropertyType, prop.GetValue(t)))
                //{
                //    updateColumns.Add($"{prop.Name}=@{prop.Name}");
                //}
            }
            string strcolum = string.Join(',', updateColumns);
            string sql = $" Update {tableName} Set {strcolum} Where {tablekey}=@{entityPrimkey} ";
            _db.Execute(sql, t);
        }

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <param name="t"></param>
        public void Delete(T t)
        {
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t.GetType()).ToString();
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t.GetType()).ToString();
            if (tableName == "")
            {
                tableName = t.GetType().Name.Replace("Entity", "");
            }
            string sql = $" Update {tableName} Set IsDelete = { ((int)Enum.Delete.删除) } Where {tablekey} = @Id ";
            _db.Execute(sql, new { Id = t.GetType().GetProperty(tablekey).GetValue(t).ToString() });
        }

        /// <summary>
        /// 单表多记录查询
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Gets(T t)
        {
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t.GetType()).ToString();//获取表名
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t.GetType()).ToString();
            StringBuilder whereColumns = new StringBuilder();
            if (tableName == "")
            {
                tableName = t.GetType().Name.Replace("Entity", "");//如果反射没获取到用实体名称去掉Entity试试
            }
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                //排除主键,排除默认值或空值的键,处理各种Attribute
                if (prop.IsDefined(typeof(DB_KeyAttribute), false))
                {

                }
                else if (prop.IsDefined(typeof(DB_NotColumAttribute), false))
                {

                }
                else if (!DefaultValue.IsDefaultValue(prop.PropertyType, prop.GetValue(t)))
                {
                    if(prop.IsDefined(typeof(DBBaseAttribute), false))
                    {
                        foreach(var attr in prop.GetCustomAttributes())
                        {
                            if(attr is DB_LimitAttribute)
                            {
                                var temp = ReadAttribute<DB_LimitAttribute>.getWhere(prop,t, (DB_LimitAttribute)attr);
                                whereColumns.Append(" And " + temp.Tostring());
                            }else if(attr is DB_LikeAttribute)
                            {
                                var temp = ReadAttribute<DB_LikeAttribute>.getWhere(prop,t, (DB_LikeAttribute)attr);
                                whereColumns.Append(" And " + temp.Tostring());
                            }else if(attr is DB_CheckAttribute)
                            {
                                var temp = ReadAttribute<DB_CheckAttribute>.getWhere(prop, t, (DB_CheckAttribute)attr);
                                whereColumns.Append(" And " + temp.Tostring());
                            }
                        }
                    }
                    else
                    {
                        whereColumns.Append($" And {prop.Name}=@{prop.Name}");
                    }
                }
            }
            string sql = $" Select * From {tableName} Where 1=1 {whereColumns} And IsDelete = 1 ";
            return _db.Query<T>(sql, t);
        }

        /// <summary>
        /// 单表多记录查询,单表包含导航属性
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public IEnumerable<T> Gets<D>(T t,Func<T, D, T> func, string OnSplit = "")
        {
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t.GetType()).ToString();//获取表名
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t.GetType()).ToString();
            StringBuilder whereColumns = new StringBuilder();
            if (tableName == "")
            {
                tableName = t.GetType().Name.Replace("Entity", "");//如果反射没获取到用实体名称去掉Entity试试
            }
            //导航表
            Type d = typeof(D);
            string tableName2 = ReadAttribute<DB_TableAttribute>.getKey(d).ToString();//获取表名
            string tablekey2 = ReadAttribute<DB_KeyAttribute>.getKey(d).ToString();
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                //排除主键,排除默认值或空值的键,处理各种Attribute
                if (prop.IsDefined(typeof(DB_KeyAttribute), false))
                {

                }
                else if (prop.IsDefined(typeof(DB_NotColumAttribute), false))
                {

                }
                else if (!DefaultValue.IsDefaultValue(prop.PropertyType, prop.GetValue(t)))
                {
                    if (prop.IsDefined(typeof(DBBaseAttribute), false))
                    {
                        foreach (var attr in prop.GetCustomAttributes())
                        {
                            if (attr is DB_LimitAttribute)
                            {
                                var temp = ReadAttribute<DB_LimitAttribute>.getWhere(prop, t, (DB_LimitAttribute)attr);
                                whereColumns.Append( $" And {tableName}." + temp.Tostring());
                            }
                            else if (attr is DB_LikeAttribute)
                            {
                                var temp = ReadAttribute<DB_LikeAttribute>.getWhere(prop, t, (DB_LikeAttribute)attr);
                                whereColumns.Append($" And {tableName}." + temp.Tostring());
                            }
                            else if (attr is DB_CheckAttribute)
                            {
                                var temp = ReadAttribute<DB_CheckAttribute>.getWhere(prop, t, (DB_CheckAttribute)attr);
                                whereColumns.Append(" And " + temp.Tostring());
                            }
                        }
                    }
                    else
                    {
                        whereColumns.Append($" And {tableName}.{prop.Name}=@{prop.Name}");
                    }
                }
            }
            string sql = @$"                            
                            Select {tableName}.*,{tableName2}.* 
                             From  {tableName}                            
                             Inner Join {tableName2} On {tableName}.{OnSplit} = {tableName2}.{OnSplit}
                             Where 1=1 And IsDelete = 1 {whereColumns} 
                              ";
            return _db.Query<T,D,T>(sql,func, t, splitOn : OnSplit).Distinct();
        }

        /// <summary>
        /// 单表多记录查询-分页
        /// 多条件Search分页列表查询
        /// </summary>
        /// <returns></returns>
        public Tuple<IEnumerable<T>,int> GetsPage(T t,int pageSize,int pageIndex)
        {
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t.GetType()).ToString();//获取表名
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t.GetType()).ToString();
            StringBuilder whereColumns = new StringBuilder();
            if (tableName == "")
            {
                tableName = t.GetType().Name.Replace("Entity", "");//如果反射没获取到用实体名称去掉Entity试试
            }
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                //排除主键,排除默认值或空值的键,处理各种Attribute
                if (prop.IsDefined(typeof(DB_KeyAttribute), false))
                {

                }
                else if (prop.IsDefined(typeof(DB_NotColumAttribute), false))
                {

                }
                else if (!DefaultValue.IsDefaultValue(prop.PropertyType, prop.GetValue(t)))
                {
                    if (prop.IsDefined(typeof(DBBaseAttribute), false))
                    {
                        foreach (var attr in prop.GetCustomAttributes())
                        {
                            if (attr is DB_LimitAttribute)
                            {
                                var temp = ReadAttribute<DB_LimitAttribute>.getWhere(prop,t, (DB_LimitAttribute)attr);
                                whereColumns.Append($" And {tableName}." + temp.Tostring());                                
                            }
                            else if (attr is DB_LikeAttribute)
                            {
                                var temp = ReadAttribute<DB_LikeAttribute>.getWhere(prop,t, (DB_LikeAttribute)attr);
                                whereColumns.Append($" And {tableName}." + temp.Tostring());
                            }
                            else if (attr is DB_CheckAttribute)
                            {
                                var temp = ReadAttribute<DB_CheckAttribute>.getWhere(prop, t, (DB_CheckAttribute)attr);
                                whereColumns.Append(" And " + temp.Tostring());
                            }
                        }
                    }
                    else
                    {
                        whereColumns.Append($" And {prop.Name}=@{prop.Name}");
                    }
                }
            }
            string sql = @$" 
            Select  count(1) as nums From {tableName} Where 1=1 And IsDelete = 1 {whereColumns}
            Select * From
            (
                Select  ROW_NUMBER() over(order by {tablekey} desc) as row_id,* From {tableName} 
                Where 1=1 And IsDelete = 1 {whereColumns} 
            )t
            Where t.row_id between {(pageIndex-1) * pageSize + 1} and { pageIndex * pageSize }
            ";
            var multi  = _db.QueryMultiple(sql, t);
            int count = multi.Read<int>().First();
            IEnumerable<T> datas = multi.Read<T>();            
            Tuple<IEnumerable<T>, int> result = new Tuple<IEnumerable<T>, int>(datas,count);
            return result;
        }

        /// <summary>
        /// 单表多记录查询-分页,带导航属性(一个)
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <param name="t"></param>
        /// <param name="func"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="OnSplit"></param>
        /// <returns></returns>
        public Tuple<IEnumerable<T>, int> GetsPage<D>(T t,Func<T,D,T> func, int pageSize, int pageIndex, string OnSplit = "")
        {
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t.GetType()).ToString();//获取表名
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t.GetType()).ToString();
            //导航表
            var d = typeof(D);
            string tableName2 = ReadAttribute<DB_TableAttribute>.getKey(d).ToString();//获取表名
            string tablekey2 = ReadAttribute<DB_KeyAttribute>.getKey(d).ToString();
            StringBuilder whereColumns = new StringBuilder();
            if (tableName == "")
            {
                tableName = t.GetType().Name.Replace("Entity", "");//如果反射没获取到用实体名称去掉Entity试试
            }
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                //排除主键,排除默认值或空值的键,处理各种Attribute
                if (prop.IsDefined(typeof(DB_KeyAttribute), false))
                {

                }
                else if (prop.IsDefined(typeof(DB_NotColumAttribute), false))
                {

                }
                else if (!DefaultValue.IsDefaultValue(prop.PropertyType, prop.GetValue(t)))
                {
                    if (prop.IsDefined(typeof(DBBaseAttribute), false))
                    {
                        foreach (var attr in prop.GetCustomAttributes())
                        {
                            if (attr is DB_LimitAttribute)
                            {
                                var temp = ReadAttribute<DB_LimitAttribute>.getWhere(prop, t, (DB_LimitAttribute)attr);
                                whereColumns.Append($" And {tableName}." + temp.Tostring());
                            }
                            else if (attr is DB_LikeAttribute)
                            {
                                var temp = ReadAttribute<DB_LikeAttribute>.getWhere(prop, t, (DB_LikeAttribute)attr);
                                whereColumns.Append($" And {tableName}." + temp.Tostring());
                            }
                            else if (attr is DB_CheckAttribute)
                            {
                                var temp = ReadAttribute<DB_CheckAttribute>.getWhere(prop, t, (DB_CheckAttribute)attr);
                                whereColumns.Append(" And " + temp.Tostring());
                            }
                        }
                    }
                    else
                    {
                        whereColumns.Append($" And {tableName}.{prop.Name}=@{prop.Name}");
                    }
                }
            }
            string sqlcount = @$"        
                Select  count(1) as nums  
                From {tableName}
                Where 1=1 And IsDelete = 1 {whereColumns};";
            string sql= @$"            
            Select {tableName}.*,{tableName2}.* 
            From
            (
                Select  ROW_NUMBER() over(order by {tablekey} desc) as row_id,{tableName}.*
                From {tableName}                 
                Where 1=1 And IsDelete = 1 {whereColumns} 
            ) as {tableName}
            Inner Join {tableName2} On  {tableName}.{OnSplit} = {tableName2}.{OnSplit}
            Where {tableName}.row_id between {(pageIndex - 1) * pageSize + 1} and { pageIndex * pageSize } ";
            int count = _db.Query<int>(sqlcount, t).FirstOrDefault();
            IEnumerable<T> datas = _db.Query<T,D,T>(sql,func,t,splitOn: OnSplit).Distinct();
            Tuple<IEnumerable<T>, int> result = new Tuple<IEnumerable<T>, int>(datas, count);            
            return result;
        }





        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="collection"></param>
        public void Update(IEnumerable<T> collection)
        {
            var t = collection.FirstOrDefault();
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t.GetType()).ToString();//获取表名
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t.GetType()).ToString();
            string entityPrimkey = "";
            string entityPrimvalue = "";
            List<string> updateColumns = new List<string>();
            if (tableName == "")
            {
                tableName = t.GetType().Name.Replace("Entity", "");//如果反射没获取到用实体名称去掉Entity试试
            }
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                if (prop.IsDefined(typeof(DB_KeyAttribute), false))
                {
                    entityPrimkey = prop.Name;
                    entityPrimvalue = prop.GetValue(t).ToString();
                }
                else if (prop.IsDefined(typeof(DB_NotColumAttribute), false))
                {

                }
                else if (!DefaultValue.IsDefaultValue(prop.PropertyType, prop.GetValue(t)))
                {
                    updateColumns.Add($"{prop.Name}=@{prop.Name}");
                }
            }
            string strcolum = string.Join(',', updateColumns);
            string sql = $" Update {tableName} Set {strcolum} Where {tablekey}=@{entityPrimkey} ";
            _db.Execute(sql, collection);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="collection"></param>
        public void Insert(IEnumerable<T> collection)
        {
            var t = collection.FirstOrDefault();
            
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t.GetType()).ToString();//获取表名
            string tablekey = ReadAttribute<DB_KeyAttribute>.getKey(t.GetType()).ToString();
            List<string> columns = new List<string>();
            List<string> param = new List<string>();
            if (tableName == "")
            {
                tableName = t.GetType().Name.Replace("Entity", "");//如果反射没获取到用实体名称去掉Entity试试
            }
            int maxid = getID(tableName, collection.Count());
            PropertyInfo[] properties = t.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                //主键可能字段属性名称和Attribute不一致
                if (prop.IsDefined(typeof(DB_KeyAttribute), false))
                {
                    columns.Add(tablekey);
                    param.Add($"@{tablekey}");
                    foreach(var item in collection)
                    {
                        prop.SetValue(item, maxid--);
                    }
                }
                else if (prop.IsDefined(typeof(DB_NotColumAttribute), false))
                {
                    //非列直接跳过
                }
                else
                {
                    columns.Add(prop.Name);
                    param.Add($"@{prop.Name}");
                }
            }
            string strcolum = string.Join(',', columns);
            string strparam = string.Join(',', param);
            string sql = $" Insert Into {tableName}({strcolum}) Values({strparam}) ";
            _db.Execute(sql, collection);
        }

        /// <summary>
        /// 获取全局唯一ID,手动输入表名
        /// </summary>
        /// <param name="name"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public int getID(string name,int step = 1)
        {
            string sql = @" 
                            if exists(Select ID From IDGlobal Where Name = @Name)
                            begin
                                 Update IDGlobal Set ID = ID + @step Where Name = @Name ;
                                Select ID From IDGlobal Where Name = @Name;
                            end else begin
                                Insert Into IDGlobal(ID,Name)
                                Values(@step,@Name)
                                Select ID From IDGlobal Where Name = @Name;
                            end 
                            ";
            lock (obj)
            {
                return _dbid.QueryFirst<int>(sql, new { Name = name, step = step });
            }
        }

        /// <summary>
        /// 获取全局唯一ID,自动解析表名
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public int getID(int step = 1)
        {
            Type t = typeof(T);
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t).ToString();//获取表名
            if(string.IsNullOrEmpty(tableName))
            {
                throw new Exception("获取唯一ID失败,未解析到表名");
            }
            string sql = @" 
                            Update IDGlobal Set ID = ID + @step Where Name = @Name ;
                            Select ID From IDGlobal Where Name = @Name; ";
            lock (obj)
            {
                return _dbid.QueryFirst<int>(sql, new { Name = tableName, step = step });
            }
        }

        /// <summary>
        /// 获取全局租户编号
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="name"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public long getBH(int tenantId,string tableName, int step = 1)
        {
            string sql = @" 
                        if exists( select BH From BHGlobal Where TenantId = @TenantId And Name=@Name )
                        Begin
	                        if exists( select BH From BHGlobal Where TenantId = @TenantId And Name=@Name And BHdate = @day )
	                        Begin
		                        Update BHGlobal Set BH = BH + @step Where TenantId=@TenantId And Name = @Name 
	                        End Else Begin
		                        Update BHGlobal Set BH = 0 + @step,BHdate=getdate() Where TenantId=@TenantId And Name = @Name
	                        End
                        End Else Begin

	                        Insert Into BHGlobal(Name,TenantId)
	                        Values(@Name,@TenantId)
                        End
                        Select BH From BHGlobal Where TenantId=@TenantId And Name = @Name;  ";
            lock (obj)
            {
                return _dbid.QueryFirst<long>(sql, new { TenantId= tenantId, Name = tableName, step = step,day = DateTime.Now.ToShortDateString() });
            }
        }

        /// <summary>
        /// 获取全局租户编号,自动解析表名
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public long getBH(int tenantId, int step = 1)
        {
            Type t = typeof(T);
            string tableName = ReadAttribute<DB_TableAttribute>.getKey(t).ToString();//获取表名
            string sql = @" 
                        if exists( select BH From BHGlobal Where TenantId = @TenantId And Name=@Name )
                        Begin
	                        if exists( select BH From BHGlobal Where TenantId = @TenantId And Name=@Name And BHdate = @day )
	                        Begin
		                        Update BHGlobal Set BH = BH + @step Where TenantId=@TenantId And Name = @Name 
	                        End Else Begin
		                        Update BHGlobal Set BH = 0 + @step,BHdate=getdate() Where TenantId=@TenantId And Name = @Name
	                        End
                        End Else Begin

	                        Insert Into BHGlobal(Name,TenantId)
	                        Values(@Name,@TenantId)
                        End
                        Select BH From BHGlobal Where TenantId=@TenantId And Name = @Name;  ";
            lock (obj)
            {
                return _dbid.QueryFirst<long>(sql, new { TenantId = tenantId, Name = tableName, step = step, day = DateTime.Now.ToShortDateString() });
            }
        }

    }
}
