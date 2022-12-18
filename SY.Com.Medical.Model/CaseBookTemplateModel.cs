using SY.Com.Medical.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SY.Com.Medical.Model
{
    
///<summary>
/// CaseBookTemplate模型
/// </summary>
	public class CaseBookTemplateModel : BaseModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public int CaseBookTemplateId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string CaseBookName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int CreatorId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int CaseBookTypeId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string Complaint {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string Diagnosis {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string Disease {get;set;} 
		///<summary> 
		///
		///</summary> 
		public DateTime? OutPatientDate {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string CaseOrder {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string PastCase {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string HistoryCase {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string Physical {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string Opinions {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string Tooth {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string Place {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string DoctorName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string DepartmentName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public DateTime CreateTime {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsEnable {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsDelete {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string DiseaseCode {get;set;} 
	}
	
///<summary>
/// CaseBookTemplate模型
/// </summary>
	public class CaseBookTemplateRequest : PageModel 
	{ 
		///<summary> 
		///病历模板名称
		///</summary> 
		public string CaseBookName {get;set;} 
		///<summary> 
		///创建者Id
		///</summary> 
		public int CreatorId {get;set;} 
		///<summary> 
		///病历模板类型
		///</summary> 
		public int CaseBookTypeId {get;set;} 
		/// <summary>
		/// 创建时间-开始
		/// </summary>
		public DateTime? CreateTimeStart { get; set; }
		/// <summary>
		/// 创建时间-结束
		/// </summary>
		public DateTime? CreateTimeEnd { get; set; }
	}
	
///<summary>
/// CaseBookTemplate模型
/// </summary>
	public class CaseBookTemplateAdd : BaseModel 
	{ 
		///<summary> 
		///病历模板名称
		///</summary> 
		public string CaseBookName {get;set;} 
		///<summary> 
		///创建者Id
		///</summary> 
		public int CreatorId {get;set;} 
		///<summary> 
		///病历模板类型
		///</summary> 
		public int CaseBookTypeId {get;set;} 
		///<summary> 
		///主诉
		///</summary> 
		public string Complaint {get;set;} 
		///<summary> 
		///诊断
		///</summary> 
		public string Diagnosis {get;set;} 
		///<summary> 
		///疾病
		///</summary> 
		public string Disease {get;set;} 
		///<summary> 
		///医嘱
		///</summary> 
		public string CaseOrder {get;set;} 
		///<summary> 
		///现病史
		///</summary> 
		public string PastCase {get;set;} 
		///<summary> 
		///既往史
		///</summary> 
		public string HistoryCase {get;set;} 
		///<summary> 
		///体格检查
		///</summary> 
		public string Physical {get;set;} 
		///<summary> 
		///治疗意见
		///</summary> 
		public string Opinions {get;set;} 
		///<summary> 
		///牙位
		///</summary> 
		public string Tooth {get;set;} 
		///<summary> 
		///部位
		///</summary> 
		public string Place {get;set;} 
		///<summary> 
		///疾病编码
		///</summary> 
		public string DiseaseCode {get;set;} 
	}
	
///<summary>
/// CaseBookTemplate模型
/// </summary>
	public class CaseBookTemplateUpdate : BaseModel 
	{ 
		///<summary> 
		///病历模板Id
		///</summary> 
		public int CaseBookTemplateId {get;set;}
		///<summary> 
		///病历模板名称
		///</summary> 
		public string CaseBookName { get; set; }
		///<summary> 
		///创建者Id
		///</summary> 
		public int CreatorId { get; set; }
		///<summary> 
		///病历模板类型
		///</summary> 
		public int CaseBookTypeId { get; set; }
		///<summary> 
		///主诉
		///</summary> 
		public string Complaint { get; set; }
		///<summary> 
		///诊断
		///</summary> 
		public string Diagnosis { get; set; }
		///<summary> 
		///疾病
		///</summary> 
		public string Disease { get; set; }
		///<summary> 
		///医嘱
		///</summary> 
		public string CaseOrder { get; set; }
		///<summary> 
		///现病史
		///</summary> 
		public string PastCase { get; set; }
		///<summary> 
		///既往史
		///</summary> 
		public string HistoryCase { get; set; }
		///<summary> 
		///体格检查
		///</summary> 
		public string Physical { get; set; }
		///<summary> 
		///治疗意见
		///</summary> 
		public string Opinions { get; set; }
		///<summary> 
		///牙位
		///</summary> 
		public string Tooth { get; set; }
		///<summary> 
		///部位
		///</summary> 
		public string Place { get; set; }
		///<summary> 
		///疾病编码
		///</summary> 
		public string DiseaseCode { get; set; }
	}
	
///<summary>
/// CaseBookTemplate模型
/// </summary>
	public class CaseBookTemplateDelete : BaseModel 
	{ 
		///<summary> 
		///病历模板Id
		///</summary> 
		public int CaseBookTemplateId {get;set;} 		
	}
	
} 