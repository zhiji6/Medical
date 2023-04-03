using Dapper;
using SY.Com.Medical.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Repository
{
    public class ReportRepository
    {
        BaseRepository<ChargeRecordEntity> charge_repository = new BaseRepository<ChargeRecordEntity>("");
        /// <summary>
        /// 查询时间段内收入和门诊量
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ReportEntity TotalPrice(int tenantId,string startTime,string endTime)
        {
            string sql = @" 
                            Select Sum(Price) as Price
                            ,Sum(case when ChargeType = '门诊退费' then Price when ChargeType = '退号' then Price else 0 end) as PriceBack
                            ,Sum(case when ChargeType = '门诊收费' then 1 when ChargeType = '门诊退费' then -1  else 0 end) as Quantitys
                            ,Sum(PayYB) as PayYB,Sum(HifpPay) HifpPay,Sum(PayCash) as PayCash                            
                            From ChargeRecords
                            Where 1=1 And TenantId = @TenantId And CreateTime between @star And @end And IsEnable = 1 And IsDelete = 1 ";
            var mods = charge_repository._db.Query<ReportEntity>(sql, new { TenantId = tenantId, star = startTime, end= endTime });
            if (mods != null && mods.Any())
            {
                return mods.First();
            }
            return null;
        }


        /// <summary>
        /// 查询时间段内的医生的统计情况
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<ReportEntity> TotalDoctors(int tenantId, string startTime, string endTime)
        {
            
            string sql = @" 
	                            Select o.DoctorId
                                ,Sum(c.Price) as Price
                                ,Sum(case when c.ChargeType = '门诊退费' then c.Price when c.ChargeType = '退号' then c.Price else 0 end) as PriceBack
                                ,Sum(case when c.ChargeType = '门诊收费' then 1 when ChargeType = '门诊退费' then -1  else 0 end) as Quantitys
                                ,Sum(c.PayYB) as PayYB,Sum(c.HifpPay) HifpPay,Sum(c.PayCash) as PayCash   
	                            From ChargeRecords as c
	                            Inner Join Outpatients as o on c.SeeDoctorId = o.OutpatientId And c.TenantId = o.TenantId
	                            Where 1=1 And c.TenantId = @TenantId And c.CreateTime between @star And @end And c.IsEnable = 1 And c.IsDelete = 1 
	                            Group by o.DoctorId ";
            var mods = charge_repository._db.Query<ReportEntity>(sql, new { TenantId = tenantId, star = startTime, end = endTime });
            if (mods != null && mods.Any())
            {
                return mods.ToList();
            }
            return null;
        }

        /// <summary>
        /// 项目统计
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="PreName"></param>
        /// <returns></returns>
        public List<ReportProjectEntity> TotalProject(int tenantId, string startTime, string endTime,string PreName)
        {
            string where = "";
            if (!string.IsNullOrEmpty(PreName))
            {
                where = " And PreName = '" + PreName + "'  ";
            }
            string sql = @"
                            Select	p.InsuranceCode,p.GoodsName,Max(GoodsPrice) as GoodsPrice,Sum(GoodsCost) as GoodsCost,Sum(GoodsNum *  case when GoodsDays = 0 then 1 else GoodsDays end) as GoodsNum
                            From
                            (
	                            Select c.TenantId,c.SeeDoctorId
	                            From ChargeRecords as c
                                Where 1=1 And c.SeeDoctorId > 0 And c.TenantId = @TenantId And c.CreateTime between @star And @end And c.IsEnable = 1 And c.IsDelete = 1 
	                            group by c.TenantId,c.SeeDoctorId having(count(1) = 1)
                            )as c
                            Inner Join Prescriptions as p on c.SeeDoctorId = p.OutpatientId And c.TenantId = p.TenantId
                            Where 1=1 "+ where + @"
                            Group By p.InsuranceCode,p.GoodsName";
            var mods = charge_repository._db.Query<ReportProjectEntity>(sql, new { TenantId = tenantId, star = startTime, end = endTime });
            if (mods != null && mods.Any())
            {
                return mods.ToList();
            }
            return null;
        }

        /// <summary>
        /// 获取机构时间段内所有的收费记录
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<ChargeRecordEntity> getByTenantId(int tenantId, string startTime, string endTime,string chargeType)
        {
            string sql = @" Select * From ChargeRecords 
                            Where 1=1 And TenantId = @TenantId And CreateTime between @star And @end And IsEnable = 1 And IsDelete = 1 ";
            if(!string.IsNullOrEmpty(chargeType))
            {
                sql += " And ChargeType = '"+ chargeType +"' ";
            }
            var mods = charge_repository._db.Query<ChargeRecordEntity>(sql, new { TenantId = tenantId, star = startTime, end = endTime });
            if (mods != null && mods.Any())
            {
                return mods.ToList();
            }
            return null;
        }
    }
}
