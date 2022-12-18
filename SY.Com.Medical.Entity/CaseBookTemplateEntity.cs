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
    /// 实体
    /// </summary>
    [DB_Table("CaseBookTemplates")]
    [DB_Key("CaseBookTemplateId")]
    public class CaseBookTemplateEntity : BaseEntity 
	{ 
		///<summary> 
		///
		///</summary> 
		[DB_Key("CaseBookTemplateId")]
		public int CaseBookTemplateId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int TenantId {get;set;} 
		///<summary> 
		///
		///</summary> 
		[DB_Like]
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
		public string DiseaseCode {get;set;} 
	}
} 