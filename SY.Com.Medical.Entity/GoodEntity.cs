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
    /// 商品实体
    /// </summary>
    [DB_Table("Goods")]
    [DB_Key("GoodId")]
    public class GoodEntity : BaseEntity 
	{ 
		///<summary> 
		///商品ID
		///</summary> 
		[DB_Key("GoodId")]
		public int GoodId {get;set;} 
		///<summary> 
		///租户ID
		///</summary> 
		public int TenantId {get;set;} 
		///<summary> 
		///商品名称
		///</summary> 
		[DB_Like]
		public string GoodName {get;set;} 
		///<summary> 
		///规格
		///</summary> 
		public string Norm {get;set;}
		///<summary> 
		///厂家
		///</summary> 		
		public string Factory {get;set;} 
		///<summary> 
		///类型（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
		///</summary> 		
		[DB_Check("6","1,2,3")]
		[DB_Default(typeof(GoodType))]		
		public GoodType GoodType {get;set;} 
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public int GoodSort {get;set;} 
		///<summary> 
		///国药准字
		///</summary> 
		[DB_Like]
		public string GoodStandard {get;set;}
		///<summary> 
		///医保编码
		///</summary> 
		[DB_Like]
		public string InsuranceCode {get;set;}
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		[DB_Like]
		public string CustomerCode {get;set;}
		///<summary> 
		///条码
		///</summary> 
		[DB_Like]
		public string BarCode {get;set;}
		///<summary> 
		///搜索词根
		///</summary> 
		[DB_Like]
		public string SearchKey {get;set;} 
		///<summary> 
		///计价单位
		///</summary> 
		public string SalesUnit {get;set;} 
		///<summary> 
		///采购单位
		///</summary> 
		public string StockUnit {get;set;} 
		///<summary> 
		///采购单位--》计价单位比率，正整数
		///</summary> 
		public int Ratio {get;set;}
		/// <summary>
		/// 价格(项目直接使用此价格),药品不使用此价格
		/// </summary>
		public long Price { get; set; }
		/// <summary>
		/// 库存量
		/// </summary>
		public int Stock { get; set; }
		/// <summary>
		/// 用法
		/// </summary>
		public string Usage { get; set; }
		/// <summary>
		/// 每次用量
		/// </summary>
		public int Single { get; set; }
		/// <summary>
		/// 每日用量
		/// </summary>
		public string EveryDay { get; set; }
		/// <summary>
		/// 部位
		/// </summary>
		public string Place { get; set; }

	}
} 