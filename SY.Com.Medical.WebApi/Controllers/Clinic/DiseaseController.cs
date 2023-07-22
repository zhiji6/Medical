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
    public class DiseaseController : ControllerBase 
	{
		 Disease bll = new Disease();
		/////<summary> 
		/////获取详情记录
		/////</summary> 
		/////<param name="id"></param>
		///// <returns></returns>
		//[HttpGet]
		//public BaseResponse<DiseaseModel> get(int id)
		//{
		//	BaseResponse<DiseaseModel> result = new BaseResponse<DiseaseModel>();
		//		try{
		//			result.Data = bll.get(id);
		//			return result;
		//		}catch(Exception ex)
		//		{
		//			if (ex is MyException)
		//			{
		//				return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
		//			}
		//			else
		//			{
		//				return result.sysException(ex.Message);
		//			}
		//		}
		//}
		///<summary> 
		///获取列表-分页
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<DiseaseModel>> gets(DiseaseRequest request)
		{
			BaseResponse<List<DiseaseModel>> result = new BaseResponse<List<DiseaseModel>>();
			try{
				result.Data = bll.getsNoPage(request.DiseaseNames);
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

        #region
		/*
        ///<summary> 
        ///新增
        ///</summary> 
        ///<param name="request"></param>
        /// <returns></returns>
        [HttpPost]
		public BaseResponse<int> add(DiseaseAdd request)
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
		public BaseResponse<bool> update(DiseaseUpdate request)
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
		public BaseResponse<bool> delete(DiseaseDelete request)
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


		*/
        #endregion
    }
} 