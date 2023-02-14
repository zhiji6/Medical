using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace SZSI_Smart.Model.SYB
{
    /// <summary>
    /// 人员信息获取
    /// </summary>
        public class In1101 
        {
            /// <summary>
            /// 
            /// </summary>
            public In1101data data { get; set; }         
        }
        /// <summary>
        /// 
        /// </summary>
    public class In1101data
    {
        /// <summary>
        ///  就诊凭证类型“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
        ///  根据页面选择的卡类型
        /// </summary>
        public string mdtrt_cert_type { get; set; }// 就诊凭证类型	字符型	“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号,"99"为外籍人员
        /// <summary>
        /// 读取到的卡信息，可能是医保卡号或者身份证号或者电子医保卡号
        /// </summary>
        public string mdtrt_cert_no { get; set; }// 就诊凭证编号  字符型	Y 
        /// <summary>
        /// 卡识别码,就诊凭证类型为“03”时必填
        /// </summary>
        public string card_sn { get; set; }// 卡识别码 字符型	32			就诊凭证类型为“03”时必填
        /// <summary>
        /// 
        /// </summary>        
        public DateTime begntime { get; set; } //   开始时间 日期时间型               获取历史参保信息时传入
        /// <summary>
        /// 人员证件类型,暂时都输入1
        /// </summary>
        public string psn_cert_type { get; set; } //  人员证件类型 字符型	6	Y
        /// <summary>
        /// 
        /// </summary>        
        public string certno { get; set; }//  证件号码 字符型	50		
        /// <summary>
        /// 
        /// </summary>        
        public string psn_name { get; set; }// 人员姓名    字符型	50		
    }
    /// <summary>
    /// 
    /// </summary>
    public class Out1101
    {

        /// <summary>
        /// 
        /// </summary>
        public BaseInfo baseinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InsuInfo> insuinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<IdetInfo> idetinfo { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class BaseInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string psn_no { get; set; } //人员编号    字符型	30		Y
        /// <summary>
        /// 
        /// </summary>
        public string psn_cert_type { get; set; }// 人员证件类型 字符型	6	Y Y
        /// <summary>
        /// 
        /// </summary>
        public string certno { get; set; }// 证件号码    字符型	50		Y
        /// <summary>
        /// 
        /// </summary>
        public string psn_name { get; set; }   //人员姓名 字符型	50		Y
        /// <summary>
        /// 
        /// </summary>
        public string gend { get; set; } // 性别 字符型	6	Y Y
        /// <summary>
        /// 
        /// </summary>
        public string naty { get; set; }// 民族  字符型	3	Y
        /// <summary>
        /// 
        /// </summary>
        public DateTime brdy { get; set; }//   出生日期 日期型  yyyy-MM-dd
        /// <summary>
        /// 
        /// </summary>
        public double age { get; set; }// 年龄 数值型	4,1	　	Y
    }
    /// <summary>
    /// 
    /// </summary>
    public class InsuInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public double balc { get;set;}// 余额  数值型	16,2		Y
        /// <summary>
        /// 
        /// </summary>
        public string insutype { get; set; }//    险种类型 字符型	6	Y Y
        /// <summary>
        /// 
        /// </summary>
        public string psn_type { get; set; }// 人员类别    字符型	6	Y Y
        /// <summary>
        /// 
        /// </summary>
        public string psn_insu_stas { get; set; }// 人员参保状态  字符型	6	Y
        /// <summary>
        /// 
        /// </summary>
        public DateTime? psn_insu_date { get; set; }//   个人参保日期 日期型
        /// <summary>
        /// 
        /// </summary>
        public DateTime? paus_insu_date { get; set; }// 暂停参保日期  日期型
        /// <summary>
        /// 
        /// </summary>
        public string cvlserv_flag { get; set; } //   公务员标志 字符型	3	Y Y
        /// <summary>
        /// 
        /// </summary>
        public string insuplc_admdvs { get; set; }// 参保地医保区划 字符型	6		Y
        /// <summary>
        /// 
        /// </summary>
        public string emp_name { get; set; } //   单位名称 字符型	200	　		　

    }
    /// <summary>
    /// 
    /// </summary>
    public class IdetInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string psn_idet_type { get; set; }// 人员身份类别  字符型	3	Y Y
        /// <summary>
        /// 
        /// </summary>
        public string psn_type_lv { get; set; }// 人员类别等级  字符型	3	Y 详见残疾等级字典
        /// <summary>
        /// 
        /// </summary>
        public string memo { get; set; }// 备注  字符型	500	　	　	
        /// <summary>
        /// 
        /// </summary>
        public DateTime? begntime { get; set; }// 开始时间    日期时间型 Y   yyyy-MM-dd HH:mm:ss
        /// <summary>
        /// 
        /// </summary>
        public DateTime? endtime { get; set; }// 结束时间 日期时间型               yyyy-MM-dd HH:mm:ss

    }
}