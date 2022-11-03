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
    [DB_Table("Prescriptions")]
    [DB_Key("PrescriptionId")]
    public class PrescriptionEntity : BaseEntity 
	{ 
		///<summary> 
		///
		///</summary> 
		[DB_Key("PrescriptionId")]
		public int PrescriptionId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int TenantId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int OutpatientId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int PreNo {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string PreName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int GoodsId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string GoodsName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string GoodsNorm {get;set;} 
		///<summary> 
		///
		///</summary> 
		public long GoodsPrice {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int GoodsNum {get;set;} 
		///<summary> 
		///
		///</summary> 
		public long GoodsCost {get;set;} 
		///<summary> 
		///用法
		///</summary> 
		public string GoodsUsage {get;set;} 
		///<summary> 
		///每天用量（频度）
		///</summary> 
		public string GoodsEveryDay {get;set;} 
		///<summary> 
		///用药天数
		///</summary> 
		public int GoodsDays {get;set;} 
		///<summary> 
		///计价单位
		///</summary> 
		public string GoodsSalesUnit {get;set;} 
		///<summary> 
		///中药-副数
		///</summary> 
		public int Pair {get;set;} 
		///<summary> 
		///部位
		///</summary> 
		public string Place {get;set;} 
		/// <summary>
		/// 医保编码
		/// </summary>
		public string InsuranceCode { get; set; }
		/// <summary>
		/// 机构编码
		/// </summary>
		public string CustomerCode { get; set; }
		/// <summary>
		/// 单次用量
		/// </summary>
		public int SingleNum { get; set; }
	}
} 