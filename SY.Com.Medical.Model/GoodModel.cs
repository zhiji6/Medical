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


	/// <summary>
	/// 药品列表出参
	/// </summary>
	public class GoodModels : BaseModel
    {
		///<summary> 
		///商品ID
		///</summary> 
		public int GoodId { get; set; }
		///<summary> 
		///商品名称
		///</summary> 
		public string GoodName { get; set; }
		///<summary> 
		///规格
		///</summary> 
		public string Norm { get; set; }
		///<summary> 
		///厂家
		///</summary> 
		public string Factory { get; set; }
		///<summary> 
		///类型（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
		///</summary> 
		public string GoodType { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public string GoodSort { get; set; }
		///<summary> 
		///国药准字
		///</summary> 
		public string GoodStandard { get; set; }
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode { get; set; }
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode { get; set; }
		///<summary> 
		///计价单位
		///</summary> 
		public string SalesUnit { get; set; }
		///<summary> 
		///创建时间
		///</summary> 
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 库存量,以计价单位显示,整数
		/// </summary>
		public int Stock { get; set; }
		/// <summary>
		/// 价格,保留3位小数
		/// </summary>		
		public double Price { get; set; }
		/// <summary>
		/// 科室(标记哪个科室能够开,一般指项目)
		/// </summary>
		public string DepartMent { get; set; }
	}

	/// <summary>
	/// 药品单个出参
	/// </summary>
	public class GoodModel : BaseModel
	{
		///<summary> 
		///商品ID
		///</summary> 
		public int GoodId { get; set; }
		///<summary> 
		///商品名称
		///</summary> 
		public string GoodName { get; set; }
		///<summary> 
		///规格
		///</summary> 
		public string Norm { get; set; }
		///<summary> 
		///厂家
		///</summary> 
		public int Factory { get; set; }
		///<summary> 
		///类型（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
		///</summary> 
		public int GoodType { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public int GoodSort { get; set; }
		///<summary> 
		///国药准字
		///</summary> 
		public string GoodStandard { get; set; }
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode { get; set; }
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode { get; set; }
		///<summary> 
		///条码
		///</summary> 
		public string BarCode { get; set; }
		///<summary> 
		///搜索词根
		///</summary> 
		public string SearchKey { get; set; }
		///<summary> 
		///计价单位
		///</summary> 
		public string SalesUnit { get; set; }
		///<summary> 
		///采购单位
		///</summary> 
		public string StockUnit { get; set; }
		///<summary> 
		///采购单位--》计价单位比率，正整数
		///</summary> 
		public int Ratio { get; set; }
		///<summary> 
		///
		///</summary> 
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 价格,保留3位小数
		/// </summary>		
		public double Price { get; set; }
		/// <summary>
		/// 库存量,以计价单位显示,整数
		/// </summary>
		public int Stock { get; set; }
		/// <summary>
		/// 用法
		/// </summary>
		public int Usage { get; set; }
		/// <summary>
		/// 单次用量
		/// </summary>
		public int Single { get; set; }
		/// <summary>
		/// 一天用量
		/// </summary>
		public int EveryDay { get; set; }
		/// <summary>
		/// 科室(标记哪个科室能够开,一般指项目)
		/// </summary>
		public string DepartMent { get; set; }
	}


	/// <summary>
	/// Goods全模型-列表
	/// </summary>
	public class GoodBllModels : BaseModel 
	{ 
		///<summary> 
		///商品ID
		///</summary> 
		public int GoodId {get;set;} 
		///<summary> 
		///商品名称
		///</summary> 
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
		public string GoodType {get;set;} 
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public string GoodSort {get;set;} 
		///<summary> 
		///国药准字
		///</summary> 
		public string GoodStandard {get;set;} 
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode {get;set;} 
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode {get;set;} 
		///<summary> 
		///条码
		///</summary> 
		public string BarCode {get;set;} 
		///<summary> 
		///搜索词根
		///</summary> 
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
		///<summary> 
		///
		///</summary> 
		public DateTime? CreateTime {get;set;}
		/// <summary>
		/// 价格,保留3位小数
		/// </summary>		
		public double Price { get; set; }
		/// <summary>
		/// 库存量,以计价单位显示,整数
		/// </summary>
		public int Stock { get; set; }
		/// <summary>
		/// 用法
		/// </summary>
		public string Usage { get; set; }
		/// <summary>
		/// 用法id
		/// </summary>
		public int UsageId { get; set; }
		/// <summary>
		/// 单次用量
		/// </summary>
		public int Single { get; set; }
		/// <summary>
		/// 一天用量
		/// </summary>
		public string EveryDay { get; set; }
		/// <summary>
		/// 一天用量Id
		/// </summary>
		public int EveryDayId { get; set; }
		/// <summary>
		/// 科室(标记哪个科室能够开,一般指项目)
		/// </summary>
		public string DepartMent { get; set; }
	}

	/// <summary>
	/// Goods全模型-单个
	/// </summary>
	public class GoodBllModel :BaseModel
    {
		///<summary> 
		///商品ID
		///</summary> 
		public int GoodId { get; set; }
		///<summary> 
		///商品名称
		///</summary> 
		public string GoodName { get; set; }
		///<summary> 
		///规格
		///</summary> 
		public string Norm { get; set; }
		///<summary> 
		///厂家
		///</summary> 
		public int Factory { get; set; }
		///<summary> 
		///类型（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
		///</summary> 
		public int GoodType { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public int GoodSort { get; set; }
		///<summary> 
		///国药准字
		///</summary> 
		public string GoodStandard { get; set; }
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode { get; set; }
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode { get; set; }
		///<summary> 
		///条码
		///</summary> 
		public string BarCode { get; set; }
		///<summary> 
		///搜索词根
		///</summary> 
		public string SearchKey { get; set; }
		///<summary> 
		///计价单位
		///</summary> 
		public int SalesUnit { get; set; }
		///<summary> 
		///采购单位
		///</summary> 
		public int StockUnit { get; set; }
		///<summary> 
		///采购单位--》计价单位比率，正整数
		///</summary> 
		public int Ratio { get; set; }
		///<summary> 
		///
		///</summary> 
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 价格,保留3位小数
		/// </summary>		
		public double Price { get; set; }
		/// <summary>
		/// 库存量,以计价单位显示,整数
		/// </summary>
		public int Stock { get; set; }
		/// <summary>
		/// 用法
		/// </summary>
		public int Usage { get; set; }
		/// <summary>
		/// 单次用量
		/// </summary>
		public int Single { get; set; }
		/// <summary>
		/// 一天用量
		/// </summary>
		public int EveryDay { get; set; }
		/// <summary>
		/// 科室(标记哪个科室能够开,一般指项目)
		/// </summary>
		public string DepartMent { get; set; }
	}

	/// <summary>
	/// 添加模型
	/// </summary>
	public class GoodAdd : BaseModel 
	{
		///<summary> 
		///商品ID
		///</summary> 
		[JsonIgnore]
		public int GoodId {get;set;} 
		///<summary> 
		///商品名称
		///</summary> 
		[Required]
		public string GoodName {get;set;}
		///<summary> 
		///规格
		///</summary> 
		[Required]
		public string Norm {get;set;}
		///<summary> 
		///厂家
		///</summary> 
		[Required]
		public int Factory {get;set;}
		///<summary> 
		///类型（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
		///</summary> 
		[Required]
		public int GoodType {get;set;}
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		[Required]
		public int GoodSort {get;set;} 
		///<summary> 
		///国药准字
		///</summary> 
		public string GoodStandard {get;set;} 
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode {get;set;} 
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode {get;set;} 
		///<summary> 
		///条码
		///</summary> 
		public string BarCode {get;set;}
		///<summary> 
		///搜索词根
		///</summary> 
		[JsonIgnore]
		public string SearchKey {get;set;}
		///<summary> 
		///计价单位
		///</summary> 
		[Required]
		public int SalesUnit {get;set;} 
		///<summary> 
		///采购单位
		///</summary> 
		public int StockUnit {get;set;} 
		///<summary> 
		///采购单位--》计价单位比率，正整数
		///</summary> 
		public int Ratio {get;set;}
		/// <summary>
		/// 价格-整数,厘为单位
		/// </summary>
		[JsonIgnore]
		public double? Price { get; set; }
		/// <summary>
		/// 单次用量
		/// </summary>
		public int Single { get; set; }
		/// <summary>
		/// 一天用量
		/// </summary>
		public int EveryDay { get; set; }
		/// <summary>
		/// 用法
		/// </summary>
		public int Usage { get; set; }
	}

	/// <summary>
	/// 修改模型
	/// </summary>
	public class GoodUpdate : BaseModel 
	{
		///<summary> 
		///商品ID
		///</summary> 
		public int GoodId { get; set; }
		///<summary> 
		///商品名称
		///</summary> 
		[Required]
		public string GoodName { get; set; }
		///<summary> 
		///规格
		///</summary> 
		[Required]
		public string Norm { get; set; }
		///<summary> 
		///厂家
		///</summary> 
		[Required]
		public int Factory { get; set; }
		///<summary> 
		///类型（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
		///</summary> 
		[Required]
		public int GoodType { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		[Required]
		public int GoodSort { get; set; }
		///<summary> 
		///国药准字
		///</summary> 
		public string GoodStandard { get; set; }
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode { get; set; }
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode { get; set; }
		///<summary> 
		///条码
		///</summary> 
		public string BarCode { get; set; }
		///<summary> 
		///搜索词根
		///</summary> 
		[JsonIgnore]
		public string SearchKey { get; set; }
		///<summary> 
		///计价单位
		///</summary> 
		[Required]
		public int SalesUnit { get; set; }
		///<summary> 
		///采购单位
		///</summary> 
		public int StockUnit { get; set; }
		///<summary> 
		///采购单位--》计价单位比率，正整数
		///</summary> 
		public int Ratio { get; set; }
		/// <summary>
		/// 价格-整数,厘为单位
		/// </summary>
		[JsonIgnore]
		public double? Price { get; set; }
		/// <summary>
		/// 单次用量
		/// </summary>
		public int Single { get; set; }
		/// <summary>
		/// 一天用量
		/// </summary>
		public int EveryDay { get; set; }
		/// <summary>
		/// 用法
		/// </summary>
		public int Usage { get; set; }
	}

	/// <summary>
	/// 删除模型
	/// </summary>
	public class GoodDelete : BaseModel 
	{
		///<summary> 
		///商品ID
		///</summary> 
		[Required]
		public int GoodId {get;set;} 	
	}

	/// <summary>
	/// 获取药品类型
	/// </summary>
	public class GoodSortModelRequest : BaseModel
    {
		/// <summary>
		/// 获取某种类型的药品,Flag:1:西药,2:中成药,3:中药,4:项目,5:材料
		/// </summary>
		public int Flag { get; set; }
    }

	/// <summary>
	/// 新增药品字典
	/// ex:{ "DicType":1,DicSubType:1,"消炎类"  },表示添加一个西药的药品分类“消炎类”
	/// </summary>
	public class GoodDicAddRequest : BaseModel
    {
		/// <summary>
		/// 字典类型,1:药品分类,2:厂家,3:用法,4:一天用量,5:单位,6:项目分类,7:材料分类
		/// </summary>
		public int DicType { get; set; }
		/// <summary>
		/// 字典子类型,当字典类型为1时,此字典必填
		/// 值:1:西药,2:中成药,3:中药
		/// </summary>
		public int DicSubType { get; set; }
		/// <summary>
		/// 字典内容
		/// </summary>
		public string Value { get; set; }
    }

	/// <summary>
	/// 删除字典
	/// </summary>
	public class GoodDicDeleteRequest : BaseModel
    {
		/// <summary>
		/// 字典Id
		/// </summary>
		public int DicId { get; set; }
    }

	/// <summary>
	/// 修改字典
	/// </summary>
	public class GoodDicUpdateRequest : BaseModel
	{
		/// <summary>
		/// 字典Id
		/// </summary>
		public int DicId { get; set; }
		/// <summary>
		/// 字典名称
		/// </summary>
		public string DicValue { get; set; }
	}

	/// <summary>
	/// 药品列表入参
	/// </summary>
	public class GoodRequest : PageModel
    {
		///<summary> 
		///搜索字段
		///</summary> 
		public string SearchKey { get; set; }

		///<summary> 
		///类型（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
		///</summary> 
		public int GoodType { get; set; }	
		/// <summary>
		/// 分类
		/// </summary>
		public int GoodSort { get; set; }
	}

	/// <summary>
	/// 药品列表入参
	/// </summary>
	public class GoodsRequest : PageModel
	{
		///<summary> 
		///搜索字段
		///</summary> 
		public string SearchKey { get; set; }

		///<summary> 
		///类型（枚举1:西药,3:中药,4:诊疗项目）
		///</summary> 
		public int GoodType { get; set; }
	}

	/// <summary>
	/// 单个药品/项目/材料信息入参
	/// </summary>
	public class GoodOneRequest : BaseModel
    {
		/// <summary>
		/// id
		/// </summary>
		public int GoodId { get; set; }
    }

	/// <summary>
	/// 项目模型
	/// </summary>
	public class GoodItemsRequest : PageModel
	{
		///<summary> 
		///搜索字段
		///</summary> 
		public string SearchKey { get; set; }

		///<summary> 
		///类型（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
		///</summary> 
		[JsonIgnore]
		public int GoodType { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public int GoodSort { get; set; }
	}

	/// <summary>
	/// 项目信息
	/// </summary>
	public class GoodItemModel : BaseModel
    {
		///<summary> 
		///商品ID
		///</summary> 
		public int GoodId { get; set; }
		///<summary> 
		///商品名称
		///</summary> 
		public string GoodName { get; set; }
		/// <summary>
		/// 价格-整数,厘为单位
		/// </summary>
		public double Price { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public string GoodSort { get; set; }
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode { get; set; }
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode { get; set; }
		///<summary> 
		///计价单位
		///</summary> 
		public string SalesUnit { get; set; }
		///<summary> 
		///
		///</summary> 
		public DateTime? CreateTime { get; set; }
	}

	/// <summary>
	/// 项目信息
	/// </summary>
	public class GoodItemOneModel : BaseModel
	{
		///<summary> 
		///商品ID
		///</summary> 
		public int GoodId { get; set; }
		///<summary> 
		///商品名称
		///</summary> 
		public string GoodName { get; set; }
		/// <summary>
		/// 价格-整数,厘为单位
		/// </summary>
		public double Price { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public int GoodSort { get; set; }
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode { get; set; }
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode { get; set; }
		///<summary> 
		///计价单位
		///</summary> 
		public int SalesUnit { get; set; }
		///<summary> 
		///
		///</summary> 
		public DateTime? CreateTime { get; set; }
	}

	/// <summary>
	/// 项目添加模型
	/// </summary>
	public class GoodItemAdd : BaseModel
	{
		///<summary> 
		///商品ID
		///</summary> 
		[JsonIgnore]
		public int GoodId { get; set; }
		///<summary> 
		///商品名称
		///</summary> 
		[Required]
		public string GoodName { get; set; }
		///<summary> 
		///类型（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
		///</summary> 
		[JsonIgnore]
		public int GoodType { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		[Required]
		public int GoodSort { get; set; }
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode { get; set; }
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode { get; set; }
		///<summary> 
		///搜索词根
		///</summary> 
		[JsonIgnore]
		public string SearchKey { get; set; }
		///<summary> 
		///计价单位
		///</summary> 
		[Required]
		public int SalesUnit { get; set; }
		/// <summary>
		/// 价格-整数,厘为单位
		/// </summary>
		public double Price { get; set; }
	}

	/// <summary>
	/// 修改模型
	/// </summary>
	public class GoodItemUpdate : BaseModel
	{
		///<summary> 
		///商品ID
		///</summary> 
		[Required]
		public int GoodId { get; set; }
		///<summary> 
		///商品名称
		///</summary> 
		[Required]
		public string GoodName { get; set; }
		///<summary> 
		///类型（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
		///</summary> 
		[JsonIgnore]
		public int GoodType { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		[Required]
		public int GoodSort { get; set; }
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode { get; set; }
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode { get; set; }
		///<summary> 
		///搜索词根
		///</summary> 
		[JsonIgnore]
		public string SearchKey { get; set; }
		///<summary> 
		///计价单位
		///</summary> 
		[Required]
		public int SalesUnit { get; set; }
		/// <summary>
		/// 价格-整数,厘为单位
		/// </summary>		
		public double Price { get; set; }
	}

	/// <summary>
	/// 材料列表入参模型
	/// </summary>
	public class GoodMaterialRequest : PageModel
	{
		///<summary> 
		///搜索字段
		///</summary> 
		public string SearchKey { get; set; }

		///<summary> 
		///类型（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
		///</summary> 
		[JsonIgnore]
		public int GoodType { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public int GoodSort { get; set; }
	}


	/// <summary>
	/// 材料列表出参
	/// </summary>
	public class GoodMaterialModels : BaseModel
	{
		///<summary> 
		///商品ID
		///</summary> 
		public int GoodId { get; set; }
		///<summary> 
		///商品名称
		///</summary> 
		public string GoodName { get; set; }
		///<summary> 
		///规格
		///</summary> 
		public string Norm { get; set; }
		///<summary> 
		///厂家
		///</summary> 
		public string Factory { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public string GoodSort { get; set; }
		///<summary> 
		///国药准字
		///</summary> 
		public string GoodStandard { get; set; }
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode { get; set; }
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode { get; set; }
		///<summary> 
		///计价单位
		///</summary> 
		public string SalesUnit { get; set; }
		///<summary> 
		///创建时间
		///</summary> 
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 库存量,以计价单位显示,整数
		/// </summary>
		public int Stock { get; set; }
		/// <summary>
		/// 价格,保留3位小数
		/// </summary>		
		public double Price { get; set; }
	}

	/// <summary>
	/// 材料当个出参
	/// </summary>
	public class GoodMaterialModel : BaseModel
	{
		///<summary> 
		///商品ID
		///</summary> 
		public int GoodId { get; set; }
		///<summary> 
		///商品名称
		///</summary> 
		public string GoodName { get; set; }
		///<summary> 
		///规格
		///</summary> 
		public string Norm { get; set; }
		///<summary> 
		///厂家
		///</summary> 
		public int Factory { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public int GoodSort { get; set; }
		///<summary> 
		///国药准字
		///</summary> 
		public string GoodStandard { get; set; }
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode { get; set; }
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode { get; set; }
		///<summary> 
		///计价单位
		///</summary> 
		public int SalesUnit { get; set; }
		///<summary> 
		///创建时间
		///</summary> 
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 库存量,以计价单位显示,整数
		/// </summary>
		public int Stock { get; set; }
		/// <summary>
		/// 价格,保留3位小数
		/// </summary>		
		public double Price { get; set; }
	}

	/// <summary>
	/// 编辑材料
	/// </summary>
	public class GoodMaterialEditModel : BaseModel
	{
		///<summary> 
		///商品ID
		///</summary> 
		public int GoodId { get; set; }
		///<summary> 
		///商品名称
		///</summary> 
		public string GoodName { get; set; }
		///<summary> 
		///规格
		///</summary> 
		public string Norm { get; set; }
		///<summary> 
		///厂家
		///</summary> 
		public int Factory { get; set; }
		///<summary> 
		///分类:自定义,和字典表进行关联
		///</summary> 
		public int GoodSort { get; set; }
		///<summary> 
		///国药准字
		///</summary> 
		public string GoodStandard { get; set; }
		///<summary> 
		///医保编码
		///</summary> 
		public string InsuranceCode { get; set; }
		///<summary> 
		///机构编码-自定义码
		///</summary> 
		public string CustomerCode { get; set; }
		///<summary> 
		///计价单位
		///</summary> 
		public int SalesUnit { get; set; }		
	}

	/// <summary>
	/// 获取药品分类相关字典
	/// </summary>
	public class GoodSortReuqest
    {
		/// <summary>
		/// 字典类型
		/// </summary>
		public string DicType { get; set; }
    }

	/// <summary>
	/// 药品模板下载入参
	/// </summary>
    public class GoodDownFileType : BaseModel
    { 
		/// <summary>
		/// 文件下载类型,1:药品,2:项目,3:材料
		/// </summary>
		public int Type { get; set; }
	}

}