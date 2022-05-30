using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Enum
{

    /// <summary>
    /// 是否启用
    /// </summary>
    public enum Enable 
    {
        /// <summary>
        /// 无用
        /// </summary>
        无用=0,
        /// <summary>
        ///  启用
        /// </summary>
        启用 = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        禁用=2
    }

    /// <summary>
    /// 是否老板
    /// </summary>
    public enum IsBoss
    {
        /// <summary>
        /// 无用
        /// </summary>
        无用=0,
        /// <summary>
        /// 老板
        /// </summary>
        是=1,
        /// <summary>
        /// 员工
        /// </summary>
        不是=2
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// 男
        /// </summary>
        男 = 1,        
        /// <summary>
        /// 女
        /// </summary>
        女 = 2
    }
}
