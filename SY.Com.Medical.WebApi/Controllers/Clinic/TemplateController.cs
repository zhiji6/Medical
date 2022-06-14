using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.BLL;
using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.BLL.Platform;
using SY.Com.Medical.Extension;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SY.Com.Medical.WebApi.Controllers.Clinic
{
    /// <summary>
    /// 控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [Api_Tenant]
    public class TemplateController : ControllerBase 
	{
		 Template bll = new Template();
		///<summary> 
		///获取详情记录
		///</summary> 
		///<param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public BaseResponse<TemplateModel> get(int id)
		{
			BaseResponse<TemplateModel> result = new BaseResponse<TemplateModel>();
				try{
					result.Data = bll.get(id);
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
		///<summary> 
		///获取列表-分页
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<TemplateModel>> gets(TemplateRequest request)
		{
				BaseResponse<List<TemplateModel>> result = new BaseResponse<List<TemplateModel>>();
				try{
					var tuple = bll.gets(request);
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
		///<summary> 
		///新增
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<int> add(TemplateAdd request)
		{
				BaseResponse<int> result = new BaseResponse<int>();
				try{
					result.Data = bll.add(request);
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
		///<summary> 
		///修改
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<bool> update(TemplateUpdate request)
		{
			BaseResponse<bool> result = new BaseResponse<bool>();
				try{
					bll.update(request);
					result.Data = true;
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
		///<summary> 
		///删除
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<bool> delete(TemplateDelete request)
		{
			BaseResponse<bool> result = new BaseResponse<bool>();
				try{
					bll.delete(request);
					result.Data = true;
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
		/// 获取模板类型
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<TemplateType>> getTemplateType()
		{
			BaseResponse<List<TemplateType>> result = new BaseResponse<List<TemplateType>>();
			try
			{
				result.Data = new List<TemplateType>();
				result.Data.Add(new TemplateType { TemplateTypeId = 1, TemplateName = "私有" });
				result.Data.Add(new TemplateType { TemplateTypeId = 2, TemplateName = "公开" });				
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

		/// <summary>
		/// 获取租户的员工
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<EmployeeModel>> getEmployeeList(EmployeeGetsModel request)
		{
			BaseResponse<List<EmployeeModel>> result = new BaseResponse<List<EmployeeModel>>();
			Employee empbll = new Employee();
			try
			{
				var tuple = empbll.getEmployees(request.TenantId, request.PageSize, request.PageIndex, request.searchKey, request.Department);
				result.Data = tuple.Item1;
				var closemods = empbll.getEmployeesClose(request.TenantId);
				result.Data.AddRange(closemods);
				result.CalcPage(tuple.Item2 + closemods.Count, request.PageIndex, request.PageSize);
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

		/// <summary>
		/// 获取药品和材料列表-分页
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<GoodBllModels>> getsGoods(GoodsRequest request)
		{
			Good goodsbll = new Good();
			BaseResponse<List<GoodBllModels>> result = new BaseResponse<List<GoodBllModels>>();
			try
			{
				//（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
				if (request.GoodType == 1)
				{
					request.GoodType = 7;
				}
				var tuple = goodsbll.getGoods(request.TenantId, request.PageSize, request.PageIndex, request.GoodType, 0, request.SearchKey);
				if (tuple.Item2 == 0)
				{
					return new BaseResponse<List<GoodBllModels>>();
				}
				result.Data = tuple.Item1.ToList().Mapping<GoodBllModels>();
				result.CalcPage(tuple.Item2, request.PageIndex, request.PageSize);
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