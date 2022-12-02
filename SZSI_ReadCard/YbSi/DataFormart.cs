using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKDBFramwork
{
    public static class DataFormart
    { 

        /// <summary>
        /// 获取当前日期格式
        /// </summary>
        /// <param name="key">YYYYMMDD,yyyyMMddHHmmss,YYYYMM</param>
        /// <returns></returns>
        public static ReturnData<string> DateFormart(dateformart key)
        {
            ReturnData<string> rd = new ReturnData<string>();
            rd.Data = DateTime.Now.ToString(key.ToString());
            return rd;
        }

        /// <summary>
        /// 获取日期时间的对应格式
        /// </summary>
        /// <param name="dt">需要使用的日期时间</param>
        /// <param name="key">YYYYMMDD,yyyyMMddHHmmss,YYYYMM</param>
        /// <returns></returns>
        public static ReturnData<string> DateFormart(DateTime dt, dateformart key)
        {
            ReturnData<string> rd = new ReturnData<string>();
            rd.Data = dt.ToString(key.ToString());
            return rd;
        }

        /// <summary>
        /// 获取日期时间的对应格式
        /// </summary>
        /// <param name="sdt">需要使用的日期时间</param>
        /// <param name="key">YYYYMMDD,yyyyMMddHHmmss,YYYYMM</param>
        /// <returns></returns>
        public static ReturnData<string> DateFormart(string sdt, dateformart key)
        {
            ReturnData<string> rd = new ReturnData<string>();
            DateTime dt = DateTime.Now;
            if (DateTime.TryParse(sdt, out dt))
            {
                rd.Data = dt.ToString(key.ToString());
            }
            else
            {
                rd.ErrorInfos("格式错误", ConstErrorCode.DataFormatIsError);
            }
            return rd;
        }

        /// <summary>
        /// 获取浮点数类型保留小数的方法
        /// </summary>
        /// <param name="snum">需要转换的源字符串</param>
        /// <param name="len">保留小数位数</param>
        /// <param name="round">进位方法</param>
        /// <returns></returns>
        public static ReturnData<double> NumberFormart(string snum, int len, rounding round = rounding.Round)
        {
            ReturnData<double> rd = new ReturnData<double>();
            double num = 0.00;
            if (double.TryParse(snum, out num))
            {
                switch (round)
                {
                    case rounding.Round: rd.Data = Math.Round(num, len); break;
                    case rounding.Floor: rd.Data = Math.Floor(num); break;
                    case rounding.Ceiling: rd.Data = Math.Ceiling(num); break;
                    default: rd.Data = Math.Round(num, len); break;
                }
            }
            else
            {
                rd.ErrorInfos("格式错误", ConstErrorCode.DataFormatIsError);
            }
            return rd;
        }


        /// <summary>
        /// 获取浮点数类型保留小数的方法
        /// </summary>
        /// <param name="num">源</param>
        /// <param name="len">保留小数位数</param>
        /// <param name="round">进位方法</param>
        /// <returns></returns>
        public static ReturnData<double> NumberFormart(double num, int len, rounding round = rounding.Round)
        {
            ReturnData<double> rd = new ReturnData<double>();
            switch (round)
            {
                case rounding.Round: rd.Data = Math.Round(num, len); break;
                case rounding.Floor: rd.Data = Math.Floor(num); break;
                case rounding.Ceiling: rd.Data = Math.Ceiling(num); break;
                default: rd.Data = Math.Round(num, len); break;
            }
            return rd;
        }

        /// <summary>
        /// 获取浮点数类型保留小数的方法
        /// </summary>
        /// <param name="num"></param>
        /// <param name="len"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public static ReturnData<double> NumberFormart(decimal num, int len, rounding round = rounding.Round)
        {
            ReturnData<double> rd = new ReturnData<double>();
            double dnum = (double)num;
            switch (round)
            {
                case rounding.Round: rd.Data = Math.Round(dnum, len); break;
                case rounding.Floor: rd.Data = Math.Floor(dnum); break;
                case rounding.Ceiling: rd.Data = Math.Ceiling(dnum); break;
                default: rd.Data = Math.Round(dnum, len); break;
            }
            return rd;
        }

        /// <summary>
        /// 获取浮点数类型保留小数的方法
        /// </summary>
        /// <param name="num"></param>
        /// <param name="len"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public static ReturnData<double> NumberFormart(int num, int len, rounding round = rounding.Round)
        {
            ReturnData<double> rd = new ReturnData<double>();
            double dnum = num;
            switch (round)
            {
                case rounding.Round: rd.Data = Math.Round(dnum, len); break;
                case rounding.Floor: rd.Data = Math.Floor(dnum); break;
                case rounding.Ceiling: rd.Data = Math.Ceiling(dnum); break;
                default: rd.Data = Math.Round(dnum, len); break;
            }
            return rd;
        }


        public enum rounding
        {
            Round = 0,
            Ceiling = 1,
            Floor = 2
        }

        public enum dateformart
        {
            yyyyMMdd = 0,
            yyyyMMddHHmmss = 1,
            YYYYMM = 2,
            yyyyMMddHHmmssSSS = 3,
            HHmmss = 4
        }

    }
}
