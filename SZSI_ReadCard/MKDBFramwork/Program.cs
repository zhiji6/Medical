using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MK.Clinic.DBUtility;
using System.Data.SqlClient;
using System.Data;
using MK.OLD.DBUtiliy;
using System.Security.Cryptography;
using ToolGood.PinYin;
using ToolGood.Words;
using System.Net;
using System.Net.Security;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using System.Net.Http.Headers;


namespace MKDBFramwork
{
    class Program
    {
        static void Main(string[] args)
        {
            string x = ",sdsd";
            string[] wuha = x.Split(',');
            Console.WriteLine(wuha.Length);
            Console.WriteLine(wuha[0] + ":" + wuha[1]);
            Console.ReadLine();

            //YBReadCard yb = new YBReadCard();
            //string result = yb.GetCardInfo("readcard","XX001");
            //Console.WriteLine(result);
            //Console.ReadLine();
            //string tableTypeName = "YBYPMLOption_wuha";
            //Console.WriteLine("创建表类型" + tableTypeName);
            //int i = CreateTableType(tableTypeName);
            //Console.WriteLine("创建表类型"+ tableTypeName +"完成" + i.ToString());
            //Console.ReadLine();
            //Console.WriteLine("插入数据");
            //InsertTableUseTableType();
            //Console.WriteLine("查询数据");
            //List<Model.YBYPMLOption> listwuha = QueryTableUseTableType();
            //foreach (var item in listwuha)
            //{
            //    Console.WriteLine(item.MC + "   " + item.DownLoadCount + "   " + item.DownLoadPage + "   " + item.DownLoadDate + "   ");
            //}
            //Console.ReadLine();
            //i = DropTalbeType(tableTypeName);
            //Console.WriteLine("删除表类型"+ tableTypeName + i.ToString());
            //Console.ReadLine();

            //Console.WriteLine(GetMD5("123456"));
            //Console.WriteLine(UserMd5("123456"));
            //string sql = @" Select YPBH,YPMC,PYJM,ZJM From XTYPXXB_YB Where PYJM = ''  ";
            //long[] zhids = new long[] { 2016010200000002, 2017010200000002, 2017040200000002, 2017060200000002, 2017062600000002, 2017080200000002, 2017120200000002,
            //                            2018010200000002,2018021200000002,2018040200000002,2018060200000002,2018100200000002};
            //for (int i=0;i<zhids.Length;i++)
            //{
            //    DataSet ds = DbHelperSQL.Query(zhids[i], sql);
            //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    {
            //        Console.WriteLine("第" + i.ToString() + "次循环启动");
            //        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            //        {
            //            DataRow dr = ds.Tables[0].Rows[j];
            //            string PinYin = WordsHelper.GetFirstPinYin(dr["YPMC"].ToString());
            //            string sqlUpdate = @" Update XTYPXXB_YB Set ZJM='" + PinYin.Replace("'", "''") + "',PYJM='" + PinYin.Replace("'", "''") + "'  Where YPBH = " + dr["YPBH"].ToString();
            //            DbHelperSQL.ExecuteSql(zhids[i], sqlUpdate);
            //            Console.WriteLine("成功:" + j.ToString());
            //        }
            //        Console.WriteLine("第" + i.ToString() + "次循环结束");
            //    }
            //}
            //1.获取下载状态
            //string url1 = @"http://120.26.165.247:8087/api/YB/GetYBMLOption?flag=ML001";
            //string result1 = GetResponse(url1);
            //HttpResponseMessage responseMessage1 = new HttpResponseMessage { Content = new StringContent(result1, Encoding.GetEncoding("UTF-8"), "application/json") };
            //string url2 = @"http://localhost:8001/SZSI_Smart/api/yb/ReadCard?method=iveryficode&flag=ML001";
            //string result2 = GetResponse(url2);
            //HttpResponseMessage responseMessage2 = new HttpResponseMessage { Content = new StringContent(result1, Encoding.GetEncoding("UTF-8"), "application/json")
            //Console.WriteLine("Hello World");
            //Console.ReadLine();
        }

