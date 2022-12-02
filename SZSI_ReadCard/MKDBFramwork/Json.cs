using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MKDBFramwork
{
    public class Json
    {
        public static string GetJsonFromObject<T>(T clsObject)
        {
            string result = "";
            try
            {
                var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                result = JsonConvert.SerializeObject(clsObject, Formatting.None, jSetting);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static T GetObjectFromJson<T>(object json)
        {
            T result;
            string temp = "";
            try
            {
                if (json is Newtonsoft.Json.Linq.JObject)
                {
                    temp = JsonConvert.SerializeObject(json);
                }
                else
                {
                    temp = json.ToString();
                }
                var jSetting = new JsonSerializerSettings { Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore };
                result = JsonConvert.DeserializeObject<T>(temp, jSetting);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
