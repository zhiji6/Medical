using SY.Com.Medical.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model
{
    /// <summary>
    /// 返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T> 
    {
        /// <summary>
        /// 结果,-1:系统异常,-2:逻辑异常,0:正常
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMes { get; set; }
        /// <summary>
        /// 报文体
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 一共多少页
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 一共多少记录
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 系统异常
        /// </summary>
        /// <param name="message">错误信息</param>
        public BaseResponse<T> sysException(string message)
        {
            Result = -1;
            ErrCode = ErrorCode.程序异常.ToString();
            ErrMes = message;
            return this;
        }

        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="errcode">错误码</param>
        /// <param name="message">错误信息</param>
        public BaseResponse<T> busExceptino(ErrorCode errcode, string message)
        {
            Result = -2;
            ErrCode = "[" + errcode.ToString() + "]";
            ErrMes = message;
            return this;
        }

        /// <summary>
        /// 计算分页
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public void CalcPage(int totalCount,int pageIndex,int pageSize)
        {
            TotalCount = totalCount;
            int extent = totalCount % pageSize == 0 ? 0 : 1;
            TotalPage = totalCount / pageSize + extent;
        }

    }
}
