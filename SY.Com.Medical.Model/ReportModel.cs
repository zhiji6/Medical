using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model
{
    /// <summary>
    /// 获取报表Model
    /// </summary>
    public class ReportModel
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
    /// 统计项目或药品明细
    /// </summary>
    public class ReportProjectModel
    {
        /// <summary>
        /// 医保编码
        /// </summary>
        public string InsuranceCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public long GoodsPrice { get; set; }
        /// <summary>
        /// 费用
        /// </summary>
        public long GoodsCost { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int GoodsNum { get; set; }
    }


    /// <summary>
    /// 获取时间段内机构所有收费记录
    /// </summary>
    public class AllChargeRequest : BaseModel
    {
        /// <summary>
        /// 最近几天,如最近3天
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// 开始日期,如果Days大于0则优先使用Days
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 记录类型枚举：门诊收费、门诊退费、挂号、退号
        /// </summary>
        public string ChargeType { get; set; }

    }

    /// <summary>
    /// 统计时间段内营收情况
    /// </summary>
    public class ReportRequest : BaseModel
    {
        /// <summary>
        /// 最近几天,如最近3天
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// 开始日期,如果Days大于0则优先使用Days
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndTime { get; set; }
    }

    /// <summary>
    /// 统计时间段内营收情况
    /// </summary>
    public class ReportProjectRequest : BaseModel
    {
        /// <summary>
        /// 最近几天,如最近3天
        /// </summary>        
        public int? Days { get; set; }
        /// <summary>
        /// 开始日期,如果Days大于0则优先使用Days
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 枚举类型：项目处方、中药处方、西药处方
        /// 可以不填
        /// </summary>       
        public string PreName { get; set; }
    }

}
