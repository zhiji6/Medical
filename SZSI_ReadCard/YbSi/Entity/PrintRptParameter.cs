using System;
using System.Collections.Generic;
using System.Text;

namespace MkSi.Entity
{
    public class PrintRptParameterList
    {
        public PrintRptParameter[] Data { get; set; }
    }
    public class PrintRptParameter: FormParameterBase
    {
        public string Zhmc { get; set; }
        public string LoadFromUrl { get; set; }
        public string LoadDataFromURL { get; set; }
    }
}
