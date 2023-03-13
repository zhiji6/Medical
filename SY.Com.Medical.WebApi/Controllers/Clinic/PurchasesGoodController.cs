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
    public class PurchasesGoodController : ControllerBase 
	{
		 PurchasesGood bll = new PurchasesGood();

		/// <summary>
		/// 采购页面搜索药品/材料
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<PurchasesGoodSearchResponse>> searchGoods(PurchasesGoodSearchRequest request)
        {
			BaseResponse<List<PurchasesGoodSearchResponse>> result = new BaseResponse<List<PurchasesGoodSearchResponse>>();
			if(request.Type == 0)
            {
				request.GoodType = 11;
			}else if(request.Type == 1)
            {
				request.GoodType = 6;
			}else
            {
				request.GoodType = 5;
			}
			var tuple = bll.searchGoods(request);
			result.Data = tuple.Item1.ToList();
			result.CalcPage(tuple.Item2, request.PageIndex, request.PageSize);
			return result;
		}


		/// <summary>
		/// 采购药品/材料
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<bool> Purchase(List<PurchasesGoodModel> request)
        {
			var tenantid = HttpContext.Request.Headers["TenantId"].ToString();
			if(string.IsNullOrEmpty(tenantid))
            {
				throw new MyException("TenantId非法");
			}
			foreach(var item in request)
            {
				item.TenantId = int.Parse(tenantid);
            }
			BaseResponse<bool> result = new BaseResponse<bool>();
			result.Data =bll.Purchases(request);
			return result;
        }

		/// <summary>
		/// 获取某采购单明细
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<PurchasesGoodResponse>> getPurchase(PurchasesGoodRequest request)
        {
			BaseResponse<List<PurchasesGoodResponse>> result = new BaseResponse<List<PurchasesGoodResponse>>();
			result.Data = bll.getPurchases(request.TenantId, request.PurchaseId);
			return result;
        }

		Purchase purchaseBll = new Purchase();
		///<summary> 
		///获取单一采购单主信息
		///</summary> 
		///<param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public BaseResponse<PurchaseModel> get(int id)
		{
			BaseResponse<PurchaseModel> result = new BaseResponse<PurchaseModel>();
			try
			{
				result.Data = purchaseBll.get(id);
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
		///获取采购单主信息列表-分页
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public BaseResponse<List<PurchaseModel>> gets(PurchaseRequest request)
		{
			BaseResponse<List<PurchaseModel>> result = new BaseResponse<List<PurchaseModel>>();
			try
			{
				var tuple = purchaseBll.gets(request);
				result.Data = tuple.Item1.ToList();
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