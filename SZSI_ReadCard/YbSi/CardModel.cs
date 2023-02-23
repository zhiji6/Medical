using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBSi
{
    public class CardModel
    {
        public int result { get; set; }//0成功，非0失败
        public string method { get; set; }
        public string cardinfo { get; set; }
        public string sign { get; set; }
        public string err_msg { get; set; } 
    }
}
