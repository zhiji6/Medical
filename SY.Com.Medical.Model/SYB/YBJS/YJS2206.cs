using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace SZSI_Smart.Model.SYB
{
    /// <summary>
    /// 
    /// </summary>
    public class YJS2206
    {
        /// <summary>
        /// 
        /// </summary>
        public class In2206Data
        {
            /// <summary>
            /// 
            /// </summary>
            public In2206 data { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        public class In2206
        {
            /// <summary>
            /// 门诊ID
            /// </summary>
            [JsonIgnore]
            public int OutPatientId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_no { get; set; }// 人员编号    字符型	30		Y
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_cert_type { get; set; }// 就诊凭证类型 字符型	3	Y Y
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_cert_no { get; set; }// 就诊凭证编号  字符型	50			就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            /// <summary>
            /// 
            /// </summary>
            public string med_type { get; set; }   // 医疗类别 字符型	6	Y Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? medfee_sumamt { get; set; }// 医疗费总额   数值型	16,2		Y
            /// <summary>
            /// 
            /// </summary>
            public string psn_setlway { get; set; }// 个人结算方式 字符型	6	Y Y
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_id { get; set; }// 就诊ID    字符型	30		Y
            /// <summary>
            /// 
            /// </summary>
            public string chrg_bchno { get; set; }//  收费批次号 字符型	30		Y
            /// <summary>
            /// 
            /// </summary>
            public string acct_used_flag { get; set; }//  个人账户使用标志 字符型	1	Y Y
            /// <summary>
            /// 
            /// </summary>
            public string insutype { get; set; }// 险种类型    字符型	6	Y Y

        }
        /// <summary>
        /// 
        /// </summary>
        public class Out2206
        {
            /// <summary>
            /// 
            /// </summary>
            public SetlInfo setlinfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<SetlDetail> setldetail { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        public class SetlInfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_id { get; set; }// 就诊ID    字符型	30	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string psn_no { get; set; } //人员编号 字符型	30	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string psn_name { get; set; } //   人员姓名 字符型	50	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string psn_cert_type { get; set; }//   人员证件类型 字符型	6	Y Y
            /// <summary>
            /// 
            /// </summary>
            public string certno { get; set; }// 证件号码    字符型	50	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string gend { get; set; }   //性别 字符型	6	Y
            /// <summary>
            /// 
            /// </summary>
            public string naty { get; set; }//    民族 字符型	3	Y
            /// <summary>
            /// 
            /// </summary>
            public DateTime? brdy { get; set; }//出生日期 日期型             yyyy-MM-dd
            /// <summary>
            /// 
            /// </summary>
            public float? age { get; set; }// 年龄 数值型	4,1	
            /// <summary>
            /// 
            /// </summary>
            public string insutype { get; set; }// 险种类型    字符型	6	Y
            /// <summary>
            /// 
            /// </summary>
            public string psn_type { get; set; }//    人员类别 字符型	6	Y Y
            /// <summary>
            /// 
            /// </summary>
            public string cvlserv_flag { get; set; }// 公务员标志   字符型	3	Y Y
            /// <summary>
            /// 
            /// </summary>
            public DateTime? setl_time { get; set; }// 结算时间    日期时间型 Y   yyyy-MM-dd HH:mm:ss
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_cert_type { get; set; }// 就诊凭证类型 字符型	3	Y
            /// <summary>
            /// 
            /// </summary>
            public string med_type { get; set; }//    医疗类别 字符型	6	Y Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? medfee_sumamt { get; set; }// 医疗费总额   数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? fulamt_ownpay_amt { get; set; }//   全自费金额 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? overlmt_selfpay { get; set; }// 超限价自费费用 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? preselfpay_amt { get; set; }//  先行自付金额 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? inscp_scp_amt { get; set; }//   符合政策范围金额 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? act_pay_dedc { get; set; } //  实际支付起付线 数值型	16,2	　	　	　
            /// <summary>
            /// 
            /// </summary>
            public decimal? hifp_pay { get; set; }// 基本医疗保险统筹基金支出    数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public double? pool_prop_selfpay { get; set; }//   基本医疗保险统筹基金支付比例 数值型	5,4	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? cvlserv_pay { get; set; }// 公务员医疗补助资金支出 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? hifes_pay { get; set; }//   企业补充医疗保险基金支出 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? hifmi_pay { get; set; }//  居民大病保险资金支出 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? hifob_pay { get; set; }//   职工大额医疗费用补助基金支出 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? maf_pay { get; set; }// 医疗救助基金支出 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? oth_pay { get; set; }// 其他支出 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? fund_pay_sumamt { get; set; }// 基金支付总额 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? psn_part_amt { get; set; }//    个人负担总金额 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? acct_pay { get; set; }//   个人账户支出 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? psn_cash_pay { get; set; } //   个人现金支出 数值型	16,2	  	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? hosp_part_amt { get; set; }//   医院负担金额 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? balc { get; set; }//    余额 数值型	16,2		Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? acct_mulaid_pay { get; set; }// 个人账户共济支付金额 数值型	16,2		Y
            /// <summary>
            /// 
            /// </summary>
            public string medins_setl_id { get; set; }//  医药机构结算ID 字符型	30		Y 存放发送方报文ID
            /// <summary>
            /// 
            /// </summary>
            public string clr_optins { get; set; }// 清算经办机构  字符型	6		　	
            /// <summary>
            /// 
            /// </summary>
            public string clr_way { get; set; } //清算方式    字符型	6	Y
            /// <summary>
            /// 
            /// </summary>
            public string clr_type { get; set; }  //  清算类别 字符型	6	Y Y

        }
        /// <summary>
        /// 
        /// </summary>
        public class SetlDetail
        {
            /// <summary>
            /// 
            /// </summary>
            public decimal? fund_pay_type { get; set; }// 基金支付类型  字符型	6	Y Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? inscp_scp_amt { get; set; }// 符合政策范围金额    数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? crt_payb_lmt_amt { get; set; }//    本次可支付限额金额 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public decimal? fund_payamt { get; set; }// 基金支付金额 数值型	16,2	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string fund_pay_type_name { get; set; } //基金支付类型名称 字符型	200		
            /// <summary>
            /// 
            /// </summary>
            public string setl_proc_info { get; set; }// 结算过程信息  字符型	4000	　	　

        }

    }
}