using SY.Com.Medical.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SY.Com.Medical.Model
{

	/// <summary>
	/// 获取门诊详情入参
	/// </summary>
	public class OutpatientStructureRequest : BaseModel
    {
		/// <summary>
		/// 门诊ID
		/// </summary>
		public int OutpatientId { get; set; }
    }

	/// <summary>
	/// 门诊复合结构-查询
	/// </summary>
	public class OutpatientStructure
	{
		/// <summary>
		/// 门诊Id
		/// </summary>
		public int OutpatientId { get; set; }
		/// <summary>
		/// 机构Id
		/// </summary>
		public int TenantId { get; set; }
		/// <summary>
		/// 医保门诊号--非医保为空
		/// </summary>
		public string mdtrt_id { get; set; }
		/// <summary>
		/// 医保流水号--非医保为空
		/// </summary>
		public string chrg_bchno { get; set; }
		/// <summary>
		/// 医保结算号--非医保为空
		/// </summary>
		public string setl_id { get; set; }
		/// <summary>
		/// 挂号Id-无挂号直接门诊此值为0
		/// </summary>
		public int RegisterId { get; set; }
		/// <summary>
		/// 处方数量
		/// </summary>
		public int PrescriptionCount { get; set; }
		/// <summary>
		/// 是否支付,1:支付,-1:未支付
		/// </summary>
		public int IsPay { get; set; }
		/// <summary>
		/// 是否退款,1:未退款,-1:退款
		/// </summary>
		public int IsBack { get; set; }
		/// <summary>
		/// 医保账户支付金额-自费此值为0
		/// </summary>
		public double PayYB { get; set; }
		/// <summary>
		/// 自费支付金额
		/// </summary>
		public double PayCash { get; set; }
		/// <summary>
		/// 医保账户余额--支付前
		/// </summary>
		public double PayYBBefor { get; set; }
		/// <summary>
		/// 医保账户余额--支付后
		/// </summary>
		public double PayYBAfter { get; set; }
		/// <summary>
		/// 门诊总费用
		/// </summary>
		public double Cost { get; set; }
		/// <summary>
		/// 实收
		/// </summary>
		public double RealCost { get; set; }
		/// <summary>
		/// 优惠
		/// </summary>
		public double DiscountCost { get; set; }
		/// <summary>
		/// 患者信息
		/// </summary>
		public PatientStructure Patient { get; set; }
		/// <summary>
		/// 医生信息
		/// </summary>
		public DoctorStructure Doctor { get; set; }
		/// <summary>
		/// 病历
		/// </summary>
		public CaseBookStructure CaseBook { get; set; }
		/// <summary>
		/// 处方
		/// </summary>
		public List<PrescriptionStructure> Prescriptions { get; set; }
	}

	/// <summary>
	/// 门诊复合结构-添加
	/// </summary>
	public class OutpatientAddStructure : BaseModel
    {
		/// <summary>
		/// 门诊Id
		/// </summary>
		public int OutpatientId { get; set; }
		/// <summary>
		/// 医保门诊号--非医保为空
		/// </summary>
		public string mdtrt_id { get; set; }
		/// <summary>
		/// 挂号Id-无挂号直接门诊此值为0
		/// </summary>
		public int RegisterId { get; set; }
		/// <summary>
		/// 医生Id
		/// </summary>
		public int DoctorId { get; set; }
		/// <summary>
		/// 患者信息
		/// </summary>
		public PatientStructure Patient { get; set; }
		/// <summary>
		/// 病历
		/// </summary>
		public CaseBookStructure CaseBook { get; set; }
		/// <summary>
		/// 处方
		/// </summary>
		public List<PrescriptionAddStructure> Prescriptions { get; set; }
	}

	/// <summary>
	/// 门诊内患者结构
	/// </summary>
	public class PatientStructure
	{
		/// <summary>
		/// 租户Id
		/// </summary>
		public int TenantId { get; set; }
		/// <summary>
		/// 患者ID
		/// </summary>
		public int PatienId { get; set; }
		/// <summary>
		/// 患者姓名
		/// </summary>
		public string PatientName { get; set; }
		/// <summary>
		/// 手机号
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? CSRQ { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		public int Sex { get; set; }
		/// <summary>
		/// 身份证
		/// </summary>
		public string SFZ { get; set; }
		/// <summary>
		/// 地址
		/// </summary>
		public string Addr { get; set; }
		/// <summary>
		/// 医保编号
		/// </summary>
		public string psn_no { get; set; }
	}

	/// <summary>
	/// 门诊内医生结构
	/// </summary>
	public class DoctorStructure
	{
		/// <summary>
		/// 医生id
		/// </summary>
		public int EmployeeId { get; set; }
		/// <summary>
		/// 医生姓名
		/// </summary>
		public string EmployeeName { get; set; }
		/// <summary>
		/// 部门
		/// </summary>
		public string Department { get; set; }
		/// <summary>
		/// 医保编码
		/// </summary>
		public string YBCode { get; set; }
	}
	/// <summary>
	/// 门诊内病历结构
	/// </summary>
	public class CaseBookStructure
	{
		/// <summary>
		/// 租户Id
		/// </summary>
		public int TenantId { get; set; }
		/// <summary>
		/// 病历id
		/// </summary>
		public int CaseBookId { get; set; }
		/// <summary>
		/// 门诊Id
		/// </summary>
		public int OutPatientId { get; set; }
		/// <summary>
		/// 患者Id
		/// </summary>
		public int PatientId { get; set; }
		/// <summary>
		/// 医生Id
		/// </summary>
		public int DoctorId { get; set; }
		/// <summary>
		/// 科室Id
		/// </summary>
		public int DepartmentId { get; set; }
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
		/// 疾病编码
		/// </summary>
		public string DiseaseCode { get; set; }
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
		/// 牙位
		/// </summary>
		public string Tooth { get; set; }
		/// <summary>
		/// 部位
		/// </summary>
		public string Place { get; set; }

	}

	/// <summary>
	/// 门诊内处方结构
	/// </summary>
	public class PrescriptionStructure
	{
		/// <summary>
		/// 处方序号
		/// </summary>
		public int PreNo { get; set; }
		/// <summary>
		/// 处方名称
		/// </summary>
		public string PreName { get; set; }
		/// <summary>
		/// 费用
		/// </summary>
		public double Cost { get; set; }

		/// <summary>
		/// 副数-中药处方使用
		/// </summary>
		public int Pair { get; set; }
		/// <summary>
		/// 物品明细
		/// </summary>
		public List<PrescriptionDetailStructure> Details { get; set; }

	}

	/// <summary>
	/// 处方明细-物品明细
	/// </summary>
	public class PrescriptionDetailStructure
	{
		/// <summary>
		/// 药物或项目id
		/// </summary>
		public int GoodsId { get; set; }
		/// <summary>
		/// 药物或项目名称
		/// </summary>
		public string GoodsName { get; set; }
		/// <summary>
		/// 物品数量
		/// </summary>
		public int GoodsNum { get; set; }
		/// <summary>
		/// 规格
		/// </summary>
		public string GoodsNorm { get; set; }
		/// <summary>
		/// 单价
		/// </summary>
		public double GoodsPrice { get; set; }
		/// <summary>
		/// 总价
		/// </summary>
		public double GoodsCost { get; set; }
		/// <summary>
		/// 用法
		/// </summary>
		public string GoodsUsage { get; set; }
		/// <summary>
		/// 频度
		/// </summary>
		public string GoodsEveryDay { get; set; }
		/// <summary>
		/// 天数
		/// </summary>
		public int GoodsDays { get; set; }
		/// <summary>
		/// 单位
		/// </summary>
		public string GoodsSalesUnit { get; set; }
		/// <summary>
		/// 部位或者牙位
		/// </summary>
		public string Place { get; set; }
		/// <summary>
		/// 医保编码
		/// </summary>
		public string InsuranceCode { get; set; }
		/// <summary>
		/// 机构编码
		/// </summary>
		public string CustomerCode { get; set; }
	}

	/// <summary>
	/// 门诊内处方结构
	/// </summary>
	public class PrescriptionAddStructure
	{
		/// <summary>
		/// 处方序号
		/// </summary>
		public int PreNo { get; set; }
		/// <summary>
		/// 处方名称
		/// </summary>
		public string PreName { get; set; }

		/// <summary>
		/// 副数-中药处方使用
		/// </summary>
		public int Pair { get; set; }
		/// <summary>
		/// 物品明细
		/// </summary>
		public List<PrescriptionDetailAddStructure> Details { get; set; }

	}

	/// <summary>
	/// 处方明细-物品明细
	/// </summary>
	public class PrescriptionDetailAddStructure
	{
		/// <summary>
		/// 药物或项目id
		/// </summary>
		public int GoodsId { get; set; }
		/// <summary>
		/// 药物或项目名称
		/// </summary>
		public string GoodsName { get; set; }
		/// <summary>
		/// 物品数量
		/// </summary>
		public int GoodsNum { get; set; }
		/// <summary>
		/// 规格
		/// </summary>
		public string GoodsNorm { get; set; }
		/// <summary>
		/// 单价
		/// </summary>
		public double GoodsPrice { get; set; }
		/// <summary>
		/// 用法
		/// </summary>				
		public int GoodsUsage { get; set; }//public string GoodsUsage { get; set; }
		/// <summary>
		/// 频度
		/// </summary>
		public int GoodsEveryDay { get; set; }
		/// <summary>
		/// 天数
		/// </summary>
		public int GoodsDays { get; set; }
		/// <summary>
		/// 单位
		/// </summary>
		public string GoodsSalesUnit { get; set; }
		/// <summary>
		/// 部位或者牙位
		/// </summary>
		public string Place { get; set; }
		/// <summary>
		/// 医保编码
		/// </summary>
		public string InsuranceCode { get; set; }
		/// <summary>
		/// 机构编码
		/// </summary>
		public string CustomerCode { get; set; }
	}

	///<summary>
	/// Outpatient模型
	/// </summary>
	public class OutpatientListModel : BaseModel 
	{ 
		///<summary> 
		///门诊Id
		///</summary> 
		public int OutpatientId {get;set;} 
		/// <summary>
		/// 患者姓名
		/// </summary>
		public string PatientName { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		public string Sex { get; set; }
		/// <summary>
		/// 年龄
		/// </summary>
		public string Age { get; set; }
		/// <summary>
		/// 手机
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		/// 医生姓名
		/// </summary>
		public string DoctorName { get; set; }
		/// <summary>
		/// 费用
		/// </summary>
		public double Cost { get; set; }
		/// <summary>
		/// 处方数量
		/// </summary>
		public int PrescriptionCount { get; set; }
		/// <summary>
		/// 挂号Id
		/// </summary>
		public int RegisterId { get; set; }
		/// <summary>
		/// 就诊日期
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 收银员Id
		/// </summary>
		public int Cashier { get; set; }
		/// <summary>
		/// 收银员名称
		/// </summary>
		public string CashierName { get; set; }
		
	}
	
	/// <summary>
	/// Outpatient搜索模型
	/// </summary>
	public class OutpatientSearchModel:PageModel
    {
		/// <summary>
		/// 搜索词
		/// </summary>
		public string searchKey { get; set; } 
		/// <summary>
		/// 搜索-开始日期
		/// </summary>
		public DateTime? start { get; set; }
		/// <summary>
		/// 搜索-结束日期
		/// </summary>
		public DateTime? end { get; set; }

	}

	/// <summary>
	/// Outpatient搜索模型
	/// </summary>
	public class OutpatientSearch2Model : PageModel
	{
		/// <summary>
		/// 搜索词
		/// </summary>
		public string searchKey { get; set; }
		/// <summary>
		/// 搜索-开始日期
		/// </summary>
		public DateTime? start { get; set; }
		/// <summary>
		/// 搜索-结束日期
		/// </summary>
		public DateTime? end { get; set; }
		/// <summary>
		/// 医生ID
		/// </summary>
		public int DoctorId { get; set; }

	}

	/// <summary>
	/// 结算入参
	/// </summary>
	public class OutpatientChargeModel:BaseModel
    {
		/// <summary>
		/// 门诊ID
		/// </summary>
		public int OutpatientId { get; set; }
		/// <summary>
		/// 医保结算ID-非医保为空
		/// </summary>
		public string setl_id { get; set; }
		/// <summary>
		/// 医保余额-非医保为0
		/// </summary>
		public double Balc { get; set; }
		/// <summary>
		/// 实收
		/// </summary>
		public double Cost { get; set; }
		/// <summary>
		/// 医保实收-非医保为0
		/// </summary>
		public double YBCost { get; set; }
		/// <summary>
		/// 自费实收-非医保时此值需要和Cost相等,医保时根据医保接口返回设置
		/// </summary>
		public double CashCost { get; set; }
		/// <summary>
		/// 自费支付方式0:现金,1:微信,2:支付宝,3:银行卡,4:医保卡标记
		/// </summary>
		public int PayWay { get; set; }
		/// <summary>
		/// 收银员Id(EmployeeId)
		/// </summary>
		public int Cashier { get; set; }


    }


} 