using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL
{
    public static class SystemEnvironment
    {
        public static string GetRootDirector()
        {
            return AppContext.BaseDirectory;
            //return System.Environment.CurrentDirectory + "/wwwroot/";
        }
    }
}
