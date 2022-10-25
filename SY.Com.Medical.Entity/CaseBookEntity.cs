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
    /// 病历实体
    /// </summary>
    [DB_Table("CaseBooks")]
    [DB_Key("CaseBookId")]
    public class CaseBookEntity : BaseEntity
    {
        /// <summary>
        /// 主键ID    
        /// </summary>
        [DB_Key("CaseBookId")]
        public int CaseBookId { get; set; }

        /// <summary>
        /// 租户ID    
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 患者ID    
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// 门诊ID    
        /// </summary>
        public int OutPatientId { get; set; }

        /// <summary>
        /// 医生ID,对应Role是医生的EmployeeId    
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// 科室id    
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 病历类型枚举ID    
        /// </summary>
        public CaseBookType CaseBookTypeId { get; set; }

        /// <summary>
        /// 病历编号    
        /// </summary>
        public long CaseBookBH { get; set; }

        /// <summary>
        /// 主诉    
        /// </summary>
        public string Complaint { get; set; }

        /// <summary>
        /// 诊断    
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// 疾病
        /// </summary>
        public string Disease { get; set; }
        /// <summary>
        /// 疾病编码
        /// </summary>
        public string DiseaseCode { get; set; }

        /// <summary>
        /// 门诊日期    
        /// </summary>
        public DateTime? OutPatientDate { get; set; }

        /// <summary>
        /// 医嘱    
        /// </summary>
        public string CaseOrder { get; set; }

        /// <summary>
        /// 现病史    
        /// </summary>
        public string PastCase { get; set; }

        /// <summary>
        /// 既往史    
        /// </summary>
        public string HistoryCase { get; set; }

        /// <summary>
        /// 体格检查    
        /// </summary>
        public string Physical { get; set; }

        /// <summary>
        /// 治疗意见    
        /// </summary>
        public string Opinions { get; set; }

        /// <summary>
        /// 牙位    
        /// </summary>
        public string Tooth { get; set; }

        /// <summary>
        /// 部位    
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// 医生姓名    
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 科室    
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 患者导航属性
        /// </summary>
        [DB_NotColum]
        [DB_Navigate("Patients", "PatientId")]
        public PatientEntity Patient { get; set; }
    }

}
