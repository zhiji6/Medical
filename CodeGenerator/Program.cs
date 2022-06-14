using Dapper;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Repository;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var tableObject = new GenParam{ TableName = "Template", TableKey = "TemplateId", dbname = dbName.Clinic, Navigate = "", Navikey = "" };
            EntityGen eg = new EntityGen(tableObject);            
            eg.GenFile();
            RepositoryGen rg = new RepositoryGen(tableObject);
            rg.GenFile();
            ModelGen mg = new ModelGen(tableObject);
            mg.GenFile();
            BllGen bg = new BllGen(tableObject);
            bg.GenFile();
            ControllerGen clg = new ControllerGen(tableObject);
            clg.GenFile();
            Console.WriteLine("Hello World!");
        }
    }

    public class CodeGenRepository :  BaseRepository<BaseEntity>
    {
        //"server=.;database=Medical_Platform;uid=sa;pwd=sql2021"
        public CodeGenRepository(string connection) : base(connection)
        {
            /*
               "Medical_Platform": "server=.;database=Medical_Platform;uid=sa;pwd=sql2021",
      "Medical_Clinic": "server=.;database=Medical_Clinic;uid=sa;pwd=sql2021"
             */
        }
        public IEnumerable<ColumnEntity> getString(string tableName)
        {
            #region  获取实体sql语句
            string sql = @$"    SELECT
        replace(col.name, ' ', '_') ColumnName,
        column_id ColumnId,
        prop.value ColName,
        case typ.name
            when 'bigint' then 'long'
            when 'binary' then 'byte[]'
            when 'bit' then 'bool'
            when 'char' then 'string'
            when 'date' then 'DateTime'
            when 'datetime' then 'DateTime'
            when 'datetime2' then 'DateTime'
            when 'datetimeoffset' then 'DateTimeOffset'
            when 'decimal' then 'decimal'
            when 'float' then 'float'
            when 'image' then 'byte[]'
            when 'int' then 'int'
            when 'money' then 'decimal'
            when 'nchar' then 'char'
            when 'ntext' then 'string'
            when 'numeric' then 'decimal'
            when 'nvarchar' then 'string'
            when 'real' then 'double'
            when 'smalldatetime' then 'DateTime'
            when 'smallint' then 'short'
            when 'smallmoney' then 'decimal'
            when 'text' then 'string'
            when 'time' then 'TimeSpan'
            when 'timestamp' then 'DateTime'
            when 'tinyint' then 'byte'
            when 'uniqueidentifier' then 'Guid'
            when 'varbinary' then 'byte[]'
            when 'varchar' then 'string'
            else 'UNKNOWN_' + typ.name
        end ColumnType,
        case
            when col.is_nullable = 1 and typ.name in ( 'date', 'datetime', 'datetime2',  'smalldatetime', 'time')
            then '?'
            else ''
        end NullableSign
    from sys.columns col
        join sys.types typ on
            col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id
            LEFT JOIN sys.extended_properties prop ON col.object_id = prop.major_id AND col.column_id = prop.minor_id
    where object_id = object_id('{tableName}')
";
            #endregion
            var result = _db.Query<ColumnEntity>(sql);
            return result;
        }
    }

    public enum dbName
    {
        Platform = 1,
        Clinic = 2
    }

    public class ColumnEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string ColumnName { get; set; }
        public int ColumnId { get; set; }
        public string ColName { get; set; }
        public string ColumnType { get; set; }
        public string NullableSign { get; set; }
    }

    public class GenParam
    {
        public string TableName { get; set; }// "CaseBooks",
        public string TableKey { get; set; }// "CaseBookId"
        public dbName dbname { get; set; } // dbName.Clinic
        public string Navigate { get; set; }// "Patient"
        public string Navikey { get; set; }//"PatientId"
    }

}
