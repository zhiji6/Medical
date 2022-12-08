using MKDBFramwork;
using MkSi.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MkSi
{
    public struct TSSCInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 70)]
        public byte[] SSCID;
    }

    public partial class ReadICard : Form
    {        

        public string key { get; set; }
        public string method { get; set; }
        public string flag { get; set; }
        public string operate { get; set; }

        public string ylzh { get; set; }

        public static string StoreSzsbhUrl;


        public ReadICard()
        {
            InitializeComponent();
        }

        public ReadICard(ReadICardParameter rip,string foperate, string rmethod, string rflag)
        {
            StoreSzsbhUrl = System.Configuration.ConfigurationManager.AppSettings["StoreSzsbhUrl"];
            if (!string.IsNullOrEmpty(rip.Key) && !string.IsNullOrEmpty(rmethod) && !string.IsNullOrEmpty(rflag))
            {
                key = rip.Key;//Regex.Match(rip.Key, @"(?<=://).+?(?=:|/|\Z)").Value;
                method = rmethod;
                flag = rflag;
                operate = foperate;
            }
            InitializeComponent();
        }

        public ReadICard(string Key)
        {
            StoreSzsbhUrl = System.Configuration.ConfigurationManager.AppSettings["StoreSzsbhUrl"];
            if (!string.IsNullOrEmpty(Key))
            {
                key = Key;//Regex.Match(rip.Key, @"(?<=://).+?(?=:|/|\Z)").Value;
            }
            InitializeComponent();
        }

        public ReadICard(String[] args)
        {
            StoreSzsbhUrl = System.Configuration.ConfigurationManager.AppSettings["StoreSzsbhUrl"];
            if (args.Length > 0)
            {
                key = Regex.Match(args[0], @"(?<=://).+?(?=:|/|\Z)").Value;
            }
            InitializeComponent();
        }

        private void ReadICard_Load(object sender, EventArgs e)
        {
            Thread thead = new Thread(ReadCard);
            thead.Start();
        }

        private void ReadCard()
        {
            try
            {
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(method) || string.IsNullOrEmpty(flag) || string.IsNullOrEmpty(operate))
                {
                    MessageBox.Show("入参异常" + "key=" + key + " ; method=" + method + " ; flag=" + flag + ";operate=" + operate);
                    System.Environment.Exit(0);
                }
                if (operate == "test")
                {
                    Virutal();
                }
                else
                {
                    SYB();
                }                
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message + $"参数:StoreSzsbhUrl:{StoreSzsbhUrl}" + "堆栈:" + ex);
                MessageBox.Show("ReadCard:异常" + ex.Message + "StoreSzsbhUrl:" + StoreSzsbhUrl);                
                System.Environment.Exit(0);
            }            
        }



        private void SYB()
        {
            string host = System.Configuration.ConfigurationManager.AppSettings["host"];
            string area = System.Configuration.ConfigurationManager.AppSettings["area"];
            int retint = SYBReadCard.MyInit(host,area);
            if(retint != 0)
            {
                throw new Exception("读卡器初始化失败");
            }
            CardModel info = new MkSi.CardModel { result = -1,err_msg="为寻找到合适方法" };
            switch (method.ToLower())
            {
                case "readcard":info = SYBReadCard.readCard();break;
                case "readid": info = SYBReadCard.readID();break;
                case "readqr": info = SYBReadCard.readQR();break;
                case "checkpin":info = SYBReadCard.CheckPin();break;
                case "updatepin":info = SYBReadCard.UpdatePin();break;
                default:break;
            }
            if (info.result != 0)
            {
                MessageBox.Show("读卡器错误:" + info.err_msg);
                System.Environment.Exit(0);
            }
            string ylzh = Newtonsoft.Json.JsonConvert.SerializeObject(info);
            //string ylzh = info.cardinfo;
            StoreSzsbhUrl = string.Format(StoreSzsbhUrl, key, ylzh, "&");
            UploadDataToLocal();
        }

        private void Virutal()
        {
            StoreSzsbhUrl = string.Format(StoreSzsbhUrl, key, "123456", "&");
            UploadDataToLocal();
        }

        /// <summary>
        /// 上传数据到Redis
        /// </summary>
        private void UploadDataToLocal()
        {
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(StoreSzsbhUrl);
            webRequest.Method = "GET";
            using (WebResponse wr = webRequest.GetResponse())
            {
                string reader = new StreamReader(wr.GetResponseStream(), Encoding.Default).ReadToEnd();
                string regstr = "\"ret\":0";
                LogHelper.Info("时间:" + DateTime.Now.ToString("yyyyMMdd-hhmmss") + "内容:" + StoreSzsbhUrl);
                if (Regex.IsMatch(reader.ToLower(), regstr))
                {
                    this.Close();
                    System.Environment.Exit(0);
                }
                else {
                    MessageBox.Show("上传数据失败:" + reader);
                    this.Close();
                    System.Environment.Exit(0);
                }
            }
        }

    }
}
