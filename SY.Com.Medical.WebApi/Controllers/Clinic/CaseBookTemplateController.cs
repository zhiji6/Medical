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
    /// 控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [Api_Tenant]
    public class CaseBookTemplateController : ControllerBase 
	{
		 CaseBookTemplate bll = new CaseBookTemplate();
		///<summary> 
		///获取详情记录
		///</summary> 
		///<param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public BaseResponse<CaseBookTemplateModel> get(int id)
		{
			BaseResponse<CaseBookTemplateModel> result = new BaseResponse<CaseBookTemplateModel>();
			result.Data = bll.get(id);
			return result;
		}
		///<summary> 
		///获取列表-分页
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<CaseBookTemplateModel>> gets(CaseBookTemplateRequest request)
		{
			BaseResponse<List<CaseBookTemplateModel>> result = new BaseResponse<List<CaseBookTemplateModel>>();
			var tuple = bll.gets(request);
			result.Data = tuple.Item1.ToList();
			result.CalcPage(tuple.Item2, request.PageIndex, request.PageSize);
			return result;
		}
		///<summary> 
		///新增
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<int> add(CaseBookTemplateAdd request)
		{
			BaseResponse<int> result = new BaseResponse<int>();
			result.Data = bll.add(request);
			return result;
		}
		///<summary> 
		///修改
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<bool> update(CaseBookTemplateUpdate request)
		{
			BaseResponse<bool> result = new BaseResponse<bool>();
			bll.update(request);
			result.Data = true;
			return result;
		}
		///<summary> 
		///删除
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<bool> delete(CaseBookTemplateDelete request)
		{
			BaseResponse<bool> result = new BaseResponse<bool>();
			bll.delete(request);
			result.Data = true;
			return result;
		}
	}
} 