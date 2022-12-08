using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model.SYB
{
    /// <summary>
    /// 
    /// </summary>
    public class SYB90991
    {
        /// <summary>
        /// 人员医保号
        /// </summary>
        public string Psn_no { get; set; }
        /// <summary>
        /// 医保号
        /// </summary>
        public string Card { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class In90991
    {
        /// <summary>
        /// 
        /// </summary>
        public In90991Model data { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Out90991
    { 
        /// <summary>
        /// 
        /// </summary>
        public Out90991Model result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class In90991Model
    {
        /// <summary>
        /// 医药机构编码
        /// </summary>
        public string fixmedins_code { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 社保卡密码,如无密码，默认上传“000000”
        /// </summary>
        public string card_pwd { get; set; }
        /// <summary>
        /// 社保卡号 后续医院需增加入参，设置过渡期，如果用户使用旧卡会报错“当前使用的社保卡已被注销，请使用有效社保卡。
        /// </summary>
        public string sscno { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Out90991Model
    {
        /// <summary>
        /// 人员编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 社保卡状态
        /// </summary>
        public string card_stat { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string certno { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string psn_name { get; set; }
        /// <summary>
        /// 校验结果 1=成功0=失败
        /// </summary>
        public string check_stat { get; set; }
        /// <summary>
        /// 社保卡号校验结果 1=成功 0=失败
        /// </summary>
        public string sscno_check_stat { get; set; }
        /*
            00	未激活
            10	激活
            20	窗口挂失
            21	电话挂失
            22	单位网上申报挂失
            23	保健办挂失
            24	个人网上挂失
            25	网上服务大厅挂失 
            30	注销
            40	回收
            99	制卡中（无效卡）
         */
    }



}
