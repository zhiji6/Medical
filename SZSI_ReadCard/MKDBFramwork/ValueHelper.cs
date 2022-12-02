using MK.OLD.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MK.OLD.DBUtiliy
{
    /// <summary>
    /// 数据帮助类
    /// @author lianghaifeng
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueHelper<T>
    {
        /// <summary>
        /// 存放每次生成的table结构
        /// @date 2016-03-16
        /// </summary>
        static Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();

        /// <summary>
        /// 实体转化为DataTable
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DataTable EntityToDataTable(T entity, string tableName = "", string GeneralColumns = "")
        {
            DataTable dt = null;
            dt = GeneralDataTable(tableName, GeneralColumns);
            if (entity != null)
            {
                EntityToDataRow(entity, dt, GeneralColumns);
            }
            return dt;
        }

        /// <summary>
        /// 实体转化为DataTable
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DataTable EntityToDataTable(List<T> entity, string tableName = "", string GeneralColumns = "")
        {
            DataTable dt = null;
            dt = GeneralDataTable(tableName, GeneralColumns);
            if (entity != null)
            {
                EntityToDataRow(entity, dt, GeneralColumns);
            }
            return dt;
        }



        /// <summary>
        /// 生成类型对应的表结构
        /// </summary>
        /// <returns></returns>
        private static DataTable GeneralDataTable(string tName = "", string GeneralColumn = "")
        {
            string[] columns = { };
            if (!string.IsNullOrEmpty(GeneralColumn))
            {
                columns = GeneralColumn.Split(new char[] { ',' });
            }
            string tableName = "";
            DataTable dt = null;
            Type t = typeof(T);
            tableName = t.Name.Replace("Model", "");
            if (!string.IsNullOrEmpty(tName))
            {
                tableName = tName;
            }
            if (dic.ContainsKey(tableName))
            {
                dt = (DataTable)dic[tableName];
            }
            else
            {
                dt = new DataTable(tableName);

                if (columns.Length > 0)
                {
                    foreach (string c in columns)
                    {
                        PropertyInfo pi = t.GetProperty(c);
                        if (pi != null)
                        {
                            dt.Columns.Add(pi.Name);
                        }
                    }
                }
                else
                {
                    PropertyInfo[] pis = t.GetProperties();
                    foreach (PropertyInfo pi in pis)
                    {
                        ColumnAttr ca = (ColumnAttr)pi.GetCustomAttribute(typeof(ColumnAttr), true);
                        if (ca == null || ca.GeneralColumn)
                        {
                            dt.Columns.Add(pi.Name);
                        }
                    }
                }
                //表结构添加到缓存
                dic.Add(tableName, dt.Clone());
            }
            return dt.Clone();
        }

        /// <summary>
        /// 实体值转化为对应列的值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dt"></param>
        private static void EntityToDataRow(T entity, DataTable dt, string GeneralColumns = "")
        {
            string[] gColumns = { };
            if (string.IsNullOrEmpty(GeneralColumns))
            {
                gColumns = GeneralColumns.Split(new char[] { ',' });
            }
            Type t = entity.GetType();
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                if (gColumns.Length > 0)
                {
                    if (gColumns.Contains(dc.ColumnName))
                    {
                        dr[dc.ColumnName] = entity.GetType().GetProperty(dc.ColumnName).GetValue(entity);
                    }
                }
                else
                {
                    dr[dc.ColumnName] = entity.GetType().GetProperty(dc.ColumnName).GetValue(entity);
                }
            }
            dt.Rows.Add(dr);
        }


        /// <summary>
        /// 实体值转化为对应列的值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dt"></param>
        private static void EntityToDataRow(List<T> entity, DataTable dt, string GeneralColumns = "")
        {
            string[] gColumns = { };
            if (string.IsNullOrEmpty(GeneralColumns))
            {
                gColumns = GeneralColumns.Split(new char[] { ',' });
            }

            DataRow dr = dt.NewRow();

            for (int i = 0; i < entity.Count; i++)
            {
                T valmodel = entity[i];

                foreach (DataColumn dc in dt.Columns)
                {
                    if (gColumns.Length > 0)
                    {
                        if (gColumns.Contains(dc.ColumnName))
                        {
                            dr[dc.ColumnName] = valmodel.GetType().GetProperty(dc.ColumnName).GetValue(valmodel);
                        }
                    }
                    else
                    {
                        dr[dc.ColumnName] = valmodel.GetType().GetProperty(dc.ColumnName).GetValue(valmodel);
                    }
                }
            }


            dt.Rows.Add(dr);
        }


        /// <summary>
        /// List数据转化为datatable
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable(List<T> list, string tName = "", string generalColumns = "")
        {
            DataTable dt = null;
            dt = GeneralDataTable(tName, generalColumns);
            if (list != null)
            {
                foreach (T t in list)
                {
                    EntityToDataRow(t, dt, generalColumns);
                }
            }
            return dt;
        }

        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, @"^[+-]?\d*$");
        }

        public static bool IsNumeric(string str)
        {
            return Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$");
        }

        public static bool IsNull<T>(T entity)
        {
            if (entity == null)
                return false;
            if (entity is DataTable)
            {
                DataTable dt = entity as DataTable;
                if (dt != null)
                {
                    return dt.Rows.Count > 0;
                }
            }
            else if (entity is DataSet)
            {
                DataSet ds = entity as DataSet;
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0].Rows.Count > 0;
                }
            }
            else if (entity is List<T>)
            {
                List<T> list = entity as List<T>;
                return list != null;
            }
            return false;
        }
    }
}
