using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.BLL;
using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SY.Com.Medical.WebApi.Controllers.Clinic
{
    /// <summary>
    /// 挂号控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [Api_Tenant]
    public class RegisterController : ControllerBase 
	{
		Register bll = new Register();
		Patient patbll = new Patient();
		RegisterProject rpbll = new RegisterProject();

		/// <summary>
		/// 挂号
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<int> Save(RegisterSaveModel request)
        {
			BaseResponse<int> result = new BaseResponse<int>();
			result.Data = bll.Save(request);
			return result;
		}

		/// <summary>
		/// 挂号时获取医生下拉框
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<RegisterDoctor>> getDoctors(BaseModel request)
        {
			BaseResponse<List<RegisterDoctor>> result = new BaseResponse<List<RegisterDoctor>>();
			result.Data = bll.getDoctors(request.TenantId);
			return result;
		}

		///<summary> 
		///获取挂号项目下拉框
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<RegisterProjectModel>> getProjects(BaseModel request)
		{
			BaseResponse<List<RegisterProjectModel>> result = new BaseResponse<List<RegisterProjectModel>>();
			try
			{
				var mods = rpbll.getByTenant(request.TenantId);
				result.Data = mods;
				return result;
			}
			catch (Exception ex)
			{
				if (ex is MyException)
				{
					return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
				}
				else
				{
					return result.sysException(ex.Message);
				}
			}
		}




		///<summary> 
		///获取列表-分页
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<RegisterModel>> gets(Register1Request request)
		{
			BaseResponse<List<RegisterModel>> result = new BaseResponse<List<RegisterModel>>();
			try{
				var tuple = bll.gets(request.TenantId,request.PageSize,request.PageIndex,request.SearchKey,request.start,request.end, request.DoctorId);
				if (tuple.Item1 != null && tuple.Item1.Count > 0)
				{
					//获取医生Id和科室Id
					var doctornames = bll.getDoctorIds(tuple.Item1.First().TenantId, tuple.Item1.Select(x => x.DoctorName).ToList());
					if (doctornames != null && doctornames.Any())
					{
						foreach (var item in tuple.Item1)
						{

						}
						tuple.Item1.ToList().ForEach(x =>
						{
							if (doctornames.ToList().Find(y => y.EmployeeName == x.DoctorName) != null)
							{
								x.DoctorId = doctornames.ToList().Find(y => y.EmployeeName == x.DoctorName).EmployeeId;
							}
						});
					}
				}
			result.Data = tuple.Item1.ToList();
				result.CalcPage(tuple.Item2, request.PageIndex, request.PageSize);
				return result;
			}catch(Exception ex)
			{
				if (ex is MyException)
				{
					return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
				}
				else
				{
					return result.sysException(ex.Message);
				}
			}
		}


		/// <summary>
		/// 模糊搜索租户患者信息
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<PatientModel>> searchPatient(PatientSearchPage request)
		{
			BaseResponse<List<PatientModel>> result = new BaseResponse<List<PatientModel>>();
			try
			{
				PatientPage page = new PatientPage();
				page.PageSize = page.PageSize;
				page.PageIndex = page.PageIndex;
				page.SearchKey = request.SearchKey;
				var tuple = patbll.gets(page);
				result.Data = tuple.Item1;
				result.CalcPage(tuple.Item2, 1, 1);
				return result;
			}
			catch (Exception ex)
			{
				if (ex is MyException)
				{
					return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
				}
				else
				{
					return result.sysException(ex.Message);
				}
			}
		}

	}
} 