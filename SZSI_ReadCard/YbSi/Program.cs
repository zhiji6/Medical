//using gregn6Lib;
using MKDBFramwork;
using MkSi.Entity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace MkSi
{
    static class Program
    {
        //static GridppReport rptMain = new GridppReport();
        //static GridppReport Report = new GridppReport();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (args.Length > 0)
                {
                    string operate = HttpUtility.UrlDecode(GetValueByKey(args[0], "operate")).TrimEnd('/');
                    string data = HttpUtility.UrlDecode(GetValueByKey(args[0], "data")).TrimEnd('/');                    
                    string method = HttpUtility.UrlDecode(GetValueByKey(args[0], "method")).TrimEnd('/');
                    string flag = HttpUtility.UrlDecode(GetValueByKey(args[0], "flag")).TrimEnd('/');
                    ReadCard(operate,data, method, flag);
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Debug(ex.Message + "堆栈:" + ex);
                MessageBox.Show(ex.Message);
            }
        }

        static string GetValueByKey(string sourceStr, String key)
        {
            string regstr = key+"=(?<"+key+">[^&]*)";
            if (Regex.IsMatch(sourceStr, regstr))
            {
                Match m = Regex.Match(sourceStr, regstr);
                return m.Groups[key].Value;
            }
            return string.Empty;
        }

        static void PrintReport(string data)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<PrintRptParameterList>(data);
            if (obj != null && obj.Data != null && obj.Data.Length > 0)
            {
                foreach (PrintRptParameter prp in obj.Data)
                {
                    //Report.LoadFromURL(prp.LoadFromUrl);
                    //Report.ParameterByName("ZHMC").AsString = prp.Zhmc;
                    //Report.LoadDataFromURL(prp.LoadDataFromURL);
                    //Report.PrintPreview(true);
                    //InsertSubReport("", "", prp);
                }
            }
            //rptMain.PrintPreview(true);
        }
        

        static void ReadCard(string operate, string data,string method,string flag)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ReadICardParameter>(data);
            ReadICard ric;
            if (obj == null)
            {
                ric = new ReadICard();
            }
            else {
                ric = new ReadICard(obj, operate, method, flag);
            }            
            Application.Run(ric);
        }

        static void InsertSubReport(string FileName, string ReportName, PrintRptParameter prp)
        {
            //IGRReportHeader Reportheader = rptMain.ReportHeaders.Add();
            //Reportheader.Height = 1.0;
            //Reportheader.CanGrow = true;

            ////插入一个静态文本框,显示报表标题文字
            //IGRSubReport SubReport = Reportheader.Controls.Add(GRControlType.grctSubReport).AsSubReport;
            //SubReport.Name = ReportName;
            //SubReport.Dock = GRDockStyle.grdsFill;
            //SubReport.ParentPageSettings = false;
            //SubReport.ToNewExcelSheet = true;  //在报表导出Excel时，将此子报表导出到一个新的工作表。

            //IGridppReport SubReportObject = SubReport.Report;

            ////从文件中载入报表模板数据到报表主对象
            
            ////SubReportObject.LoadFromFile(PathFileName);

            //SubReportObject.LoadFromURL(prp.LoadFromUrl);
            //SubReportObject.ParameterByName("ZHMC").AsString = prp.Zhmc;
            //SubReportObject.LoadDataFromURL(prp.LoadDataFromURL);

        }
    }
}
