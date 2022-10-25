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
/// Disease模型
/// </summary>
	public class DiseaseModel : BaseModel 
	{ 
		///<summary> 
		///疾病名称
		///</summary> 
		public string DiseaseName { get;set;} 
		///<summary> 
		///疾病编码
		///</summary> 
		public string DiseaseCode { get;set;} 
	}
	
///<summary>
/// Disease模型
/// </summary>
	public class DiseaseRequest : BaseModel
	{ 
		///<summary> 
		///疾病名称
		///</summary> 
		public string DiseaseName { get;set;} 
	}
	
///<summary>
/// Disease模型
/// </summary>
	public class DiseaseAdd : BaseModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public string DiseaseName { get;set;} 
		///<summary> 
		///
		///</summary> 
		public string DiseaseCode { get;set;} 
	}
	
///<summary>
/// Disease模型
/// </summary>
	public class DiseaseUpdate : BaseModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public string DiseaseName { get;set;} 
		///<summary> 
		///
		///</summary> 
		public string DiseaseCode { get;set;} 
	}
	
///<summary>
/// Disease模型
/// </summary>
	public class DiseaseDelete : BaseModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public string DiseaseName { get;set;} 
		///<summary> 
		///
		///</summary> 
		public string DiseaseCode { get;set;} 
	}
	
} 