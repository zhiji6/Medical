using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model
{
	/// <summary>
	/// 挂号打印模板入参
	/// </summary>
	public class PrintRegisterRequestModel : BaseModel
	{
		/// <summary>
		/// 挂号Id
		/// </summary>
		public int RegisterId { get; set; }
	}

	/// <summary>
	/// 挂号打印模板输出
	/// </summary>
	public class PrintRegisterResponseModel : BaseModel
	{
		/// <summary>
		/// 模板路径-相对
		/// </summary>
		public string ViewPath { get; set; }
		/// <summary>
		/// 挂号Id
		/// </summary>
		public int RegisterId { get; set; }
		///<summary> 
		///人员系统Id
		///</summary> 
		public int PatientId { get; set; }
		///<summary> 
		///医保人员编号
		///</summary> 
		public string psn_no { get; set; }
		///<summary> 
		///患者姓名
		///</summary> 
		public string Name { get; set; }
		///<summary> 
		///患者性别
		///</summary> 
		public int Sex { get; set; }
		/// <summary>
		/// 性别描述
		/// </summary>
		public string SexDesc { get {
				if (Sex == 0) return "男";
				return "女";
			} }
		///<summary> 
		///出生日期
		///</summary> 
		public DateTime? CSRQ { get; set; }
		///// <summary>
		///// 年龄
		///// </summary>
		//public string Age { get {
		//		if (CSRQ == null) return "不详";
		//		int diffyear = DateTime.Now.Year - CSRQ.Value.Year;
		//		double diffday = (DateTime.Now - CSRQ.Value).TotalDays;
		//		int totalday = 0;
		//		for(int i =DateTime.Now.Year;i<= CSRQ.Value.Year; i++)
		//              {
		//			//润年
		//			if( (i % 4 == 0 && i % 100 != 0 ) || i % 400 == 0)
		//                  {
		//				totalday += 366;
		//                  }
		//                  else
		//                  {
		//				totalday += 365;
		//                  }
		//              }
		//		if(totalday < diffday)
		//              {
		//			return (diffyear - 1).ToString();
		//              }
		//              else
		//              {
		//			return diffyear.ToString();
		//              }
		//	} }

		/// <summary>
		/// 年龄
		/// 使用出生日期计算
		/// 大于等于24个月，只显示岁，岁用总月数整除12向下取整
		/// 小于24个月,显示岁和月，岁依然是月数整除12向下取整，月为月数对12求余        
		/// 小于6月的显示天
		/// </summary>
		public string Age
		{
			get
			{
				if (CSRQ == null) return "未知";
				var totalMonth = (DateTime.Now.Year - CSRQ.Value.Year) * 12 - CSRQ.Value.Month + DateTime.Now.Month;
				if (totalMonth < 2)
				{
					return $"{(DateTime.Now - CSRQ.Value).Days}天";
				}
				else if (totalMonth >= 24)
				{
					return $"{totalMonth / 12}岁";
				}
				else
				{
					if (totalMonth >= 12)
					{
						return $"{totalMonth / 12}岁{totalMonth % 12}月";
					}
					else
					{
						return $"{totalMonth}月";
					}
				}
			}
		}

		///<summary> 
		///省份证号
		///</summary> 
		public string SFZH { get; set; }
		///<summary> 
		///电话号码
		///</summary> 
		public string Phone { get; set; }
		///<summary> 
		///地址
		///</summary> 
		public string Addr { get; set; }

		///<summary> 
		///医生姓名
		///</summary> 
		public string DoctorName { get; set; }
		///<summary> 
		///科室名称
		///</summary> 
		public string DepartmentName { get; set; }
		///<summary> 
		///项目名称
		///</summary> 
		public string GoodsName { get; set; }
		///<summary> 
		///金额
		///</summary> 
		public long GoodsPrice { get; set; }
		/// <summary>
		/// 医保ipt_otp_no
		/// </summary>
		public string ipt_otp_no { get; set; }
		/// <summary>
		/// 医保mdtrt_id
		/// </summary>
		public string mdtrt_id { get; set; }
		/// <summary>
		/// 是否使用1:已使用,-1未使用
		/// </summary>
		public int IsUsed { get; set; }
		/// <summary>
		/// 搜索字段
		/// </summary>
		public string SearchKey { get; set; }
		/// <summary>
		/// 挂号时间
		/// </summary>
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 机构名称
		/// </summary>
		public string TenantName { get; set; }

		/// <summary>
		/// 临时凑数
		/// </summary>
		public List<PrintTemp> Temp { get; set; }
	}

	/// <summary>
	/// 处方打印入参
	/// </summary>
	public class PrintPrescriptionRequestModel : BaseModel
	{
		/// <summary>
		/// 门诊Id
		/// </summary>
		public int OutpatientId { get; set; }
		/// <summary>
		/// 中药处方:1,西药处方:2,项目处方:3,治疗单:4
		/// </summary>
		public int Type { get; set; }
	}
	/// <summary>
	/// 处方打印模板输出
	/// </summary>
	public class PrintPrescriptionResponseModel : BaseModel
	{
		/// <summary>
		/// 模板路径-相对
		/// </summary>
		public string ViewPath { get; set; }
		/// <summary>
		/// 具体数据
		/// </summary>
		public OutpatientStructure Data { get; set; }
	}

	/// <summary>
	/// 打印收费退费输入
	/// </summary>
	public class ChargeRecordRequestModel : BaseModel
	{
		/// <summary>
		/// 门诊Id
		/// </summary>
		public int OutPatientId { get; set; }
		/// <summary>
		/// 类型,门诊收费,门诊退费
		/// </summary>
		public string ChargeType { get; set; }

	}

	/// <summary>
	/// 打印收费退费输出
	/// </summary>
	public class ChargeRecordResponseModel : BaseModel
	{
		/// <summary>
		/// 模板路径
		/// </summary>
		public string ViewPath { get; set; }
		/// <summary>
		/// 机构名称
		/// </summary>
		public string TenantName { get; set; }
		/// <summary>
		/// 机构编码
		/// </summary>
		public string TenantCode { get; set; }
		///<summary> 
		///收费记录Id
		///</summary> 
		public int ChargeRecordId { get; set; }
		///<summary> 
		/// 患者Id
		///</summary> 
		public int PatientId { get; set; }
		///<summary> 
		/// 挂号Id
		///</summary> 
		public int RegisterId { get; set; }
		///<summary> 
		///门诊Id		
		///</summary> 
		public int SeeDoctorId { get; set; }
		///<summary> 
		///金额
		///</summary> 
		public long Price { get; set; }
		///<summary> 
		///类型
		///</summary> 
		public string ChargeType { get; set; }
		///<summary> 
		///医保支付
		///</summary> 
		public long PayYB { get; set; }
		///<summary> 
		///现金支付
		///</summary> 
		public long PayCash { get; set; }
		///<summary> 
		///微信支付
		///</summary> 
		public long PayWx { get; set; }
		///<summary> 
		///银行卡支付
		///</summary> 
		public long PayBank { get; set; }
		///<summary> 
		///支付宝支付
		///</summary> 
		public long PayAli { get; set; }

		/// <summary>
		/// 医保mdtrt_id值
		/// </summary>
		public string mdtrt_id { get; set; }
		/// <summary>
		/// 医保结算号
		/// </summary>
		public string setl_id { get; set; }
	}

	/// <summary>
	/// 打印病历输入
	/// </summary>
	public class CaseBookRequestModel : BaseModel
	{
		/// <summary>
		/// 病历Id
		/// </summary>
		public int CaseBookId { get; set; }
	}

	/// <summary>
	/// 打印病历输出
	/// </summary>
	public class CaseBookResponseModel
	{
		/// <summary>
		/// 打印模板路径
		/// </summary>
		public string ViewPath { get; set; }

		/// <summary>
		/// 主诉    
		/// </summary>
		public string Complaint { get; set; }

		/// <summary>
		/// 诊断    
		/// </summary>
		public string Diagnosis { get; set; }

		/// <summary>
		/// 疾病
		/// </summary>
		public string Disease { get; set; }

		/// <summary>
		/// 门诊入诊日期    
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 医嘱    
		/// </summary>
		public string CaseOrder { get; set; }

		/// <summary>
		/// 现病史    
		/// </summary>
		public string PastCase { get; set; }

		/// <summary>
		/// 既往史    
		/// </summary>
		public string HistoryCase { get; set; }

		/// <summary>
		/// 体格检查    
		/// </summary>
		public string Physical { get; set; }

		/// <summary>
		/// 治疗意见    
		/// </summary>
		public string Opinions { get; set; }

		/// <summary>
		/// 部位    
		/// </summary>
		public string Place { get; set; }

		/// <summary>
		/// 医生姓名    
		/// </summary>
		public string DoctorName { get; set; }

		/// <summary>
		/// 科室    
		/// </summary>
		public string DepartmentName { get; set; }



		/// <summary>
		/// 患者姓名    
		/// </summary>    
		public string PatientName { get; set; }


		/// <summary>
		/// 性别    
		/// </summary>
		public int Sex { get; set; }

		/// <summary>
		/// 性别描述
		/// </summary>
		public string SexDesc
		{
			get
			{
				if (Sex == 1) return "男";
				return "女";
			}
		}

		/// <summary>
		/// 患者电话
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime CSRQ { get; set; }
		/// <summary>
		/// 年龄
		/// 使用出生日期计算
		/// 大于等于24个月，只显示岁，岁用总月数整除12向下取整
		/// 小于24个月,显示岁和月，岁依然是月数整除12向下取整，月为月数对12求余        
		/// 小于6月的显示天
		/// </summary>
		public string Age
		{
			get
			{
				var totalMonth = (DateTime.Now.Year - CSRQ.Year) * 12 - CSRQ.Month + DateTime.Now.Month;
				if (totalMonth < 2)
				{
					return $"{(DateTime.Now - CSRQ).Days}天";
				}
				else if (totalMonth >= 24)
				{
					return $"{totalMonth / 12}岁";
				}
				else
				{
					if (totalMonth >= 12)
					{
						return $"{totalMonth / 12}岁{totalMonth % 12}月";
					}
					else
					{
						return $"{totalMonth}月";
					}
				}
			}
		}

		/// <summary>
		/// 身份证
		/// </summary>
		public string SFZ { get; set; }

		/// <summary>
		/// 打印控件必须要有一个数组
		/// </summary>
		public List<PrintTemp> Temp { get; set; }

	}

	/// <summary>
	/// 打印临时凑数
	/// </summary>
	public class PrintTemp
    {
		/// <summary>
		/// 
		/// </summary>
		public string tempname { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int tempid { get; set; }
    }

	/// <summary>
	/// 综合打印数据
	/// </summary>
	public class CombinePrintDataModel
    {
		/// <summary>
		/// 打印模板路径
		/// </summary>
		public string ViewPath { get; set; }
		/// <summary>
		/// 机构名称
		/// </summary>
		public string TenantName { get; set; }
		/// <summary>
		/// 结构编码
		/// </summary>
		public string TenantCode { get; set; }
		/// <summary>
		/// 患者姓名
		/// </summary>
		public string PatientName { get; set; }
		/// <summary>
		/// 患者医保号码
		/// </summary>
		public string psn_no { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		public int Sex { get; set; }
		/// <summary>
		/// 性别描述
		/// </summary>
		public string SexDesc
		{
			get
			{
				if (Sex == 1) return "男";
				return "女";
			}
		}
		///<summary> 
		///出生日期
		///</summary> 
		public DateTime? CSRQ { get; set; }
		/// <summary>
		/// 年龄
		/// 使用出生日期计算
		/// 大于等于24个月，只显示岁，岁用总月数整除12向下取整
		/// 小于24个月,显示岁和月，岁依然是月数整除12向下取整，月为月数对12求余        
		/// 小于6月的显示天
		/// </summary>
		public string Age
		{
			get
			{
				if (CSRQ == null) return "未知";
				var totalMonth = (DateTime.Now.Year - CSRQ.Value.Year) * 12 - CSRQ.Value.Month + DateTime.Now.Month;
				if (totalMonth < 2)
				{
					return $"{(DateTime.Now - CSRQ.Value).Days}天";
				}
				else if (totalMonth >= 24)
				{
					return $"{totalMonth / 12}岁";
				}
				else
				{
					if (totalMonth >= 12)
					{
						return $"{totalMonth / 12}岁{totalMonth % 12}月";
					}
					else
					{
						return $"{totalMonth}月";
					}
				}
			}
		}
		/// <summary>
		/// 电话
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		/// 地址
		/// </summary>
		public string Addr { get; set; }
		/// <summary>
		/// 身份证
		/// </summary>
		public string SFZ { get; set; }
		/// <summary>
		/// 科室名称
		/// </summary>
		public string DepartmentName { get; set; }
		/// <summary>
		/// 医生名称
		/// </summary>
		public string DoctorName { get; set; }
		/// <summary>
		/// 医生编码
		/// </summary>
		public string DoctorCode { get; set; }
		/// <summary>
		/// 诊断
		/// </summary>
		public string Diagnosis { get; set; }
		/// <summary>
		/// 门诊时间
		/// </summary>
		public DateTime? OutpatienTime { get; set; }
		/// <summary>
		/// 医保同名字段
		/// </summary>
		public string mdtrt_id { get; set; }
		/// <summary>
		/// 医保同名字段
		/// </summary>
		public string chrg_bchno { get; set; }
		/// <summary>
		/// 医保同名字段
		/// </summary>
		public string setl_id { get; set; }
		/// <summary>
		/// 医保余额
		/// </summary>
		public double Balc { get; set; }
		/// <summary>
		/// 医保支付
		/// </summary>
		public double PayYB { get; set; }
		/// <summary>
		/// 实收
		/// </summary>
		public double Cost { get; set; }
		/// <summary>
		/// 医药实收
		/// </summary>
		public double WestCost { get; set; }
		/// <summary>
		/// 中药实收
		/// </summary>
		public double EastCost { get; set; }
		/// <summary>
		/// 项目实收
		/// </summary>
		public double ProjCost { get; set;}
		/// <summary>
		/// 收费人名称
		/// </summary>
		public string CashierName { get; set; }
		/// <summary>
		/// 收费日期
		/// </summary>
		public DateTime? PayTime { get; set; }
		/// <summary>
		/// 物料
		/// </summary>
		public List<CombinePrintGoodModel> Goods { get; set; }
		/// <summary>
		/// 中药副数
		/// </summary>
		public int Pair { get; set; }

	}

	/// <summary>
	/// 综合打印数据之物料
	/// </summary>
	public class CombinePrintGoodModel
    {
		/// <summary>
		/// 处方种类名称
		/// </summary>
		public string PreName { get; set; }
		/// <summary>
		/// 物料名称
		/// </summary>
		public string GoodsName { get; set; }
		/// <summary>
		/// 医保编码
		/// </summary>
		public string InsuranceCode { get; set; }
		/// <summary>
		/// 自定义编码
		/// </summary>
		public string CustomerCode { get; set; }
		/// <summary>
		/// 用法
		/// </summary>
		public string Usage { get; set; }
		/// <summary>
		/// 单次用量
		/// </summary>
		public int Single { get; set; }
		/// <summary>
		/// 总量
		/// </summary>
		public int Total { get; set; }
		/// <summary>
		///  单位
		/// </summary>
		public string Unit { get; set; }
		/// <summary>
		/// 部位
		/// </summary>
		public string Place { get; set; }
		/// <summary>
		/// 单价
		/// </summary>
		public double Price { get; set; }
	}
}
