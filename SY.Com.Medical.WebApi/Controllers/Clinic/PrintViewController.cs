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
	[ObsoleteAttribute]
	public class PrintViewController : ControllerBase 
	{
		 PrintView bll = new PrintView();
		///<summary> 
		///获取详情记录
		///</summary> 
		///<param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public BaseResponse<PrintViewModel> get(int id)
		{
			BaseResponse<PrintViewModel> result = new BaseResponse<PrintViewModel>();
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


		/// <summary>
		/// 获取打印视图的所有分类
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<PrintStyleModel>> getStyle()
        {
			BaseResponse<List<PrintStyleModel>> result = new BaseResponse<List<PrintStyleModel>>();
			try
			{
				result.Data = bll.getStyles();
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
		///获取某个类型的视图
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<PrintViewModel>> gets(PrintViewRequest request)
		{
			BaseResponse<List<PrintViewModel>> result = new BaseResponse<List<PrintViewModel>>();
			try{
				result.Data = bll.gets(request);
				if(result.Data == null)
                {
					result.Data = new List<PrintViewModel>();
                }
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


	}
} 