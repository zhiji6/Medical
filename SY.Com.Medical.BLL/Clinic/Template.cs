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
    public class Template 
	{
		private TemplateRepository db;
		public Template()
		{
			db = new TemplateRepository();
		}
		///<summary> 
		///获取详情记录
		///</summary> 
		///<param name="id"></param>
		/// <returns></returns>
		public TemplateModel get(int id)
		{
			var result = db.Get(id).EntityToDto<TemplateModel>();
			result.Prescription = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PrescriptionAddStructure>>(result.Content);			
			return result;
		}
		///<summary> 
		///获取列表-分页
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public Tuple<IEnumerable<TemplateModel>,int> gets(TemplateRequest request)
		{
			var datas  = db.GetsPage(request.DtoToEntity<TemplateEntity>(), request.PageSize, request.PageIndex);
			Tuple<IEnumerable<TemplateModel>, int> result = new(datas.Item1.EntityToDto<TemplateModel>(), datas.Item2);
			result.Item1.ToList().ForEach(x => x.Prescription = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PrescriptionAddStructure>>(x.Content));
			return result;
		}
		///<summary> 
		///新增
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public int add(TemplateAdd request)
		{
			request.Content = Newtonsoft.Json.JsonConvert.SerializeObject(request.Prescription);
			return db.Create(request.DtoToEntity<TemplateEntity>());
		}
		///<summary> 
		///修改
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public void update(TemplateUpdate request)
		{
			var mod = request.DtoToEntity<TemplateEntity>();
			mod.Content = Newtonsoft.Json.JsonConvert.SerializeObject(request.Prescription);
			db.Update(mod);
		}
		///<summary> 
		///删除
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public void delete(TemplateDelete request)
		{
			db.Delete(request.DtoToEntity<TemplateEntity>());
		}
	}
} 