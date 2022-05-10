using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.BLL.Platform;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Extension;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SY.Com.Medical.BLL.Clinic
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public class Register 
	{
		private RegisterRepository db;
		private Patient patientbll = new Patient();
		private Employee employeebll = new Employee();
		private SYBbll ybbll = new SYBbll();
		private ChargeRecord chargebll = new ChargeRecord();
		public Register()
		{
			db = new RegisterRepository();
		}

		/// <summary>
		/// 挂号,先做系统挂号
		/// 如果是医保流程,则生成医保挂号的报文并返回,且设置
		/// </summary>
		/// <param name="mod"></param>
		/// <returns></returns>
		public int Save(RegisterSaveModel mod)
        {
			int result = 0;
			//首先保存或更新患者信息
			if(mod.PatientId == 0)
            {
				PatientAdddto padd = new PatientAdddto();
				padd.psn_no = mod.psn_no;
				padd.PatientName = mod.Name;
				padd.Sex = mod.Sex;
				padd.CSRQ = mod.CSRQ;
				padd.SFZ = mod.SFZH;
				padd.Phone = mod.Phone;
				padd.Addr = mod.Addr;
				padd.TenantId = mod.TenantId;
				int patientid = patientbll.add(padd);
				mod.PatientId = patientid;

			}
            else
            {
				PatientUpdatedto pupd = new PatientUpdatedto();
				pupd.PatientId = mod.PatientId;
				pupd.psn_no = mod.psn_no;
				pupd.PatientName = mod.Name;
				pupd.Sex = mod.Sex;
				pupd.CSRQ = mod.CSRQ;
				pupd.SFZ = mod.SFZH;
				pupd.Phone = mod.Phone;
				pupd.Addr = mod.Addr;
				pupd.TenantId = mod.TenantId;
				patientbll.update(pupd);
			}
			//获取医生信息和科室信息
			if(mod.EmployeeId == 0)
            {
				throw new MyException("医生信息不能为空");
            }
			var employeemodel = employeebll.getEmployee(mod.EmployeeId);
			


			//保存挂号信息

			RegisterAdd radd = new RegisterAdd();
			radd = mod.Mapping<RegisterAdd>();
			radd.DoctorName = employeemodel.EmployeeName;
			radd.DepartmentName = employeemodel.Departments;
			radd.SearchKey = mod.Name.GetPinYin() + mod.Phone + mod.psn_no + mod.SFZH;
			var entity = radd.DtoToEntity<RegisterEntity>();
			entity.IsUsed = -1;			
			var registerid = db.Create(entity);

			//保存收费记录
			ChargeRecordEntity chargeentity = new ChargeRecordEntity();
			chargeentity.TenantId = mod.TenantId;
			chargeentity.PatientId = mod.PatientId;
			chargeentity.RegisterId = registerid;
			chargeentity.SeeDoctorId = 0;
			chargeentity.Price = Convert.ToInt64( mod.GoodsPrice * 1000);
			chargeentity.ChargeType = "挂号";
			if(!string.IsNullOrEmpty(mod.mdtrt_id))
            {
				chargeentity.PayYB = chargeentity.Price;
            }
            else
            {
				chargeentity.PayCash = chargeentity.Price;
			}
			chargeentity.PayWx = 0;
			chargeentity.PayBank = 0;
			chargeentity.PayAli = 0;
			int chargeid = chargebll.add(chargeentity);
			result = registerid;
			return result;
        }

		/// <summary>
		/// 退号
		/// </summary>
		/// <param name="tenantId"></param>
		/// <param name="registerId"></param>
		/// <returns></returns>
		public int Back(int tenantId,int registerId)
        {
			int result = 0;
			var i = db.Back(tenantId, registerId);
			var entity = db.Get(registerId);
			//保存退费记录
			ChargeRecordEntity chargeentity = new ChargeRecordEntity();
			chargeentity.TenantId = tenantId;
			chargeentity.PatientId = entity.PatientId;
			chargeentity.RegisterId = registerId;
			chargeentity.SeeDoctorId = 0;
			chargeentity.Price = entity.GoodsPrice;
			chargeentity.ChargeType = "退号";
			if (!string.IsNullOrEmpty(entity.mdtrt_id))
			{
				chargeentity.PayYB = -chargeentity.Price;
			}
			else
			{
				chargeentity.PayCash = -chargeentity.Price;
			}
			chargeentity.PayWx = 0;
			chargeentity.PayBank = 0;
			chargeentity.PayAli = 0;
			int chargeid = chargebll.add(chargeentity);
			return result;
        }

		/// <summary>
		/// 获取医生下拉框
		/// </summary>
		/// <param name="tenantId"></param>
		/// <returns></returns>
		public List<RegisterDoctor> getDoctors(int tenantId)
        {
			List<RegisterDoctor> result = new List<RegisterDoctor>();
			var tuple = employeebll.getEmployees(tenantId);
			var mods = tuple.Item1;
			if (mods != null && mods.Any())
            {
				foreach(var node in mods)
                {
					RegisterDoctor mod = new RegisterDoctor();
					mod.DoctorId = node.EmployeeId;
					mod.DoctorName = node.EmployeeName;
					result.Add(mod);
				}
            }
			return result;
		}

		///<summary> 
		///获取详情记录
		///</summary> 
		///<param name="id"></param>
		/// <returns></returns>
		public RegisterEntity get(int id)
		{
			return db.Get(id);
		}
		///<summary> 
		///获取列表-分页
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public Tuple<List<RegisterModel>,int> gets(int request,int pageSize,int pageIndex,string searchKey,DateTime? start,DateTime? end,int doctorid,int isUsed = 0)
		{
			string doctorname = "";
			//	string sql = "select * from Employees where TenantId=" + request + " and EmployeeId=" + doctorid;
			var doctornames =db.BackDoctorName(request, doctorid);
		///	doctorname = doctornames;
			var datas  = db.SearchPage(request,pageSize,pageIndex,searchKey,start,end, doctorname, isUsed);
			if(datas.Item1 == null || !datas.Item1.Any())
            {
				Tuple<List<RegisterModel>, int> result1 = new(new List<RegisterModel>(), datas.Item2);
				return result1;
            }
            else
            {
				Tuple<List<RegisterModel>, int> result2 = new(datas.Item1.EntityToDto<RegisterModel>(), datas.Item2);
				return result2;
			}
		}

		public List<EmployeeModel> getDoctorIds(int tenantid,List<string> names)
        {
			return db.getDoctorIds(tenantid, names).ToList().EntityToDto<EmployeeModel>();
		}

	}
};