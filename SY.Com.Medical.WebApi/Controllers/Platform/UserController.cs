using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    /// 用户控制器
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]    
    public class UserController : ControllerBase
    {
        User userbll = new User();
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]        
        public BaseResponse<bool> ChangePwd(ChangeRequst request)
        {            
            BaseResponse<bool> result = new BaseResponse<bool>();
            if (string.IsNullOrEmpty(request.OldPwd)) return result.busExceptino(Enum.ErrorCode.业务逻辑错误, "未传入原密码");
            if (request.UserId == 0) return result.busExceptino(Enum.ErrorCode.业务逻辑错误, "无法识别用户");
            if(request.Pwd.Trim() != request.PwdConfirm.Trim()) return result.busExceptino(Enum.ErrorCode.业务逻辑错误, "两次密码不同");
            try
            {
                result.Data = userbll.Change(request.Pwd,request.UserId,request.OldPwd);
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
