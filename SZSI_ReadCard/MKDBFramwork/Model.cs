using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MKDBFramwork
{
    public static class Model
    {
        public class YBYPMLOption
        {
            public string MC { get; set; }
	        public int DownLoadCount { get; set; }
	        public int DownLoadPage { get; set; }
	        public string DownLoadDate { get; set; }
	        public string Remark { get; set; }
	        public string Remark1 { get; set; }
        }
    }
}
