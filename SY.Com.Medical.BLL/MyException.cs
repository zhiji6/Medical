using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL
{
    public class MyException : Exception
    {
        public MyException(string message) : base(message)
        {

        }
    }

    public class DataStateException : MyException
    {
        public DataStateException(string message) : base(message)
        {

        }
    }

    public class DataNotExists : MyException
    {
        public DataNotExists(string message) : base(message)
        {

        }
    }

    public class DataLogicFails : MyException
    {
        public DataLogicFails(string message) : base(message)
        {            
        }
    }

}
