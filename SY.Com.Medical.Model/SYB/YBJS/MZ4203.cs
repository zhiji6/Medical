using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model.SYB
{
    /// <summary>
    /// 自费病人就诊以及费用明细上传完成
    /// </summary>
    public class MZ4203
    {
        /// <summary>
        /// 入参总格式
        /// </summary>
        public class In4203Data
        {
            /// <summary>
            /// 
            /// </summary>
            public InMZ4203 data { get; set; }
        }
        /// <summary>
        /// 真实入参
        /// </summary>
        public class InMZ4203 { 
            /// <summary>
            /// 机构内生成唯一就诊流水
            /// </summary>
            public string fixmedins_mdtrt_id { get; set; }
            /// <summary>
            /// 必须上传医保标准规范的编码
            /// </summary>
            public string fixmedins_code { get; set; }
            /// <summary>
            /// 费用明细和就诊信息上传完成后修改完成标志
            /// </summary>
            public string cplt_flag { get; set; }

        }

    }
}
