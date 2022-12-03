using Client.Model;
using System;
using System.Collections.Concurrent;
using System.Web.Http;

namespace Client.Controllers
{
    public class YBController : ApiController
    {

        public static ConcurrentDictionary<string, string> ylzhlist = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 读取医保卡
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public string Get(string guid)
        {
            try
            {
                //return new ReturnData<string>() { Data = "3423423894320" };
                string ybkh = "";//"%GAAFSAKSXSUKKWDKHDAD?;07734724145330238292?";
                if (ylzhlist.TryRemove(guid, out ybkh))
                {
                    return ybkh;
                }
                return "";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "";

        }

        /// <summary>
        /// 更新医保卡
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="ybkh"></param>
        /// <returns></returns>
        [HttpGet]
        public ReturnData<string> UpdateCardInfo(string guid,string ybkh)
        {
            LogHelper.Debug($"guid:{guid},ybka:{ybkh}");
            ylzhlist.TryAdd(guid, ybkh);
            return new ReturnData<string>();
        }


        public string Options()
        {
            return null; // HTTP 200 response with empty body
        }

        [HttpPost]
        public string Message9001()
        {
            return "";
        }


    }
}
