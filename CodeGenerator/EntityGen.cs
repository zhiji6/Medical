using SY.Com.Medical.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    /// <summary>
    /// Entity生成器
    /// </summary>
    public class EntityGen
    {
        public CodeGenRepository db { get; set; }
        public string EntityName { get; private set; }
        public string TableName { get;private set; }
        public string TableKey { get;private set; }
        public string ClassName { get; private set; }
        private string TextUsing { get; set; }
        private string TextNamespace { get; set; }
        private string TextClass { get; set; }
        private FileGen fg { get; set; }
        private string NavigateEntity { get; set; }
        private string NavigateKey { get; set; }


        public EntityGen(GenParam param)
        {
            if (param.dbname == dbName.Platform)
            {
                db = new CodeGenRepository("server=.\\MS2008;database=Medical_Platform;uid=sa;pwd=sql2021");
            }
            else if (param.dbname == dbName.Clinic){
                db = new CodeGenRepository("server=.\\MS2008;database=Medical_Clinic;uid=sa;pwd=sql2021");
            }else
            {
                throw new Exception("数据库名称不对");
            }
            TableName = param.TableName;
            TableKey = param.TableKey;
            ClassName = TableName.Substring(TableName.Length - 1, 1) == "s"
                ? TableName.Substring(0, TableName.Length - 1) : TableName;//Table是复数命名,class单数形式  
            fg = new FileGen();
            NavigateEntity = param.Navigate;
            NavigateKey = param.Navikey;
            if(!string.IsNullOrEmpty(NavigateEntity) && string.IsNullOrEmpty(NavigateKey))
            {
                throw new Exception("导航属性必须要具备匹配的Id名称");
            }
            if(!string.IsNullOrEmpty(param.Navigate))
            {
                NavigateEntity += "Entity";
            }            
        }

        public string Gen()
        {
            string result = "";

            return result;
        }

        /// <summary>
        /// Using
        /// </summary>
        private void GenUsing()
        {
            TextUsing = @"using SY.Com.Medical.Attribute;
using SY.Com.Medical.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
";
        }

        /// <summary>
        /// NameSpace
        /// </summary>
        private void GenNameSpace()
        {
            TextNamespace =  @"namespace SY.Com.Medical.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    [DB_Table("+ "\"" + TableName + "\"" +@")]
    [DB_Key("+ "\"" + TableKey + "\"" + @")]
    #Class#
} ";
        }

        private void GenClass()
        {                   
            StringBuilder txt = new StringBuilder();
            txt.Append($"public class {ClassName}Entity : BaseEntity ");
            txt.Append("\r\n\t{ ");
            // 数据库读出表信息
            var columns = db.getString(TableName);
            //每一列生成一个Property
            foreach(var column in columns)
            {
                //BaseEntity中的属性就不需要重复了
                if (typeof(BaseEntity).GetProperty(column.ColumnName) != null) continue;                
                txt.Append("\r\n\t\t///<summary> ");
                txt.Append($"\r\n\t\t///{column.ColName ??  ""}");
                txt.Append("\r\n\t\t///</summary> ");
                if(column.ColumnName.ToLower() == TableKey.ToLower())
                {
                    txt.Append("\r\n\t\t[DB_Key(\""+ TableKey + "\")]");
                }
                txt.Append($"\r\n\t\tpublic {column.ColumnType}{column.NullableSign} {column.ColumnName} {{get;set;}} ");
            }
            //导航属性
            if(!string.IsNullOrEmpty(NavigateEntity))
            {
                txt.Append("\r\n\t\t///<summary> ");
                txt.Append($"\r\n\t\t///导航属性");
                txt.Append("\r\n\t\t///</summary> ");
                txt.Append("\r\n\t\t[DB_NotColum]");
                txt.Append("\r\n\t\t[DB_Navigate(\"" + NavigateEntity.Replace("Entity","s") + "\",\"" + NavigateKey + "\")]");
                txt.Append($"\r\n\t\tpublic {NavigateEntity} {NavigateEntity.Replace("Entity", "")} {{get;set;}} ");                
            }
            txt.Append("\r\n\t}");
            TextClass = txt.ToString();
        }

        /// <summary>
        /// 获取文本
        /// </summary>
        /// <returns></returns>
        public string getCode()
        {
            GenUsing();
            GenNameSpace();
            GenClass();
            return TextUsing + TextNamespace.Replace("#Class#", TextClass);
        }

        public void GenFile()
        {
            fg.Gen("GenEntity",$"{ClassName}Entity.cs",getCode());
        }

    }
}
