using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.BLL;
using SY.Com.Medical.BLL.Platform;
using SY.Com.Medical.Model;
using SY.Com.Medical.WebApi.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SY.Com.Medical.WebApi.Controllers.Platform
{
    /// <summary>
    /// 登录相关
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        User userbll = new User();
        UserManager managerbll = new UserManager();

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>       
        [HttpPost]
        [AllowAnonymous]
        public BaseResponse<LoginResponse> login(LoginRequest request)
        {
            BaseResponse<LoginResponse> result = new BaseResponse<LoginResponse>();
            try
            {
                result.Data = new LoginResponse();
                var usermodel = userbll.Login(request);
                result.Data.access_token = JWTTokenValidationParameters.getSecurityToken(usermodel.UserId, usermodel.Account);
                result.Data.Account = usermodel.Account;
                result.Data.LogoImg = usermodel.LogoImg;
                result.Data.Uid = usermodel.UserId;
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
        /// 后台充值页面登录
        /// </summary>
        /// <param name="request">账号密码</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public BaseResponse<LoginResponse> loginSystem(LoginRequest request)
        {
            BaseResponse<LoginResponse> result = new BaseResponse<LoginResponse>();
            try
            {
                result.Data = new LoginResponse();                
                var usermodel = managerbll.Login(request);
                result.Data.access_token = JWTTokenValidationParameters.getSecurityToken(usermodel.UserId, usermodel.Account);
                result.Data.Account = usermodel.Account;
                result.Data.LogoImg = usermodel.LogoImg;
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
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public BaseResponse<RegisterResponse> register(RegisterRequest request)
        {
            BaseResponse<RegisterResponse> result = new BaseResponse<RegisterResponse>();
            try
            {
                if(userbll.ExistsAccount(request.Account))
                {
                    return result.busExceptino(Enum.ErrorCode.用户已存在,"用户已存在,请重新登录");
                }                
                result.Data = userbll.Register(request);
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
        /// 重置密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public BaseResponse<ResetResponse> reset(ResetRequest request)
        {
            BaseResponse<ResetResponse> result = new BaseResponse<ResetResponse>();
            try
            {
                result.Data = userbll.Reset(request);
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
