using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SYBCardModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int result { get; set; }//0成功，非0失败
        /// <summary>
        /// 
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// 发卡地区行政区划代码（卡识别码前6位）、社会保障号码、卡号、卡识别码、姓名、卡复位信息（仅取历史字节）
        /// 、规范版本、发卡日期、卡有效期、终端机编号、终端设备号。
        /// 各数据项之间以“|”分割，且最后一个数据项以“|”结尾
        /// </summary>
        public string cardinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string err_msg { get; set; }

    }

}
