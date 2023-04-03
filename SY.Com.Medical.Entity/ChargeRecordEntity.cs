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
    [DB_Table("ChargeRecords")]
    [DB_Key("ChargeRecordId")]
    public class ChargeRecordEntity : BaseEntity 
	{ 
		///<summary> 
		///
		///</summary> 
		[DB_Key("ChargeRecordId")]
		public int ChargeRecordId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int TenantId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int PatientId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int RegisterId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int SeeDoctorId {get;set;} 
		///<summary> 
		///
		///</summary> 
		public long Price {get;set;} 
		///<summary> 
		///
		///</summary> 
		public string ChargeType {get;set;} 
		///<summary> 
		///
		///</summary> 
		public long PayYB {get;set;} 
		/// <summary>
		/// 统筹支付
		/// </summary>
		public long HifpPay { get; set; }
		///<summary> 
		///
		///</summary> 
		public long PayCash {get;set;} 
		///<summary> 
		///
		///</summary> 
		public long PayWx {get;set;} 
		///<summary> 
		///
		///</summary> 
		public long PayBank {get;set;} 
		///<summary> 
		///
		///</summary> 
		public long PayAli {get;set;} 
		/// <summary>
		/// 收银员id
		/// </summary>
		public int Cashier { get; set; }
		/// <summary>
		/// 收银员名称
		/// </summary>
		public string CashierName { get; set; }
	}
} 