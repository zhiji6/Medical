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
    public class CaseBookTemplate 
	{
		private CaseBookTemplateRepository db;
		public CaseBookTemplate()
		{
			db = new CaseBookTemplateRepository();
		}
		///<summary> 
		///获取详情记录
		///</summary> 
		///<param name="id"></param>
		/// <returns></returns>
		public CaseBookTemplateModel get(int id)
		{
			return db.Get(id).EntityToDto<CaseBookTemplateModel>();
		}
		///<summary> 
		///获取列表-分页
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public Tuple<IEnumerable<CaseBookTemplateModel>,int> gets(CaseBookTemplateRequest request)
		{
			var datas  = db.GetsPage(request.DtoToEntity<CaseBookTemplateEntity>(), request.PageSize, request.PageIndex);
			Tuple<IEnumerable<CaseBookTemplateModel>, int> result = new(datas.Item1.EntityToDto<CaseBookTemplateModel>(), datas.Item2);
			return result;
		}
		///<summary> 
		///新增
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public int add(CaseBookTemplateAdd request)
		{
			return db.Create(request.DtoToEntity<CaseBookTemplateEntity>());
		}
		///<summary> 
		///修改
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public void update(CaseBookTemplateUpdate request)
		{
			db.Update(request.DtoToEntity<CaseBookTemplateEntity>());
		}
		///<summary> 
		///删除
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public void delete(CaseBookTemplateDelete request)
		{
			db.Delete(request.DtoToEntity<CaseBookTemplateEntity>());
		}
	}
} 