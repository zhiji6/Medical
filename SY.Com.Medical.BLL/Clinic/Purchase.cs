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
    public class Purchase 
	{
		private PurchaseRepository db;
		public Purchase()
		{
			db = new PurchaseRepository();
		}
		///<summary> 
		///获取详情记录
		///</summary> 
		///<param name="id"></param>
		/// <returns></returns>
		public PurchaseModel get(int id)
		{
			var x = db.Get(id);
			PurchaseModel model = new PurchaseModel();
			model.PurchaseId = x.PurchaseId;
			model.GoodsCount = x.GoodsCount;
			model.GoodsMoney = Math.Round(x.GoodsMoney/1000.00,3); ///Math.Round(x.PurchasePrice / 1000.00, 3);
			model.CreateTime = x.CreateTime.Value;
			return model;
		}
		///<summary> 
		///获取列表-分页
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public Tuple<IEnumerable<PurchaseModel>,int> gets(PurchaseRequest request)
		{
			var datas  = db.page(request.TenantId, request.PageSize, request.PageIndex,request.start,request.end,request.GoodName);
			List<PurchaseModel> list = new List<PurchaseModel>();
			datas.Item1.ToList().ForEach(x =>
			{
				PurchaseModel model = new PurchaseModel();
				model.PurchaseId = x.PurchaseId;
				model.GoodsCount = x.GoodsCount;
				model.GoodsMoney = Math.Round(x.GoodsMoney/1000.00,3);
				model.CreateTime = x.CreateTime.Value;
				list.Add(model);
			});
			Tuple<IEnumerable<PurchaseModel>, int> result = new(list, datas.Item2);
			return result;
		}
	
	}
} 