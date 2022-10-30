using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.BLL;
using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.BLL.Platform;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SY.Com.Medical.WebApi.Controllers.Clinic
{
    /// <summary>
    /// 打印报表控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [Api_Tenant]
    public class PrintPreViewController : ControllerBase
    {
        PrintPreView bll = new PrintPreView();
        Tenant tenant = new Tenant();
        /// <summary>
        /// 挂号打印
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<PrintRegisterResponseModel> PrintRegister(PrintRegisterRequestModel request)
        {
            BaseResponse<PrintRegisterResponseModel> result = new BaseResponse<PrintRegisterResponseModel>();
            try {
                result.Data = bll.getRegisterData(request.RegisterId);
                result.Data.ViewPath = bll.getViewPath(1, request.TenantId);
                var tenantmodel = tenant.getById(request.TenantId);
                result.Data.TenantName = tenantmodel.TenantName;
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
        /// 退号打印
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<PrintRegisterResponseModel> PrintBackRegister(PrintRegisterRequestModel request)
        {
            BaseResponse<PrintRegisterResponseModel> result = new BaseResponse<PrintRegisterResponseModel>();
            try
            { 
                result.Data = bll.getRegisterData(request.RegisterId);
                result.Data.ViewPath = bll.getViewPath(2, request.TenantId);
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
        /// 打印处方和治疗单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<PrintPrescriptionResponseModel> PrintPrescriptions(PrintPrescriptionRequestModel request)
        {
            BaseResponse<PrintPrescriptionResponseModel> result = new BaseResponse<PrintPrescriptionResponseModel>();
            try
            {
                result.Data = new PrintPrescriptionResponseModel();
                result.Data.Data = bll.getOutpatient(request.TenantId,request.OutpatientId);
                switch (request.Type)
                {
                    case 1: result.Data.ViewPath = bll.getViewPath(3, request.TenantId);
                        result.Data.Data.Prescriptions = result.Data.Data.Prescriptions.Where(x => x.PreName == "中处方药").ToList();
                        break;
                    case 2: result.Data.ViewPath = bll.getViewPath(4, request.TenantId);
                        result.Data.Data.Prescriptions = result.Data.Data.Prescriptions.Where(x => x.PreName == "西处方药").ToList();
                        break;
                    case 3: result.Data.ViewPath = bll.getViewPath(5, request.TenantId);
                        result.Data.Data.Prescriptions = result.Data.Data.Prescriptions.Where(x => x.PreName == "项目处方").ToList();
                        break;
                    case 4: result.Data.ViewPath = bll.getViewPath(8, request.TenantId);
                        result.Data.Data.Prescriptions = result.Data.Data.Prescriptions.Where(x => x.PreName == "项目处方").ToList();
                        break;
                }
                var tenantmodel = tenant.getById(request.TenantId);
                result.Data.Data.TenantName = tenantmodel.TenantName;
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
        public BaseResponse<ChargeRecordResponseModel> PrintCharge(ChargeRecordRequestModel request)
        {
            BaseResponse<ChargeRecordResponseModel> result = new BaseResponse<ChargeRecordResponseModel>();
            try
            {
                result.Data = bll.getByOutpatientId(request.TenantId,request.OutPatientId,request.ChargeType);
                switch(request.ChargeType)
                {
                    case "门诊收费": result.Data.ViewPath = bll.getViewPath(6, request.TenantId);break;
                    case "门诊退费": result.Data.ViewPath = bll.getViewPath(7, request.TenantId); break;
                }
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
        /// 打印病历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<CaseBookResponseModel> PrintCaseBook(CaseBookRequestModel request)
        {
            BaseResponse<CaseBookResponseModel> result = new BaseResponse<CaseBookResponseModel>();
            try
            {
                result.Data = new CaseBookResponseModel();
                result.Data.Data = bll.getCaseBookOne(request.CaseBookId);
                result.Data.ViewPath = bll.getViewPath(9, request.TenantId);
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
