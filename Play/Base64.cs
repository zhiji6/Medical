using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Play
{
    public class Base64
    {
        /// <summary>
        /// 文件转base64
        /// </summary>
        /// <returns>base64字符串</returns>
        public static string FileToBase64String(string filepathname)
        {
            FileStream fsForRead = new FileStream(filepathname, FileMode.Open);//文件路径
            string base64Str = "";
            try
            {
                //读写指针移到距开头10个字节处
                fsForRead.Seek(0, SeekOrigin.Begin);
                byte[] bs = new byte[fsForRead.Length];
                int log = Convert.ToInt32(fsForRead.Length);
                //从文件中读取10个字节放到数组bs中
                fsForRead.Read(bs, 0, log);
                base64Str = Convert.ToBase64String(bs);
                return base64Str;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.ReadLine();
                return base64Str;
            }
            finally
            {
                fsForRead.Close();
            }
        }

        /// <summary>
        /// Base64字符串转文件并保存
        /// </summary>
        /// <param name="base64String">base64字符串</param>
        /// <param name="fileName">保存的文件名</param>
        /// <returns>是否转换并保存成功</returns>
        public static bool Base64StringToFile(string base64String, string fileName)
        {
            bool opResult = false;
            try
            {
                string strDate = DateTime.Now.ToString("yyyyMMdd");
                string fileFullPath = Environment.CurrentDirectory + "/" + strDate;//文件保存路径
                if (!Directory.Exists(fileFullPath))
                {
                    Directory.CreateDirectory(fileFullPath);
                }

                string strbase64 = base64String.Trim().Substring(base64String.IndexOf(",") + 1);   //将‘，’以前的多余字符串删除
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(strbase64));
                FileStream fs = new FileStream(fileFullPath + "\\" + fileName, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] b = stream.ToArray();
                fs.Write(b, 0, b.Length);
                fs.Close();

                opResult = true;
            }
            catch (Exception e)
            {

                Console.WriteLine("异常类型: \t" + e.GetType());
                Console.WriteLine("异常描述：\t" + e.Message);
                Console.WriteLine("异常方法：\t" + e.TargetSite);
                Console.WriteLine("异常堆栈：\t" + e.StackTrace);
            }
            return opResult;
        }
    }
}
