using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK.OLD.Model
{
    /// <summary>
    /// 自定义扩展属性
    /// @author lianghaifeng
    /// @date 2016-03-16
    /// </summary>
    public class ColumnAttr : Attribute
    {
        /// <summary>
        /// 标识此属性是否需要插入
        /// </summary>
        public bool Insert { get; set; }
        /// <summary>
        /// 标识此属性是否需要更新
        /// </summary>
        public bool Update { get; set; }

        /// <summary>
        /// 标识此属性是否生成datatable的结构
        /// </summary>
        public bool GeneralColumn { get; set; }
    }
}
