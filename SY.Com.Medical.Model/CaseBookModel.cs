using SY.Com.Medical.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model
{
    /// <summary>
    /// 病历全模型
    /// </summary>
    public class CaseBookModel : BaseModel
    {
        /// <summary>
        /// 主键ID    
        /// </summary>
        public int CaseBookId { get; set; }

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
        public int CaseBookTypeId { get ; set; }

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
        /// 门诊入诊日期    
        /// </summary>
        public DateTime? CreateTime { get; set; }

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
        public PatientModel PatientInfo { get; set; }

        
    }
    
    /// <summary>
    /// 病历新增模型
    /// </summary>
    public class CaseBookAdd  : BaseModel
    {
        /// <summary>
        /// 患者ID    
        /// </summary>
        [Required]
        [Range(0,100000000)]
        public int PatientId { get; set; }

        /// <summary>
        /// 医生ID,对应Role是医生的EmployeeId    
        /// </summary>
        [Required]
        [Range(0, 100000000)]
        public int DoctorId { get; set; }
        /// <summary>
        /// 病历类型枚举ID    
        /// </summary>
        [Required]
        public CaseBookType CaseBookTypeId { get; set; }
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

    }

    /// <summary>
    /// 病历修改模型
    /// </summary>
    public class CaseBookUpdate : BaseModel
    {

        /// <summary>
        /// 病历ID主键   
        /// </summary>
        [Required]
        [Range(0, 100000000)]
        public int CaseBookId { get; set; }
        /// <summary>
        /// 医生ID,对应Role是医生的EmployeeId    
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// 病历类型枚举ID    
        /// </summary>
        [Required]
        public CaseBookType CaseBookTypeId { get; set; }
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

    }

    /// <summary>
    /// 病历删除模型
    /// </summary>
    public class CaseBookDelete : BaseModel
    {
        /// <summary>
        /// 病历ID主键   
        /// </summary>
        [Required]
        [Range(0, 100000000)]
        public int CaseBookId { get; set; }
    }    

    /// <summary>
    /// 病历分页模型
    /// </summary>
    public class CaseBookPage : PageModel
    {
        /// <summary>
        /// 患者ID    
        /// </summary>
        public int PatientId { get; set; }
    }

    /// <summary>
    /// 医生科室
    /// </summary>
    public class CaseBookDoctorDepart : BaseModel
    {
        /// <summary>
        /// 医生Id
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string DoctorName { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string Departments { get; set; }
        /// <summary>
        /// 科室Id
        /// </summary>
        public int DepartmentId { get; set; }
    }

}
