using SY.Com.Medical.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Entity
{
    /// <summary>
    ///  科室实体    
    /// </summary>
    [DB_Table("Departments")]
    [DB_Key("DepartmentId")]
    public class DepartmentEntity : BaseEntity
    {
        /// <summary>
        /// 科室ID    
        /// </summary>
        [DB_Key("DepartmentId")]
        public int DepartmentId { get; set; }
        /// <summary>
        /// 租户ID
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 科室名称    
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 科室编码    
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }



    }

}
