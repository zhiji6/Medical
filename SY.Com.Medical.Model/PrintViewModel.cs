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
/// PrintView模型
/// </summary>
	public class PrintViewModel : BaseModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public int PrintViewId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string PrintViewName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int Style {get;set;} 
		/// <summary>
		/// 打印视图文件地址(不带域名)
		/// </summary>
		public string Url { get { return $"Print/PrintView/{Style}/{TenantId}/print.grf"; } }
		///<summary> 
		///
		///</summary> 
		public int IsDelete {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsEnable {get;set;} 
	}

	/// <summary>
	/// 打印视图类型
	/// </summary>
	public class PrintStyleModel : BaseModel
    {
		/// <summary>
		/// 类型Id,1:挂号打印，2:退号打印,3:中药处方打印,4:西药处方打印,5:项目处方打印,6:收费打印,7:退费打印,8:治疗单打印,9:病历打印
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// 类型Id,1:挂号打印，2:退号打印,3:中药处方打印,4:西药处方打印,5:项目处方打印,6:收费打印,7:退费打印,8:治疗单打印,9:病历打印
		/// </summary>
		public string Name { get; set; }
    }
	
///<summary>
/// PrintView模型
/// </summary>
	public class PrintViewRequest : BaseModel 
	{
		///<summary> 
		/// 打印视图Id，可使用getStyle接口获取
		///</summary> 
		public int Style {get;set;} 
	}
	
///<summary>
/// PrintView模型
/// </summary>
	public class PrintViewAdd : BaseModel 
	{ 
		///<summary> 
		///打印视图类型Id
		///</summary> 
		public int Style {get;set;} 
	}
	
///<summary>
/// PrintView模型
/// </summary>
	public class PrintViewUpdate : BaseModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public int PrintViewId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string PrintViewName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int Style {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsDelete {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsEnable {get;set;} 
	}
	
///<summary>
/// PrintView模型
/// </summary>
	public class PrintViewDelete : BaseModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public int PrintViewId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string PrintViewName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int Style {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsDelete {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsEnable {get;set;} 
	}

	/// <summary>
	/// 打印视图上传
	/// </summary>
	public class PrintUpload
    {
		/// <summary>
		/// 租户Id
		/// </summary>
		public int TenantId { get; set; }
		/// <summary>
		/// 类型Id
		/// </summary>
		public int Style { get; set; }

    }
	
} 