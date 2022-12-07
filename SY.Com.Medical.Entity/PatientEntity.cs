using SY.Com.Medical.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Entity
{
    /// <summary>
    ///  患者实体    
    /// </summary>
    [DB_Table("Patients")]
    [DB_Key("PatientId")]
    public class PatientEntity : BaseEntity
    {
        /// <summary>
        /// 主键ID    
        /// </summary>
        [DB_Key("PatientId")]
        public int PatientId { get; set; }

        /// <summary>
        /// 租户ID    
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 患者姓名    
        /// </summary>
        [DB_Like]
        public string PatientName { get; set; }

        /// <summary>
        /// 姓名拼音    
        /// </summary>
        [DB_Like]
        public string PatientPinYin { get; set; }

        /// <summary>
        /// 电话（手机号）    
        /// </summary>
        [DB_Like]
        public string Phone { get; set; }

        /// <summary>
        /// 性别    
        /// </summary>
        public Enum.Sex Sex { get; set; }

        /// <summary>
        /// 出生日期    
        /// </summary>        
        public DateTime? CSRQ { get; set; }

        /// <summary>
        /// 身份证号    
        /// </summary>
        [DB_Like]
        public string SFZ { get; set; }

        /// <summary>
        /// 医保电脑号    
        /// </summary>
        [DB_Like]
        public string YBDNH { get; set; }

        /// <summary>
        /// 医保卡号    
        /// </summary>
        [DB_Like]
        public string YBKH { get; set; }

        /// <summary>
        /// 地址    
        /// </summary>
        [DB_Like]
        public string Addr { get; set; }
        [DB_Like]
        public string SearchKey { get; set; }

        /// <summary>
        /// 医保编号
        /// </summary>
        public string psn_no { get; set; }
        /// <summary>
        /// 医保区划
        /// </summary>
        public string insuplc_admdvs { get; set; }
    }
}
