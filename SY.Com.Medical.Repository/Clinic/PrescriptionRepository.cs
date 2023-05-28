using Dapper;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SY.Com.Medical.Repository.Clinic
{
    /// <summary>
    /// 数据访问层
    /// </summary>
    public class PrescriptionRepository : BaseRepository<PrescriptionEntity> 
	{ 
        /// <summary>
        /// 根据门诊ID查询处方信息
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="outpatientId"></param>
        /// <returns></returns>
        public List<PrescriptionEntity> getByOutpatientId(int tenantId,int outpatientId)
        {
            string sql = @" Select * From Prescriptions Where TenantId=@TenantId And OutpatientId=@OutpatientId ";
            var entitys = _db.Query<PrescriptionEntity>(sql, new { TenantId = tenantId, OutpatientId = outpatientId });
            if(entitys == null || !entitys.Any())
            {
                return null;
            }
            return entitys.ToList();
        }

        /// <summary>
        /// 插入处方
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int InsertBulk(List<PrescriptionEntity> mod)
        {
            if(mod == null || mod.Count < 1)
            {
                return 0;
            }
            string sql = @" Insert Into Prescriptions(PrescriptionId,TenantId,OutpatientId,PreNo,PreName,GoodsId,GoodsName
                                                      ,GoodsNorm,GoodsPrice,GoodsNum,GoodsCost,GoodsUsage,GoodsEveryDay,GoodsDays
                                                      ,GoodsSalesUnit,Pair,Place,InsuranceCode,CustomerCode,IsEnable,IsDelete,CreateTime,Remark)
                            Values(@PrescriptionId,@TenantId,@OutpatientId,@PreNo,@PreName,@GoodsId,@GoodsName
                                                      ,@GoodsNorm,@GoodsPrice,@GoodsNum,@GoodsCost,@GoodsUsage,@GoodsEveryDay,@GoodsDays
                                                      ,@GoodsSalesUnit,@Pair,@Place,@InsuranceCode,@CustomerCode,1,1,getdate(),@Remark) ";
            var maxid = getID("Prescriptions", mod.Count);
            foreach (var item in mod)
            {
                item.PrescriptionId = maxid;
                maxid = maxid - 1;
            }
            return _db.Execute(sql, mod);
        }

        /// <summary>
        /// 修改处方
        /// 先删除-再插入
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public int UpdateBulk(List<PrescriptionEntity> mod)
        {
            if(mod == null || mod.Count < 1)
            {
                return 0;
            }
            //删除旧的,插入新的
            string sql = @" Delete From Prescriptions Where TenantId=@TenantId And OutpatientId=@OutpatientId ";
            var deletecount = _db.Execute(sql, new { TenantId = mod[0].TenantId, OutpatientId = mod[0].OutpatientId });
            return InsertBulk(mod);
        }


	}
} 