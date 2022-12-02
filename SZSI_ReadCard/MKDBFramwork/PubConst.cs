using System;
using System.Collections.Generic;
using System.Configuration;
namespace MK.Clinic.DBUtility
{

    public class PubConst
    {
        public static string ConnectionString = string.Empty;

        private static Dictionary<string, string> dbConnectionStrings;
        private static List<KeyValuePair<string, Int64>> dbIDs;
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        private static Dictionary<string, string> ConnectionStrings
        {
            get
            {
                if (dbConnectionStrings == null)
                {
                    dbConnectionStrings = new Dictionary<string, string>();
                    string[] connectionStrings = ConfigurationManager.AppSettings["DBConnectionStrings"].Split('|');
                    foreach (string s in connectionStrings)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            string[] db = s.Split(':');
                            dbConnectionStrings.Add(db[0], db[1]);
                        }
                    }
                }
                return dbConnectionStrings;
            }
        }
        /// <summary>
        /// 获取DB分库规则
        /// </summary>
        private static List<KeyValuePair<string, Int64>> DBIDs
        {
            get
            {
                if (dbIDs == null)
                {
                    dbIDs = new List<KeyValuePair<string, Int64>>();
                    string[] connectionStrings = ConfigurationManager.AppSettings["DBIDS"].Split(';');
                    foreach (string s in connectionStrings)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            string[] db = s.Split(':');
                            dbIDs.Add(new KeyValuePair<string, Int64>(db[1], Int64.Parse(db[0])));
                        }
                    }
                }
                return dbIDs;
            }
        }

        /// <summary>
        /// 根据ID获取DBID,ID为租户ID或者0，0代表主库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static Int64 GetDBID(Int64 id)
        {
            Int64 dbid = 0;
            if (id > 0)
            {
                foreach (KeyValuePair<string, Int64> k in DBIDs)
                {
                    string[] arryDB = k.Key.Split('-');
                    if (arryDB.Length > 0)
                    {
                        Int64 minIDS = Int64.Parse(arryDB[0]);
                        Int64 MaxIDS = Int64.Parse(arryDB[1]);
                        if (id >= minIDS && id <= MaxIDS)
                        {
                            dbid = k.Value;
                            break;
                        }
                    }
                }
            }
            //else if(id==-1)
            //{
            //    dbid = -1;
            //}
            return dbid;
        }

        /// <summary>
        /// 根据ID获取DBID,ID为租户ID或者0，0代表主库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static List<Int64> GetDBID()
        {
            List<Int64> dbid = new List<Int64>();

            foreach (KeyValuePair<string, Int64> k in DBIDs)
            {
                string[] arryDB = k.Key.Split('-');
                if (arryDB.Length > 0)
                {
                    dbid.Add(k.Value);                          
                }
            }
            
            //else if(id==-1)
            //{
            //    dbid = -1;
            //}
            return dbid;
        }


        public static string GetDbconnectionString(Int64 id)
        {
            //id = id / (Int64)Math.Pow(10, 15);
            id = GetDBID(id);
            string connectionString = string.Empty;
            if (!ConnectionStrings.TryGetValue(id.ToString(), out connectionString))
                connectionString = ConnectionStrings["0"];
            return connectionString;
        }

        public static List<string> GetDbconnectionString()
        {
            //id = id / (Int64)Math.Pow(10, 15);
            List<string> list = new List<string>();
            List<Int64> ids = GetDBID();
            foreach (var id in ids)
            {
                string connectionString = string.Empty;
                if (!ConnectionStrings.TryGetValue(id.ToString(), out connectionString))
                    connectionString = ConnectionStrings["0"];
                list.Add(connectionString);
            }
         
            return list;
        }

        /// <summary>
        /// 获取版本号
        /// lifx2014年7月11日16:20:43
        /// </summary>
        /// <returns></returns>
        public static string Get_Version()
        {
            return ConfigurationManager.AppSettings["Version"];
        }

    }
}
