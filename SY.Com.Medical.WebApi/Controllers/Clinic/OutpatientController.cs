using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.BLL;
using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.Extension;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SY.Com.Medical.WebApi.Controllers.Clinic
{
    /// <summary>
    /// 门诊+处方控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [Api_Tenant]
    public class OutpatientController : ControllerBase 
	{
	    Outpatient bll = new Outpatient();
        Good goodsbll = new Good();
        Register regbll = new Register();
        Patient patbll = new Patient();

        /// <summary>
        /// 保存门诊
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<int> Save(OutpatientAddStructure request)
        {
            if(request.Prescriptions == null || request.Prescriptions.Count < 1)
            {
                throw new Exception("请上传处方");
            }
            if(request.Prescriptions != null && request.Prescriptions.Count > 0)
            {
                foreach(var p in request.Prescriptions)
                {
                    if(p.Details != null && p.Details.Count > 0)
                    {
                        if (p.Details.Exists(w => w.GoodsDays < 0 || w.GoodsDays > 15))
                        {
                            var good = p.Details.Find(w => w.GoodsDays < 0 || w.GoodsDays > 15);
                            throw new MyException($"{good.GoodsName}的天数不能小于0或大于15");
                        }                            
                    }
                }                
            }
            BaseResponse<int> result = new BaseResponse<int>();
            if(request.OutpatientId == 0)
            {
               result.Data =  bll.AddStructure(request);
            }
            else
            {
                bll.UpdateStructure(request);
                result.Data = request.OutpatientId;
            }
            return result;
        }

        /// <summary>
        /// 查询挂单处方-列表-分页
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<OutpatientListModel>> getNoPaid(OutpatientSearchModel request)
        {
            BaseResponse<List<OutpatientListModel>> result = new BaseResponse<List<OutpatientListModel>>();
            var tuple =  bll.getNoPaid(request.TenantId, request.PageSize, request.PageIndex, request.searchKey, request.start, request.end);
            result.Data = tuple.Item1.ToList();
            result.CalcPage(tuple.Item2, request.PageIndex, request.PageSize);
            return result;
        }

        /// <summary>
        /// 查询历史处方-列表-分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<OutpatientListModel>> getHistory(OutpatientSearchModel request)
        {
            BaseResponse<List<OutpatientListModel>> result = new BaseResponse<List<OutpatientListModel>>();
            var tuple = bll.getHistory(request.TenantId, request.PageSize, request.PageIndex, request.searchKey, request.start, request.end);
            result.Data = tuple.Item1.ToList();
            result.CalcPage(tuple.Item2, request.PageIndex, request.PageSize);
            return result;
        }

        /// <summary>
        /// 获取具体一个门诊+处方详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<OutpatientAddStructure> getDetail(OutpatientStructureRequest request)
        {
            BaseResponse<OutpatientAddStructure> result = new BaseResponse<OutpatientAddStructure>();
            //result.Data = bll.getStructure(request.TenantId, request.OutpatientId);
            result.Data = bll.getStructure2(request.TenantId, request.OutpatientId);
            result.Data.OutpatientId = request.OutpatientId;
            return result;
        }

        /// <summary>
        /// 门诊收费
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<int> OutpatientCharge(OutpatientChargeModel request)
        {            
            BaseResponse<int> result = new BaseResponse<int>();
            if (request.Cashier == 0) return result.busExceptino(Enum.ErrorCode.业务逻辑错误, "需要传入Cashier表示收银员Id");
            try
            {
                result.Data = bll.Charge(request);
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
        /// 获取药品和材料列表-分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<GoodBllModels>> getsGoods(GoodsRequest request)
        {
            BaseResponse<List<GoodBllModels>> result = new BaseResponse<List<GoodBllModels>>();
            try
            {
                //（枚举1:西药,2:中成药,3:中药,4:诊疗项目,5:材料）
                if (request.GoodType == 1)
                {
                    request.GoodType = 7;
                }
                var tuple = goodsbll.getGoods(request.TenantId, request.PageSize, request.PageIndex, request.GoodType,0, request.SearchKey);
                if (tuple.Item2 == 0)
                {
                    return new BaseResponse<List<GoodBllModels>>();
                }
                result.Data = tuple.Item1.ToList().Mapping<GoodBllModels>();
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
        /// 收费管理时时获取医生下拉框
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<RegisterDoctor>> getDoctors(BaseModel request)
        {
            BaseResponse<List<RegisterDoctor>> result = new BaseResponse<List<RegisterDoctor>>();
            result.Data = regbll.getDoctors(request.TenantId);
            return result;
        }

        /// <summary>
        /// 模糊搜索租户患者信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<PatientModel>> searchPatient(PatientSearchPage request)
        {
            BaseResponse<List<PatientModel>> result = new BaseResponse<List<PatientModel>>();
            try
            {
                PatientPage page = new PatientPage();
                page.PageSize = request.PageSize;
                page.PageIndex = request.PageIndex;
                page.SearchKey = request.SearchKey;
                page.TenantId = request.TenantId;
                var tuple =  patbll.gets(page);
                result.Data = tuple.Item1;
                result.CalcPage(tuple.Item2,1, 1);
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
        /// 查找未被使用的挂号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<RegisterModel>> findRegister(Register1Request request)
        {
            BaseResponse<List<RegisterModel>> result = new BaseResponse<List<RegisterModel>>();
            try
            {
                var tuple = regbll.gets(request.TenantId, request.PageSize, request.PageIndex, request.SearchKey, request.start, request.end,request.DoctorId,-1);
                if(tuple.Item1 != null && tuple.Item1.Count > 0 )
                {
                    //获取医生Id和科室Id
                    var doctornames = regbll.getDoctorIds(tuple.Item1.First().TenantId, tuple.Item1.Select(x => x.DoctorName ?? "").ToList());
                    if (doctornames != null && doctornames.Any())
                    {
                        tuple.Item1.ToList().ForEach(x =>
                        {
                            if (doctornames.ToList().Find(y => y.EmployeeName == x.DoctorName) != null)
                            {
                                x.DoctorId = doctornames.ToList().Find(y => y.EmployeeName == x.DoctorName).EmployeeId;
                            }
                        });    
                    }
                }
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

        /// <summary>
        /// 获取具体一个门诊处方详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<OutpatientAddStructure> getPrescriptionDetail(PrescriptionDetailRequest request)
        {
            BaseResponse<OutpatientAddStructure> result = new BaseResponse<OutpatientAddStructure>();
            //result.Data = bll.getStructure(request.TenantId, request.OutpatientId);
            result.Data = bll.getStructure2(request.TenantId, request.OutpatientId);
            result.Data.OutpatientId = request.OutpatientId;
            return result;
        }

    }
} 