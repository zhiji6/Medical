using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SY.Com.Medical.Enum
{
    /// <summary>
    /// 商品类型
    /// </summary>
    public enum GoodType
    {
        /// <summary>
        /// 无=0
        /// </summary>
        无=0,
        /// <summary>
        /// 西药=1
        /// </summary>
        西药=1,
        /// <summary>
        /// 中成药=2
        /// </summary>
        中成药=2,
        /// <summary>
        /// 中药=3
        /// </summary>
        中药=3,
        /// <summary>
        /// 诊疗项目=4
        /// </summary>
        [JsonIgnore]
        项目=4,
        /// <summary>
        /// 材料=5
        /// </summary>
        [JsonIgnore]
        材料 =5
    }
}
