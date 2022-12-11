using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.BLL;
using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.BLL.Platform;
using SY.Com.Medical.Enum;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SY.Com.Medical.WebApi.Controllers.Platform
{
    /// <summary>
    /// 静态资源
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class StaticFileController : ControllerBase
    {
        /// <summary>
        /// 上传静态资源
        /// </summary>
        /// <param name="files">Form表单提交图片</param>
        /// <param name="staticFileType">文件类型枚举</param>
        /// <param name="staticFileBusiness">上传业务枚举</param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<string>> UpLoadStaticFile([FromForm]List<IFormFile> files, [FromForm] StaticFileType staticFileType, [FromForm] StaticFileBusiness staticFileBusiness)
        {
            BaseResponse<List<string>> result = new BaseResponse<List< string>>();
            result.Data = new List<string>();
            try
            {                
                
                StaticFileModel request = new StaticFileModel() { StaticFileType = staticFileType, StaticFileBusiness = staticFileBusiness };
                if (files.Count < 1)
                {
                    throw new MyException("为上传任何文件");
                }
                foreach (var item in files)
                {
                    if (item != null)
                    {
                        //文件后缀
                        request.fileExtension = Path.GetExtension(item.FileName);
                        //获取文件操作类
                        var fileupload = FileUploadFactory.getInstance(request);
                        MemoryStream ms = new MemoryStream();
                        item.CopyTo(ms);
                        ms.Flush();
                        //执行文件保存
                        string filepath = fileupload.SaveFile(ms);
                        ms.Close();
                        result.Data.Add("http://" +  Request.Host.Value + filepath);
                    }
                }
                //返回文件路径
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
        /// 上传打印视图
        /// </summary>
        /// <param name="files">grf文件</param>
        /// <param name="TenantId">租户Id</param>
        /// <param name="Style">打印视图类型Id</param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<string>> PrintFile([FromForm] List<IFormFile> files,int TenantId,int Style)
        {                        
            if(TenantId == 0)
            {
                TenantId = int.Parse(Request.Headers["TenantId"].ToString());
            }
            BaseResponse<List<string>> result = new BaseResponse<List<string>>();
            result.Data = new List<string>();

            StaticFileModel request = new StaticFileModel() { StaticFileType = StaticFileType.Print, StaticFileBusiness = StaticFileBusiness.租户打印视图, filepathExtension=$"{Style}/{TenantId}" };
            if (files.Count < 1)
            {
                throw new MyException("为上传任何文件");
            }
            foreach (var item in files)
            {
                if (item != null)
                {
                    //文件后缀
                    request.fileExtension = Path.GetExtension(item.FileName);
                    //获取文件操作类
                    var fileupload = FileUploadFactory.getInstance(request);
                    MemoryStream ms = new MemoryStream();
                    item.CopyTo(ms);
                    ms.Flush();
                    //执行文件保存
                    string filepath = fileupload.SaveFile(ms);
                    ms.Close();
                    PrintView print = new PrintView();
                    print.add(new PrintViewAdd() { TenantId = TenantId, Style = Style });
                    result.Data.Add("http://" + Request.Host.Value + filepath);
                }
            }
            //返回文件路径
            return result;
         
        }

        /// <summary>
        /// 导入物料Excel
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<string>> GoodExcel([FromForm] List<IFormFile> files)
        {
            var tenantid = Request.Headers["TenantId"].ToString();
            BaseResponse<List<string>> result = new BaseResponse<List<string>>();
            if (string.IsNullOrEmpty(tenantid))
            {
                return result.busExceptino(Enum.ErrorCode.业务逻辑错误, "TenantId为空");
            }
            result.Data = new List<string>();
            try
            {

                StaticFileModel request = new StaticFileModel() { StaticFileType = StaticFileType.Excel, StaticFileBusiness = StaticFileBusiness.物料导入Excel, filepathExtension = tenantid };
                if (files.Count < 1)
                {
                    throw new MyException("未上传任何文件");
                }
                foreach (var item in files)
                {
                    if (item != null)
                    {
                        //文件后缀
                        request.fileExtension = Path.GetExtension(item.FileName);
                        //获取文件操作类
                        var fileupload = FileUploadFactory.getInstance(request);
                        MemoryStream ms = new MemoryStream();
                        item.CopyTo(ms);
                        ms.Flush();
                        //执行文件保存
                        string filepath = fileupload.SaveFile(ms);
                        ms.Close();
                        result.Data.Add("http://" + Request.Host.Value + filepath);
                    }
                }
                //返回文件路径
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
