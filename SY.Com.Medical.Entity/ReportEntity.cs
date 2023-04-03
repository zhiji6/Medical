using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Entity
{
    /// <summary>
    /// ReportEntity不对应实体表,是表的统计
    /// </summary>
    public class ReportEntity
    {
        /// <summary>
        /// 总费用
        /// </summary>
        public long Price { get; set; }
        /// <summary>
        /// 医保支付
        /// </summary>
        public long PayYB { get; set; }
        /// <summary>
        /// 统筹支付
        /// </summary>
        public long HifpPay { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        public long PayCash { get; set; }
        /// <summary>
        /// 退费金额
        /// </summary>
        public long PriceBack { get; set; }
        /// <summary>
        /// 门诊人数
        /// </summary>
        public long Quantitys { get; set; }        
        /// <summary>
        /// 医生Id
        /// </summary>
        public int DoctorId { get; set; }
    }

    /// <summary>
    /// 不是真实实体
    /// </summary>
    public class ReportProjectEntity
    {
        public string InsuranceCode { get; set; }
        public string GoodsName { get; set; }
        public long GoodsPrice { get; set; }
        public long GoodsCost { get; set; }
        public int GoodsNum { get; set; }
    }

}
