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
    /// 员工控制器
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    [Api_Tenant]
    public class EmployeeController : ControllerBase
    {
        Employee bll = new Employee();

        /// <summary>
        /// 获取租户的员工
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<EmployeeModel>> getList(EmployeeGetsModel request)
        {
            BaseResponse<List<EmployeeModel>> result = new BaseResponse<List<EmployeeModel>>();
            try
            {
                var tuple = bll.getEmployees(request.TenantId,request.PageSize,request.PageIndex,request.searchKey,request.Department);
                result.Data = tuple.Item1;
                var closemods = bll.getEmployeesClose(request.TenantId);
                result.Data.AddRange(closemods);
                User userbll = new User();
                var users = userbll.getUsers(result.Data.Select(s => s.UserId).ToList());
                if(users != null)
                {
                    foreach(var data in result.Data)
                    {
                        if(users.Exists(e=>e.UserId == data.UserId))
                        {
                            data.Account = users.Find(e => e.UserId == data.UserId).Account;
                        }
                    }
                }
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
        /// 获取租户的员工-禁用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<EmployeeModel>> getCloseList(EmployeeGetsModel request)
        {
            BaseResponse<List<EmployeeModel>> result = new BaseResponse<List<EmployeeModel>>();
            try
            {
                var tuple = bll.getEmployeesClose(request.TenantId, request.PageSize, request.PageIndex, request.searchKey, request.Department);
                result.Data = tuple.Item1;
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

        /// <summary>
        /// 获取单个员工信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        public BaseResponse<EmployeeModel> get(int employeeId)
        {
            BaseResponse<EmployeeModel> result = new BaseResponse<EmployeeModel>();
            try
            {
                result.Data = bll.getEmployee(employeeId);
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
        /// 邀请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<int> invity(EmployeeInvity request)
        {
            BaseResponse<int> result = new BaseResponse<int>();
            try
            {
                result.Data = bll.invite(request.Account,request.TenantId,request.Roles,request.EmployeeName);
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
        /// 启用禁用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> OpenClose(EmployeeOpenClose request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                var model = bll.getEmployee(request.EmployeeId);
                bll.updateEmployee(model, request.OpenClose);
                result.Data = true;
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
        /// 更新员工信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> update(EmployeeModel request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                bll.updateEmployee(request,1);
                result.Data = true;
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
