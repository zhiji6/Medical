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
	/// 挂号
	/// </summary>
	public class RegisterSaveModel:BaseModel
    {
		/// <summary>
		/// 患者ID,新患者此值为0
		/// </summary>
		public int PatientId { get; set; }
		/// <summary>
		/// 人员医保编号,医保必传
		/// </summary>
		public string psn_no { get; set; }
		/// <summary>
		/// 医生ID
		/// </summary>
		public int EmployeeId { get; set; }
		/// <summary>
		/// 患者姓名
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 性别,1:男,2:女
		/// </summary>
		public int Sex { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? CSRQ { get; set; }
		/// <summary>
		/// 身份证号
		/// </summary>
		public string SFZH { get; set; }
		/// <summary>
		/// 手机号
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		/// 住址
		/// </summary>
		public string Addr { get; set; }
		/// <summary>
		/// 项目名称
		/// </summary>
		public string GoodsName { get; set; }
		/// <summary>
		/// 价格
		/// </summary>
		public double? GoodsPrice { get; set; }
		/// <summary>
		/// 医保时需要-医保解析接口会返回此值
		/// </summary>
		public string ipt_otp_no { get; set; }
		/// <summary>
		/// 医保时需要-医保解析接口会返回此值
		/// </summary>
		public string mdtrt_id { get; set; }
	}

	/// <summary>
	/// 挂号时选择的医生列表
	/// </summary>
	public class RegisterDoctor : BaseModel
    {
		/// <summary>
		/// 医生ID
		/// </summary>
		public int DoctorId { get; set; }
		/// <summary>
		/// 医生姓名
		/// </summary>
		public string DoctorName { get; set; }		
    }




	///<summary>
	/// Register模型
	/// </summary>
	public class RegisterModel : BaseModel 
	{ 
		///<summary> 
		///挂号id
		///</summary> 
		public long RegisterId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public long PatientId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string psn_no {get;set;} 
		///<summary> 
		///患者姓名
		///</summary> 
		public string Name {get;set;} 
		///<summary> 
		///患者性别
		///</summary> 
		public int Sex {get;set;} 
		///<summary> 
		///
		///</summary> 
		public DateTime? CSRQ {get;set;}
		/// <summary>
		/// 年龄
		/// </summary>
		public string Age
		{
			get
			{
				if(CSRQ == null)
                {
					return "未知";
                }
				var totalMonth = (DateTime.Now.Year - CSRQ?.Year) * 12 - CSRQ?.Month + DateTime.Now.Month;
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
		///
		///</summary> 
		public string SFZH {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string Phone {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string Addr {get;set;} 	
		///<summary> 
		///医生Id
		///</summary> 
		public string DoctorName {get;set;} 
		/// <summary>
		/// 医生Id
		/// </summary>
		public int DoctorId { get; set; }
		///<summary> 
		///
		///</summary> 
		public string DepartmentName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string GoodsName {get;set;} 
		///<summary> 
		///
		///</summary> 
		public long GoodsPrice {get;set;} 
			
		///<summary> 
		///
		///</summary> 
		public DateTime? CreateTime {get;set;}
		///<summary> 
		///是否退号,1未退号,2已退号
		///</summary> 
		public int IsEnable {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsDelete {get;set;} 
		/// <summary>
		/// 医保流水号-非医保字段为空
		/// </summary>
		public string ipt_otp_no { get; set; }
		/// <summary>
		/// 医保挂号id号-非医保字段为空
		/// </summary>
		public string mdtrt_id { get; set; }

	}
	
	///<summary>
	/// Register模型
	/// </summary>
	public class Register1Request : PageModel 
	{ 
		/// <summary>
		/// 搜索关键字
		/// </summary>
		public string SearchKey { get; set; }
		/// <summary>
		/// 医生id
		/// </summary>
		public int DoctorId { get; set; }
		/// <summary>
		/// 根据使用情况筛选挂号,0:查看所有挂号,-1:查看未使用挂号,1:查看已使用挂号
		/// </summary>
		[JsonIgnore]
		public int IsUsed { get; set; }
		/// <summary>
		/// 挂号时间-开始
		/// </summary>
		public DateTime? start { get; set; }
		/// <summary>
		/// 挂号时间结束
		/// </summary>
		public DateTime? end { get; set; }
	}
	
	/// <summary>
	/// 
	/// </summary>
	public class RegisterAdd : BaseModel
    {
		/// <summary>
		/// 患者ID,新患者此值为0
		/// </summary>
		public int PatientId { get; set; }
		/// <summary>
		/// 人员医保编号,医保必传
		/// </summary>
		public string psn_no { get; set; }		
		/// <summary>
		/// 患者姓名
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 性别,1:男,2:女
		/// </summary>
		public int Sex { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? CSRQ { get; set; }
		/// <summary>
		/// 身份证号
		/// </summary>
		public string SFZH { get; set; }
		/// <summary>
		/// 手机号
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		/// 住址
		/// </summary>
		public string Addr { get; set; }
		/// <summary>
		/// 医生
		/// </summary>
		public string DoctorName { get; set; }
		/// <summary>
		/// 科室
		/// </summary>
		public string DepartmentName { get; set; }
		/// <summary>
		/// 项目名称
		/// </summary>
		public string GoodsName { get; set; }
		/// <summary>
		/// 价格
		/// </summary>
		public double? GoodsPrice { get; set; }
		/// <summary>
		/// 医保时需要-医保解析接口会返回此值
		/// </summary>
		public string ipt_otp_no { get; set; }
		/// <summary>
		/// 医保时需要-医保解析接口会返回此值
		/// </summary>
		public string mdtrt_id { get; set; }
		/// <summary>
		/// 搜索
		/// </summary>
		public string SearchKey { get; set; }
	}

	/// <summary>
	/// 医保挂号解析返回
	/// </summary>
	public class RegisterYBReturn
    {
		/// <summary>
		/// 就诊ID:医保返回唯一流水
		/// </summary>
		public string mdtrt_id { get; set; }
		/// <summary>
		/// 人员编号
		/// </summary>
		public string psn_no { get; set; }
		/// <summary>
		/// 住院/门诊号-院内唯一流水
		/// </summary>
		public string ipt_otp_no { get; set; }

	}

	/// <summary>
	/// 挂号入参模型
	/// </summary>
	public class RegisterBackModel : BaseModel
    {
		/// <summary>
		/// 挂号记录id-普通退号使用
		/// </summary>
		public int RegisterId { get; set; }

		/// <summary>
		/// 医保流水号-医保退号使用
		/// </summary>
		public string mdtrt_id { get; set; }

	}
} 