using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SY.Com.Medical.WebApi.Controllers.Clinic
{
    /// <summary>
    /// 报表接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [Api_Tenant]    
    public class ReportController : ControllerBase
    {
        Report report_bll = new Report();
        /// <summary>
        /// 获取时间段内诊所的所有收费记录
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<ChargeRecordModel>> GetAllCharge(AllChargeRequest mod)
        {
            BaseResponse<List<ChargeRecordModel>> result = new BaseResponse<List<ChargeRecordModel>>();
            string starttime = mod.StartTime;
            string endtime = mod.EndTime;
            if(mod.Days > 0)
            {
                int intval = mod.Days - 1;
                starttime = DateTime.Now.AddDays(-intval).ToString("yyyy-MM-dd") + " 00:00:00";
                endtime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            if (string.IsNullOrEmpty(starttime))
            {
                starttime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            }
            if (string.IsNullOrEmpty(endtime))
            {
                endtime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            result.Data = report_bll.getByTenantId(mod.TenantId, starttime, endtime, mod.ChargeType);
            return result;
        }

        /// <summary>
        /// 统计一段时间内机构营收情况
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<ReportModel> TotalPrice(ReportRequest mod)
        {
            BaseResponse<ReportModel> result = new BaseResponse<ReportModel>();
            string starttime = mod.StartTime;
            string endtime = mod.EndTime;
            if (mod.Days > 0)
            {
                int intval = mod.Days - 1;
                starttime = DateTime.Now.AddDays(-intval).ToString("yyyy-MM-dd") + " 00:00:00";
                endtime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            if (string.IsNullOrEmpty(starttime))
            {
                starttime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            }
            if (string.IsNullOrEmpty(endtime))
            {
                endtime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            result.Data =  report_bll.TotalPrice(mod.TenantId, starttime, endtime);
            return result;
        }

        /// <summary>
        /// 统计一段时间内的项目信息
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<ReportProjectModel>> TotalProject(ReportProjectRequest mod)
        {
            BaseResponse<List<ReportProjectModel>> result = new BaseResponse<List<ReportProjectModel>>();
            string starttime = mod.StartTime;
            string endtime = mod.EndTime;
            if (mod.Days != null && mod.Days.Value > 0)
            {
                int intval = mod.Days.Value - 1;
                starttime = DateTime.Now.AddDays(-intval).ToString("yyyy-MM-dd") + " 00:00:00";
                endtime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            if (string.IsNullOrEmpty(starttime))
            {
                starttime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            }
            if (string.IsNullOrEmpty(endtime))
            {
                endtime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            result.Data = report_bll.TotalProject(mod.TenantId, starttime, endtime,mod.PreName);
            return result;
        }

        /// <summary>
        /// 统计一段时间内医生营收信息
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<ReportModel>> TotalDoctors(ReportRequest mod)
        {
            BaseResponse<List<ReportModel>> result = new BaseResponse<List<ReportModel>>();
            string starttime = mod.StartTime;
            string endtime = mod.EndTime;
            if (mod.Days > 0)
            {
                int intval = mod.Days - 1;
                starttime = DateTime.Now.AddDays(-intval).ToString("yyyy-MM-dd") + " 00:00:00";
                endtime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            if (string.IsNullOrEmpty(starttime))
            {
                starttime = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            }
            if (string.IsNullOrEmpty(endtime))
            {
                endtime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            result.Data = report_bll.TotalDoctors(mod.TenantId, starttime, endtime);
            return result;
        }


    }
}
