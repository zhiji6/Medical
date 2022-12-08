using Client.Model;
using System;
using System.Collections.Concurrent;
using System.Configuration;
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
                string isdebug = ConfigurationManager.AppSettings["Debug"] ?? "0";
                if (isdebug == "1")
                {
                    //return "{ \"result\":1,\"method\":\"ReadCardBas\",\"cardinfo\":\"440300|445222198709062439|BCH826720|440300D156000005006CFEED8AA783F6|陈力生|0081544C9286844403006CFEED|2.00|20180416|20280416|000000000000|00010601202211000461||\",\"sign\":\"\",\"err_msg\":\"\"}";
                    return "{ \"result\":0,\"method\":\"ReadCardBas\",\"cardinfo\":\"440300|420528198310290065|BCH826720|440300D156000005006CFEED8AA783F6|陈力生|0081544C9286844403006CFEED|2.00|20180416|20280416|000000000000|00010601202211000461||\",\"sign\":\"\",\"err_msg\":\"\"}";
                }
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
                LogHelper.Debug(ex);
                return "";
            }

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
