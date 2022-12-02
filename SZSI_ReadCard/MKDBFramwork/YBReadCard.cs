using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Configuration;
using SSCARDDRIVEROCXLib;
using System.Reflection;

namespace MKDBFramwork
{
    public class YBReadCard
    {
        [DllImport(@"SSCardDriver_SZ.dll")]        

        private static extern long iVerifyCode(string inParams, [Out]byte[] pOutInfo);

        [DllImport(@"SSCardDriver_SZ.dll")]
        private static extern long iReadCardBas(int iType, string inParams, [Out]byte[] pOutInfo);
        public string GetCardInfo(string method, string Flag)
        {
            cardModel cardInfo = new cardModel();
            switch (method)
            {
                case "readcard": cardInfo = getReadCardBas(Flag); break;
                case "iveryficode": cardInfo = getVerifyCode(Flag); break;
            }
            if (cardInfo == null)
                return "";
            return Json.GetJsonFromObject<cardModel>(cardInfo);
        }

        /// <summary>
        /// 读卡
        /// </summary>
        /// <returns></returns>
        public cardModel getReadCardBas(string Flag)
        {
            cardModel outResult = null;

            int inType = int.Parse(ConfigurationManager.AppSettings["cardType"].ToString());
            string YLJGBM = ConfigurationManager.AppSettings["YLJGBM"].ToString();
            string serialNumber = YLJGBM + "0" + DataFormart.DateFormart(DataFormart.dateformart.yyyyMMddHHmmss).Data;
            string inParameter = (string.IsNullOrEmpty(Flag) ? "XX001" : Flag) + "|" + serialNumber + "|" + YLJGBM + "|";
            try
            {
                SSCARD szcard = new SSCARD();
                var result = szcard.iReadCardBas(inType, inParameter);
                if (result.ToString().Trim().Equals("0"))
                {
                    outResult = new cardModel();
                    string[] sps = szcard.pOutInfo.Split('|');
                    /*
                 	发卡地区行政区划代码（卡识别码前6位）|社会保障号码|卡号|卡识别码|姓名|卡复位信息（仅取历史字节）|规范版本|
                    发卡日期|卡有效期|终端机编号|终端设备号|PSAM卡芯片号|医疗证号|加密因子|版本号|
示例：639900|111111198101011110|X00000019|639900D15600000500BF7C7A48FB4966|张三|00814E43238697159900BF7C7A|1.00|20101001|20201001|
410100813475|MT170701000006|3B7D940000008150220886431E10C9022736|%GALJGMJKASUKZIUNIHKD?;07890592745347567832?| 5f2ab04d6d289495b7b14ec9a9602a23|V0.1|    
                 */
                    outResult.verifyCode = sps[11] + "|" + sps[13];
                    outResult.transVersion = sps[14];
                    outResult.aaz500 = sps[12];
                    outResult.cardArea = sps[0];
                    outResult.serialNumber = serialNumber;
                }

            }
            catch (Exception ep)
            {
                throw ep;
            }
            return outResult;
        }

        /// <summary>
        /// 获取读卡器验证消息
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public cardModel getVerifyCode(string flag)
        {
            cardModel outResult = null;
            string YLJGBM = ConfigurationManager.AppSettings["YLJGBM"].ToString();
            string serialNumber = YLJGBM + "0" + DataFormart.DateFormart(DataFormart.dateformart.yyyyMMddHHmmss).Data;
            string inParameter = flag + "|" + serialNumber + "|" + YLJGBM + "|";
            try
            {
                SSCARD szcard = new SSCARD();
                var result = szcard.iVerifyCode(inParameter);
                if (result.ToString().Trim().Equals("0"))
                {
                    outResult = new cardModel();
                    string[] sps = szcard.pOutInfo.Split('|');
                    /*
                        * PSAM卡芯片号|加密因子|版本号|
                        示例：
                        000000|5c3a7b86d81de0cac3f0892760f15442|V0.1|*/
                    outResult.verifyCode = sps[0] + "|" + sps[1];
                    outResult.transVersion = sps[2];
                    outResult.serialNumber = serialNumber;
                }
            }
            catch (Exception ep)
            {
                throw ep;
            }
            return outResult;
        }

        public class cardModel
        {
            public string aaz500 { get; set; }
            public string bzz269 { get; set; }
            public string transVersion { get; set; }
            public string verifyCode { get; set; }

            public string cardArea { get; set; }

            public string serialNumber { get; set; }
        }





    }
}
