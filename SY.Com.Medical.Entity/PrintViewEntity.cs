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
    [DB_Table("PrintViews")]
    [DB_Key("PrintViewId")]
    public class PrintViewEntity : BaseEntity 
	{ 
		///<summary> 
		///
		///</summary> 
		[DB_Key("PrintViewId")]
		public int PrintViewId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int TenantId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string PrintViewName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int Style {get;set;} 
		/// <summary>
		/// 
		/// </summary>
		public bool IsUse { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string PrintPathName { get; set; }
	}
} 