using System;
using System.Text.Json.Serialization;

namespace SZSI_Smart.Model.SYB
{
    /// <summary>
    /// 门诊挂号
    /// </summary>

    public class In2201
        {
        /// <summary>
        /// 
        /// </summary>
        public In2201data data { get; set; }

        }
    /// <summary>
    /// 
    /// </summary>
    public class In2201data
        {
        /// <summary>
        /// 医生Id
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string psn_no { get; set; }// 人员编号    字符型	30	　	Y
        /// <summary>
        /// 
        /// </summary>
        public string insutype { get; set; }//    险种类型 字符型	6	Y Y
        /// <summary>
        /// 
        /// </summary>
        public DateTime begntime { get; set; }// 开始时间    日期时间型 Y   挂号时间yyyy-MM-dd HH:mm:ss
        /// <summary>
        /// 
        /// </summary>
        public string mdtrt_cert_type { get; set; }// 就诊凭证类型 字符型	3	Y Y
        /// <summary>
        /// 
        /// </summary>
        public string mdtrt_cert_no { get; set; }//就诊凭证编号就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
        /// <summary>
        /// 
        /// </summary>        
        public string ipt_otp_no { get; set; }//  住院/门诊号 字符型	30	　	Y 院内唯一流水
        /// <summary>
        /// 
        /// </summary>        
        public string atddr_no { get; set; } //医师编码    字符型	30	　	Y
        /// <summary>
        /// 
        /// </summary>        
        public string dr_name { get; set; }//医师姓名 字符型	50	　	Y
        /// <summary>
        /// 
        /// </summary>        
        public string dept_code { get; set; } // 科室编码 字符型	30	　	Y
        /// <summary>
        /// 
        /// </summary>        
        public string dept_name { get; set; } //  科室名称 字符型	100	　	Y
        /// <summary>
        /// 
        /// </summary>        
        public string caty { get; set; }//    科别 字符型	6	Y Y
        }
    /// <summary>
    /// 
    /// </summary>
    public class Out2201
        {
        /// <summary>
        /// 
        /// </summary>
        public Out2201data data { get; set; }
        }
    /// <summary>
    /// 
    /// </summary>
    public class Out2201data 
    {
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_id { get; set; }// 就诊ID    字符型	30	　	Y 医保返回唯一流水
            /// <summary>
            /// 
            /// </summary>
            public string psn_no { get; set; } //人员编号    字符型	30	　	Y
            /// <summary>
            /// 
            /// </summary>
            public string ipt_otp_no { get; set; }//  住院/门诊号 字符型	30	　	Y 院内唯一流水
        }


    }
    /// <summary>
    /// 
    /// </summary>
    public class MKIn2201
    {
        /// <summary>
        /// 
        /// </summary>
        public long? zhid { get; set; }// 字符串 是
        /// <summary>
        /// 
        /// </summary>
        public long? YHBH { get; set; }//   字符串 是   用户编号
        /// <summary>
        /// 
        /// </summary>
        public string psn_no { get; set; }//  字符串 是   人员编号
        /// <summary>
        /// 
        /// </summary>
        public long? YSBH { get; set; }//    字符串 是   医生诊所云编号
        /// <summary>
        /// 
        /// </summary>
        public long? KSBH { get; set; }//    字符串 是   科室诊所云编号
        /// <summary>
        /// 
        /// </summary>
        public string mdtrt_cert_no { get; set; }//  字符串 是   就诊凭证类型
        /// <summary>
        /// 
        /// </summary>
        public string mdtrt_cert_type { get; set; }// 字符串 是   就诊凭证编号
        /// <summary>
        /// 
        /// </summary>
        public string card_sn { get; set; }//类型为03时间必填

    }
    
}