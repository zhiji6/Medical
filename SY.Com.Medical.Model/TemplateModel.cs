using SY.Com.Medical.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace SY.Com.Medical.Model
{
    
///<summary>
/// Template模型
/// </summary>
	public class TemplateModel : BaseModel 
	{ 
		///<summary> 
		///模板Id
		///</summary> 
		public int TemplateId {get;set;} 
		///<summary> 
		///创建人Id
		///</summary> 
		public int EmployeeId {get;set;} 
		/// <summary>
		/// 创建人姓名
		/// </summary>
		public string EmployeeName { get; set; }
		///<summary> 
		///模板名称
		///</summary> 
		public string TemplateName {get;set;} 
		///<summary> 
		///模板类型
		///</summary> 
		public string TemplateType {get;set;} 
		///<summary> 
		///西药处方数量
		///</summary> 
		public int WestCount {get;set;} 
		///<summary> 
		///中药处方数量
		///</summary> 
		public int EastCount {get;set;} 
		///<summary> 
		///项目处方数量
		///</summary> 
		public int ProjectCount {get;set;} 
		///<summary> 
		///处方文本
		///</summary> 
		[JsonIgnore]
		public string Content {get;set;} 
		/// <summary>
		/// 处方信息
		/// </summary>
		public List<PrescriptionAddStructure> Prescription { get; set; }
	}
	
///<summary>
/// Template模型
/// </summary>
	public class TemplateRequest : PageModel 
	{ 
		///<summary> 
		///创建人Id
		///</summary> 
		public int EmployeeId {get;set;} 
		///<summary> 
		///模板名称
		///</summary> 
		public string TemplateName {get;set;} 
		///<summary> 
		///模板类型
		///</summary> 
		public string TemplateType {get;set;} 		
	}
	
///<summary>
/// Template模型
/// </summary>
	public class TemplateAdd : BaseModel 
	{ 
		///<summary> 
		///创建人Id
		///</summary> 
		public int EmployeeId {get;set;} 
		///<summary> 
		///模板名称
		///</summary> 
		public string TemplateName {get;set;} 
		///<summary> 
		///模板类型
		///</summary> 
		public string TemplateType {get;set;} 		
		///<summary> 
		///处方内容文本
		///</summary> 
		[JsonIgnore]
		public string Content {get;set;} 
		/// <summary>
		/// 处方信息
		/// </summary>
		public List<PrescriptionAddStructure> Prescription { get; set; }
	}
	
///<summary>
/// Template模型
/// </summary>
	public class TemplateUpdate : BaseModel 
	{ 
		///<summary> 
		///模板Id
		///</summary> 
		public int TemplateId {get;set;} 
		///<summary> 
		///创建人Id
		///</summary> 
		public int EmployeeId {get;set;} 
		///<summary> 
		///模板名称
		///</summary> 
		public string TemplateName {get;set;} 
		///<summary> 
		///模板类型
		///</summary> 
		public string TemplateType {get;set;} 
		///<summary> 
		///处方文本Json格式
		///</summary> 
		[JsonIgnore]
		public string Content {get;set;}
		/// <summary>
		/// 处方信息
		/// </summary>
		public List<PrescriptionAddStructure> Prescription { get; set; }
	}
	
	///<summary>
	/// Template模型
	/// </summary>
	public class TemplateDelete : BaseModel 
	{ 
		///<summary> 
		///模板Id
		///</summary> 
		public int TemplateId {get;set;} 
	}
	/// <summary>
	/// 获取Template类型
	/// </summary>
	public class TemplateType
    {		
		/// <summary>
		/// 模板类型Id
		/// </summary>
		public int TemplateTypeId { get; set; }
		/// <summary>
		/// 模板类型名称
		/// </summary>
		public string TemplateName { get; set; }		
    }
	
} 