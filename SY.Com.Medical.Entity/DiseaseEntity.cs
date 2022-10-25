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
    [DB_Table("Disease")]
    [DB_Key("Id")]
    public class DiseaseEntity : BaseEntity 
	{ 
		///<summary> 
		///
		///</summary> 
		[DB_Key("DiseaseId")]
		public int DiseaseId { get;set;} 

		///<summary> 
		///
		///</summary> 
		[DB_Like]
		public string DiseaseName { get;set;} 
		///<summary> 
		///
		///</summary> 
		[DB_Like]
		public string DiseaseCode { get;set;} 
	}
} 