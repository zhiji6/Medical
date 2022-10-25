using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class ModelGen
    {
        public CodeGenRepository db { get; set; }
        public string EntityName { get; private set; }
        public string TableName { get; private set; }
        public string TableKey { get; private set; }
        public string ClassName { get; private set; }
        private string TextUsing { get; set; }
        private string TextNamespace { get; set; }
        private string TextClass { get; set; }
        private FileGen fg { get; set; }
        private string NavigateEntity { get; set; }
        private string NavigateKey { get; set; }


        public ModelGen(GenParam param)
        {
            if (param.dbname == dbName.Platform)
            {
                db = new CodeGenRepository("server=.\\MS2008;database=Medical_Platform;uid=sa;pwd=sql2021");
            }
            else if (param.dbname == dbName.Clinic)
            {
                db = new CodeGenRepository("server=.\\MS2008;database=Medical_Clinic;uid=sa;pwd=sql2021");
            }
            else
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
            if (!string.IsNullOrEmpty(NavigateEntity) && string.IsNullOrEmpty(NavigateKey))
            {
                throw new Exception("导航属性必须要具备匹配的Id名称");
            }
            if (!string.IsNullOrEmpty(param.Navigate))
            {
                NavigateEntity += "Model";
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
            TextUsing = @"using SY.Com.Medical.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            TextNamespace = @"namespace SY.Com.Medical.Model
{
    #Class#
} ";
        }

        private void GenClass(string className)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append("\r\n///<summary>");
            txt.Append($"\r\n/// {ClassName}模型");
            txt.Append("\r\n/// </summary>");
            txt.Append($"\r\n\tpublic class {ClassName}{className} : { (className == "Request" ? "PageModel" : "BaseModel")   } ");
            txt.Append("\r\n\t{ ");
            // 数据库读出表信息
            var columns = db.getString(TableName);
            //每一列生成一个Property
            foreach (var column in columns)
            {
                //BaseModel中的属性就不需要重复了
                if (typeof(BaseModel).GetProperty(column.ColumnName) != null) continue;           
                txt.Append("\r\n\t\t///<summary> ");
                txt.Append($"\r\n\t\t///{column.ColName ?? ""}");
                txt.Append("\r\n\t\t///</summary> ");
                txt.Append($"\r\n\t\tpublic {column.ColumnType}{column.NullableSign} {column.ColumnName} {{get;set;}} ");
            }
            //导航属性
            if (!string.IsNullOrEmpty(NavigateEntity))
            {
                txt.Append("\r\n\t\t///<summary> ");
                txt.Append($"\r\n\t\t///导航属性");
                txt.Append("\r\n\t\t///</summary> ");
                txt.Append($"\r\n\t\tpublic {NavigateEntity} {NavigateEntity.Replace("Model", "")} {{get;set;}} ");
            }
            txt.Append("\r\n\t}\r\n\t");
            TextClass += txt.ToString();
        }

        /// <summary>
        /// 获取文本
        /// </summary>
        /// <returns></returns>
        public string getCode()
        {
            GenUsing();
            GenNameSpace();
            GenClass("Model");
            GenClass("Request");
            GenClass("Add");
            GenClass("Update");
            GenClass("Delete");
            return TextUsing + TextNamespace.Replace("#Class#", TextClass);
        }

        public void GenFile()
        {
            fg.Gen("GenModel", $"{ClassName}Model.cs", getCode());
        }
    }
}
