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
    [DB_Table("Template")]
    [DB_Key("TemplateId")]
    public class TemplateEntity : BaseEntity 
	{ 
		///<summary> 
		///
		///</summary> 
		[DB_Key("TemplateId")]
		public int TemplateId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int TenantId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int EmployeeId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string TemplateName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string TemplateType {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int WestCount {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int EastCount {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int ProjectCount {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string Content {get;set;} 
	}
} 