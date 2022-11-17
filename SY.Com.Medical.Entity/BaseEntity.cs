using SY.Com.Medical.Attribute;
using SY.Com.Medical.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Entity
{
    /// <summary>
    /// 基础实体
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 是否可用:枚举    
        /// </summary>
        [DB_Default(typeof(Enable))]
        public Enable IsEnable { get; set; }

        /// <summary>
        /// 启用禁用
        /// </summary>
        [DB_Default(typeof(Delete))]
        public Delete IsDelete { get; set; }
        /// <summary>
        /// 记录创建时间
        /// </summary>
        [DB_Default(typeof(DateTime))]
        [DB_Limit("CreateTimeStart", "CreateTimeEnd")]
        public DateTime? CreateTime { get; set; }

        [DB_NotColum]
        public DateTime? CreateTimeStart { get; set; }
        [DB_NotColum]
        public DateTime? CreateTimeEnd { get; set; }

    }
}
