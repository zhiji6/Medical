using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace SY.Com.Medical.Model
{

    /// <summary>
    /// 
    /// </summary>
    public class In9001
    {
        /// <summary>
        /// 
        /// </summary>
        public In9001model signIn { get; set; }


    }

    /// <summary>
    /// 
    /// </summary>
    public class In9001model
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public string opter_no { get; set; }// 操作员编号   字符型	20		Y
        /// <summary>
        /// Mac地址,调用接口Message9001时可不传
        /// </summary>
        public string mac { get; set; }// 签到MAC地址 字符型	20		Y
        /// <summary>
        /// Ip地址,调用接口Message9001时可不传
        /// </summary>
        public string ip { get; set; }// 签到IP地址 字符型	20		Y
    }
    /// <summary>
    /// 
    /// </summary>
    public class Out9001
    {
        /// <summary>
        /// 
        /// </summary>
        public Signinoutb signinoutb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public class Signinoutb
        {
            /// <summary>
            /// 
            /// </summary>
            public DateTime sign_time { get; set; }//  签到时间 日期型         Y yyyy-MM-dd HH:mm:ss
            /// <summary>
            /// 
            /// </summary>
            public string sign_no { get; set; }// 签到编号 字符型	30		Y
        }



    }
    /// <summary>
    /// 
    /// </summary>
    public class CheckIn
        {
        /// <summary>
        /// 
        /// </summary>

        public string sign_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime sign_time { get; set;  }

        }
   
}