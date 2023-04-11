using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.BLL;
using SY.Com.Medical.BLL.Clinic.Print;
using SY.Com.Medical.BLL.Platform;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SY.Com.Medical.WebApi.Controllers.Clinic
{
    /// <summary>
    /// 系统打印接口v1,目前统一使用此类接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [Api_Tenant]
    public class PrintController : ControllerBase
    {
        PrintTemplate printTemplate = new PrintTemplate();
        Tenant tenant = new Tenant();

        /// <summary>
        /// 获取用户(Tenant)所有类型可用的打印文件,按照打印类型进行分类
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public BaseResponse<List<PrintTemplate>> GetTenantPrintFiles(BaseModel request)
        {
            BaseResponse<List<PrintTemplate>> result = new BaseResponse<List<PrintTemplate>>();
            result.Data = printTemplate.getTemplates(request.TenantId);
            return result;
        }

        /// <summary>
        /// 用户创建打印文件,每个类型的打印文件最多只能创建两个(系统文件不计数)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<PrintFile> AddPrintFile(PrintAddFileRequest request)
        {
            BaseResponse<PrintFile> result = new BaseResponse<PrintFile>();
            result.Data = printTemplate.AddPrintFile(request.TenantId, request.TemplateId);
            return result;
        }

        /// <summary>
        /// 修改打印文件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<PrintFile> UpdatePrintFile(PrintUpdateFileRequest request)
        {
            BaseResponse<PrintFile> result = new BaseResponse<PrintFile>();
            PrintFile printfile = new PrintFile(request.TenantId,  request.FileId);
            result.Data = printfile.Update(request.Bytes);
            return result;
        }

        /// <summary>
        /// 删除打印文件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> DeletePrintFile(PrintDeleteFileRequest request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            PrintFile printfile = new PrintFile(request.TenantId, request.FileId);
            printfile.Delete();
            result.Data = true;
            return result;
        }

        /// <summary>
        /// 设置打印文件为使用状态
        /// (一个类型中只有一个为使用状态)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> UsePrintFile(PrintUseFileRequest request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            PrintFile printfile = new PrintFile( request.FileId);
            printfile.SetUse(request.TenantId);
            result.Data = true;
            return result;
        }


        /// <summary>
        /// 挂号打印,退号打印,通过IsBack进行区分
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<PrintRegisterResponseModel> PrintRegister(PrintRegisterRequestModel request)
        {
            BaseResponse<PrintRegisterResponseModel> result = new BaseResponse<PrintRegisterResponseModel>();
            try
            {
                if(request.IsView)
                {
                    result.Data = new RegisterPrint().View(request.TenantId, request.RegisterId);                    
                }
                else
                {
                    result.Data = new RegisterPrint().Print(request.TenantId, request.RegisterId);
                }
                result.Data.ViewPath = new PrintTemplate().ChooseFile(request.TenantId, request.IsBack ? 2 : 1).FilePath;
                var tenantmodel = tenant.getById(request.TenantId);
                result.Data.TenantName = tenantmodel.TenantName;
                result.Data.Temp = new List<PrintTemp>() { new PrintTemp { tempid = 1, tempname = "a" } };
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
        /// 打印处方
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<CombinePrintDataModel>> PrintPrescriptions(PrintPrescriptionRequestModel request)
        {
            BaseResponse<List<CombinePrintDataModel>> result = new BaseResponse<List<CombinePrintDataModel>>();
            try
            {
                CombinePrintDataModel data;
                var print = new OutPatienPrint();
                if (request.IsView)
                {
                    data = print.View(request.TenantId, request.OutpatientId);
                }
                else
                {
                    data = print.Print(request.TenantId, request.OutpatientId);
                }
                var tenantmodel = tenant.getById(request.TenantId);
                print.EastPrescription(request.TenantId, data, tenantmodel, result.Data);
                print.WestPrescription(request.TenantId, data, tenantmodel, result.Data);
                print.ProjPrescription(request.TenantId, data, tenantmodel, result.Data);
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
        /// 打印处置单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<CombinePrintDataModel> PrintDisposal(PrintPrescriptionRequestModel request)
        {
            BaseResponse<CombinePrintDataModel> result = new BaseResponse<CombinePrintDataModel>();
            try
            {
                if (request.IsView)
                {
                    result.Data = new OutPatienPrint().View(request.TenantId, request.OutpatientId);
                }
                else
                {
                    result.Data = new OutPatienPrint().Print(request.TenantId, request.OutpatientId);
                }
                var tenantmodel = tenant.getById(request.TenantId);
                result.Data.ViewPath = new PrintTemplate().ChooseFile(request.TenantId, 8).FilePath;
                result.Data.TenantName = tenantmodel.TenantName;
                result.Data.TenantCode = tenantmodel.YBCode;
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
        /// 打印收费退费
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<CombinePrintDataModel> PrintCharge(ChargeRecordRequestModel request)
        {
            BaseResponse<CombinePrintDataModel> result = new BaseResponse<CombinePrintDataModel>();
            if (request.IsView)
            {
                result.Data = new OutPatienPrint().View(request.TenantId, request.OutPatientId);
            }
            else
            {
                result.Data = new OutPatienPrint().Print(request.TenantId, request.OutPatientId);
            }
            var tenantmodel = tenant.getById(request.TenantId);
            result.Data.TenantName = tenantmodel.TenantName;
            result.Data.TenantCode = tenantmodel.YBCode;
            switch (request.ChargeType)
            {
                case "门诊收费": result.Data.ViewPath = new PrintTemplate().ChooseFile(request.TenantId, 6).FilePath ; break;
                case "门诊退费": result.Data.ViewPath = new PrintTemplate().ChooseFile(request.TenantId, 7).FilePath ; break;
            }
            return result;
        }
    }
}
