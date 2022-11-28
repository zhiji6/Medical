using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SY.Com.Medical.BLL;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SY.Com.Medical.WebApi.Filter
{
    /// <summary>
    /// 统一处理异常
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        log4net.ILog log = LogManager.GetLogger("mylog");
        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                if(context.Exception is MyException)
                {
                    context.Result = new JsonResult(new BaseResponse<string>().busExceptino(Enum.ErrorCode.业务逻辑错误, context.Exception.Message));
                }
                else
                {
                    log.Debug(context.Exception.Message + " 堆栈:" + context.Exception.StackTrace);
                    context.Result = new JsonResult(new BaseResponse<string>().sysException(context.Exception.Message));
                }                
            }
            context.ExceptionHandled = true;
        }


    }
}
