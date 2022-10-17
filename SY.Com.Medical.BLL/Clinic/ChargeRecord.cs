using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Extension;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SY.Com.Medical.BLL.Clinic
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public class ChargeRecord 
	{
		private ChargeRecordRepository db;
		public ChargeRecord()
		{
			db = new ChargeRecordRepository();
		}

		/// <summary>
		/// 获取收费
		/// </summary>
		/// <param name="tenantId"></param>
		/// <param name="outpatientId"></param>
		/// <returns></returns>
		public ChargeRecordEntity getByOutpatientId(int tenantId, int outpatientId,string chargetype = "门诊收费")
		{
			return db.getByOutpatientId(tenantId, outpatientId,chargetype);
        }


		/// <summary>
		/// 获取收费-批量
		/// </summary>
		/// <param name="tenantId"></param>
		/// <param name="outpatientIds"></param>
		/// <param name="chargetype"></param>
		/// <returns></returns>
		public List<ChargeRecordEntity> getByOutpatientIds(int tenantId, List<int> outpatientIds, string chargetype = "门诊收费")
		{
			if (outpatientIds == null || outpatientIds.Count < 1) return null;
			return db.getByOutpatientIds(tenantId, outpatientIds, chargetype);
		}
		

		///<summary> 
		///获取详情记录
		///</summary> 
		///<param name="id"></param>
		/// <returns></returns>
		public ChargeRecordModel get(int id)
		{
			return db.Get(id).EntityToDto<ChargeRecordModel>();
		}
		///<summary> 
		///获取列表-分页
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public Tuple<IEnumerable<ChargeRecordModel>,int> gets(ChargeRecordRequest request)
		{
			var datas  = db.GetsPage(request.DtoToEntity<ChargeRecordEntity>(), request.PageSize, request.PageIndex);
			Tuple<IEnumerable<ChargeRecordModel>, int> result = new(datas.Item1.EntityToDto<ChargeRecordModel>(), datas.Item2);
			return result;
		}
		///<summary> 
		///新增
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public int add(ChargeRecordEntity request)
		{
			return db.Create(request);
		}
		///<summary> 
		///修改
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public void update(ChargeRecordUpdate request)
		{
			db.Update(request.DtoToEntity<ChargeRecordEntity>());
		}
		///<summary> 
		///删除
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public void delete(ChargeRecordDelete request)
		{
			db.Delete(request.DtoToEntity<ChargeRecordEntity>());
		}
	}
} 