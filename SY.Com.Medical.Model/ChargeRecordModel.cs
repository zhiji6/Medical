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
/// ChargeRecord模型
/// </summary>
public class ChargeRecordModel : BaseModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public int ChargeRecordId {get;set;} 
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
		///<summary> 
		///
		///</summary> 
		public DateTime? CreateTime {get;set;} 
		/// <summary>
		/// 收费人id
		/// </summary>
		public int Cashier { get; set; }
		/// <summary>
		/// 收费人姓名
		/// </summary>
		public string CashierName { get; set; }
		///<summary> 
		///
		///</summary> 
		public int IsEnable {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsDelete {get;set;} 
	}
	
///<summary>
/// ChargeRecord模型
/// </summary>
public class ChargeRecordRequest : PageModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public int ChargeRecordId {get;set;} 
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
		///<summary> 
		///
		///</summary> 
		public DateTime? CreateTime {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsEnable {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsDelete {get;set;} 
	}
	
///<summary>
/// ChargeRecord模型
/// </summary>
public class ChargeRecordAdd : BaseModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public int ChargeRecordId {get;set;} 
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
		///<summary> 
		///
		///</summary> 
		public DateTime? CreateTime {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsEnable {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsDelete {get;set;} 
	}
	
///<summary>
/// ChargeRecord模型
/// </summary>
public class ChargeRecordUpdate : BaseModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public int ChargeRecordId {get;set;} 
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
		///<summary> 
		///
		///</summary> 
		public DateTime? CreateTime {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsEnable {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsDelete {get;set;} 
	}
	
///<summary>
/// ChargeRecord模型
/// </summary>
public class ChargeRecordDelete : BaseModel 
	{ 
		///<summary> 
		///
		///</summary> 
		public int ChargeRecordId {get;set;} 
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
		///<summary> 
		///
		///</summary> 
		public DateTime? CreateTime {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsEnable {get;set;} 
		///<summary> 
		///
		///</summary> 
		public int IsDelete {get;set;} 
	}
	
} 