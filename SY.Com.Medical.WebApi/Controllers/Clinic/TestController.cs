using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.BLL.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SY.Com.Medical.WebApi.Controllers.Clinic
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 测试科室
        /// </summary>
        /// <param name="kename"></param>
        /// <returns></returns>
        [HttpGet]
        public string getKS(string kename)
        {
            return SYBKS.getYBDepartCode(kename);
        }
    }
}