        public static string GetResponse(string url)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }

        public static string PostResponse(string url, string postData)
        {
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }

        private static bool CheckValidationResult(object sender,
X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;// Always accept
        }

        public static string CommnReques(string url, string method = "post", string data = "")
        {
            string result = "";
            try
            {
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                //定义request并设置request的路径
                WebRequest request = HttpWebRequest.Create(url);
                request.Method = method;

                //初始化request参数
                string postData = data;

                //设置参数的编码格式，解决中文乱码
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                //设置request的MIME类型及内容长度
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                request.Timeout = 1000 * 30;

                //打开request字符流
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                //定义response为前面的request响应
                WebResponse response = request.GetResponse();

                //获取相应的状态代码
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                //定义response字符流
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();//读取所有     
                return responseFromServer;
            }
            catch (Exception ex)
            {
                result = "连接出错";
            }
            return result;



        }



        private static string GetMD5(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);

            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }

        public static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");

            }
            return pwd;
        }

        private static int CreateTableType(string tableTypeName)
        {
            string sql = @"

IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'"+ tableTypeName + @"' AND ss.name = N'dbo')
DROP TYPE [dbo].["+ tableTypeName + @"]

CREATE TYPE [dbo].["+ tableTypeName + @"] AS TABLE(
	[MC] [nvarchar](20) NULL,
	[DownLoadCount] [int] NOT NULL DEFAULT ((0)),
	[DownLoadPage] [int] NOT NULL DEFAULT ((0)),
	[DownLoadDate] [datetime] NULL,
	[Remark] [nvarchar](100) NULL,
	[Remark1] [nvarchar](100) NULL
)


 ";
            int i = DbHelperSQL.ExecuteSql(0, sql);
            return i;
        }

        private static int DropTalbeType( string tableTypeName)
        {
            string sql = @"  DROP TYPE " + tableTypeName;
            int i = DbHelperSQL.ExecuteSql(0, sql);
            return i;
        }

        private static void InsertTableUseTableType()
        {
            List<Model.YBYPMLOption> wuhaList = new List<Model.YBYPMLOption>() { new Model.YBYPMLOption { MC = "ML001", DownLoadCount = 0, DownLoadPage = 0, Remark = "", Remark1 = "", DownLoadDate = "2018-01-18 11:07:00" } };
            string Columns = "MC,DownLoadCount,DownLoadPage,DownLoadDate,Remark,Remark1";
            DataTable dtYPXXB = ValueHelper<Model.YBYPMLOption>.ListToDataTable(wuhaList, "YPXXB", Columns);
            SqlParameter[] parameters = {
                        new SqlParameter("@YBYPMLOption_wuha",SqlDbType.Structured) {Value = dtYPXXB }                    
                    };
            parameters[0].TypeName = "YBYPMLOption_wuha";
            string sql = @" 
                            Insert Into YBYPMLOption Select * from @YBYPMLOption_wuha ";
            DbHelperSQL.ExecuteSql(0, sql,parameters);
        }

        private static List<Model.YBYPMLOption> QueryTableUseTableType()
        {
            string sql = " Select * From  YBYPMLOption ";
            DataSet ds = DbHelperSQL.Query(0, sql);
            List<Model.YBYPMLOption> listwuha = new List<Model.YBYPMLOption>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {                
                for (int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    Model.YBYPMLOption wuha = new Model.YBYPMLOption() { MC = dr["MC"].ToString(), DownLoadCount = int.Parse(dr["DownLoadCount"].ToString()), DownLoadPage = int.Parse(dr["DownLoadPage"].ToString()), DownLoadDate = dr["DownLoadDate"].ToString() };
                    listwuha.Add(wuha);
                }
            }
            return listwuha;
        }

        //测试存储过程返回
        private static string ProcOutPut()
        {
            SqlParameter[] para = {
                new SqlParameter("@datacount",SqlDbType.Int) {Direction=ParameterDirection.Output }
            };
            DbHelperSQL.RunProcedure(2018050100000001, "testaha", para);
            return para[0].Value.ToString();
        }



    }
}
