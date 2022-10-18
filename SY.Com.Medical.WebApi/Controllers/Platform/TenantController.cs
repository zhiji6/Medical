using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.BLL;
using SY.Com.Medical.BLL.Platform;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SY.Com.Medical.WebApi.Controllers.Platform
{
    /// <summary>
    /// 租户控制器
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [Authorize]
    [ApiController]
    public class TenantController : ControllerBase
    {
        Tenant tenantbll = new Tenant();

        /// <summary>
        /// 获取用户租户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<UserTenantResponse>> getTenants(BaseModel request)
        {
            BaseResponse<List<UserTenantResponse>> response = new BaseResponse<List<UserTenantResponse>>();
            try
            {
                response.Data = tenantbll.GetTenants(request).ToList();
                return response;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return response.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return response.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取租户详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Api_Tenant]
        public BaseResponse<UserTenantResponse> getTenant(BaseModel request)
        {
            BaseResponse<UserTenantResponse> response = new BaseResponse<UserTenantResponse>();
            try
            {
                response.Data = tenantbll.GetTenant(request);
                if (response.Data != null)
                {
                    if (string.IsNullOrEmpty(response.Data.YBUrl))
                    {
                        response.Data.YBUrl = "http://localhost:8001/";
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return response.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return response.sysException(ex.Message);
                }
            }
        }


        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<UserTenantResponse> createTenant(TenentCreateRequest request)
        {
            BaseResponse<UserTenantResponse> response = new BaseResponse<UserTenantResponse>();
            try
            {
                response.Data = tenantbll.CreateTenant(request);

                return response;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return response.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return response.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 修改租户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Api_Tenant]
        public BaseResponse<TenantModel> updateTenant(TenantRequest request)
        {
            BaseResponse<TenantModel> response = new BaseResponse<TenantModel>();
            try
            {
                tenantbll.UpdateTenant(request);
                response.Data = null;
                return response;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return response.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return response.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 删除租户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Api_Tenant]
        public BaseResponse<bool> deleteTenant(BaseModel request)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                tenantbll.DeleteTenant(request.TenantId, request.UserId);
                response.Data = true;
                return response;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return response.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return response.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取用户的菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Api_Tenant]
        public BaseResponse<List<MenuTreeResponse>> getMenu(EmployeeGetModel request)
        {
            BaseResponse<List<MenuTreeResponse>> response = new BaseResponse<List<MenuTreeResponse>>();
            try
            {
                response.Data = tenantbll.GetMenu(request);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return response.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return response.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 搜索全网租户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<TenantAllSearchResponse>> getAllTenants(TenantAllSearchRequest request)
        {
            BaseResponse<List<TenantAllSearchResponse>> response = new BaseResponse<List<TenantAllSearchResponse>>();
            try {
                var tuple = tenantbll.getAllPlatform(request);
                response.Data = tuple.Item1.ToList();
                response.CalcPage(tuple.Item2, request.PageIndex, request.PageSize);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return response.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return response.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 禁用租户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Api_Tenant]
        public BaseResponse<bool> disableTenant(TernantOperationEnable request)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                tenantbll.DisableTenant(request.TenantId, request.isEnable);
                response.Data = true;
                return response;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return response.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return response.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 租户充值
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Api_Tenant]
        public BaseResponse<bool> BuyServiceTime(TenantBuyRequest request)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                response.Data = tenantbll.BuyServiceTime(request);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return response.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return response.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 点击进入诊所，或者员工信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Api_Tenant]        
        public BaseResponse<EmployeeModel> IntoTenant(BaseModel request)
        {
            BaseResponse<EmployeeModel> response = new BaseResponse<EmployeeModel>();
            try
            {
                response.Data = tenantbll.IntoTenant(request.TenantId,request.UserId);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return response.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return response.sysException(ex.Message);
                }
            }
        }

    }
}
