using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace YBSi
{
    public partial class Form1 : Form
    {
        public string key { get; set; }
        WebBrowser webBrowser1 = new WebBrowser();

        public Form1()
        {

        }

        public Form1(String[] args)
        {
            if (args.Length > 0)
            {
                key = Regex.Match(args[0], @"(?<=://).+?(?=:|/|\Z)").Value;
            }
            InitializeComponent();
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser1_DocumentCompleted);
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument doc = webBrowser1.Document;
            string sbh = doc.GetElementById("szsbh").OuterHtml;
            Regex reg = new Regex("value=(?<szsbh>[^>]*)");
            if (!string.IsNullOrEmpty(sbh) && reg.IsMatch(sbh))
            {
                Match m = reg.Match(sbh);
                string guid = key;
                string ybkh = m.Groups["szsbh"].Value;
                string StoreSzsbhUrl = System.Configuration.ConfigurationManager.AppSettings["StoreSzsbhUrl"];
                string url = StoreSzsbhUrl + "/api/SzSi/UploadYbkh?guid=" + guid + "&ybkh=" + ybkh;
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webRequest.Method = "GET";
                using (WebResponse wr = webRequest.GetResponse())
                {
                    string reader = new StreamReader(wr.GetResponseStream(), Encoding.Default).ReadToEnd();
                    string regstr = "\"ret\":0";
                    if (Regex.IsMatch(reader, regstr))
                    {
                        System.Environment.Exit(0);
                    }
                }
                ReadCardError();
            }
            else
            {
                ReadCardError();
            }
        }

        private void ReadCardError()
        {
            string closetime = System.Configuration.ConfigurationManager.AppSettings["CloseFormTime"];
            this.lblMsg.Text = "读卡失败\r\n(" + double.Parse(closetime) / 1000 + "秒后自动关闭)";
            System.Timers.Timer t = new System.Timers.Timer(double.Parse(closetime));
            t.Elapsed += new System.Timers.ElapsedEventHandler(T_Elapsed);
            t.AutoReset = true;   //设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件； 
        }

        private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string readurl = System.Configuration.ConfigurationManager.AppSettings["ReadSzsbUrl"];
            webBrowser1.Url = new Uri(readurl);
        }
    }
}
