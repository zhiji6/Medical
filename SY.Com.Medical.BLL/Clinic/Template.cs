using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.BLL.Platform;
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
		private Employee ebll = new Employee();
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
			result.EmployeeName = ebll.getEmployee(result.EmployeeId).EmployeeName;
			result.Prescription = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PrescriptionAddStructure>>(result.Content);			
			return result;
		}
		///<summary> 
		///获取列表-分页
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public Tuple<List<TemplateModel>,int> gets(TemplateRequest request)
		{
			var datas = db.gets(request);			
			if (datas != null && datas.Item1.Count > 0)
            {
				var result = datas.Item1.EntityToDto<TemplateModel>();
				foreach(var item in result)
                {
					item.Prescription = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PrescriptionAddStructure>>(item.Content);
					if(item.Prescription != null && item.Prescription.Any())
                    {
						item.Prescription = item.Prescription.Where(w => w.Details != null && w.Details.Count > 0)?.ToList() ?? new List<PrescriptionAddStructure>();
                    }
					item.WestCount = item.Prescription.Where(w => w.PreName == "西药处方")?.Count() ?? 0;
					item.EastCount = item.Prescription.Where(w => w.PreName == "中药处方")?.Count() ?? 0;
					item.ProjectCount = item.Prescription.Where(w => w.PreName == "项目处方")?.Count() ?? 0;
					item.EmployeeName = ebll.getEmployee(item.EmployeeId).EmployeeName;					
				}
				return new Tuple<List<TemplateModel>, int>(result, datas.Item2);
            }
			return null;
		}
		///<summary> 
		///新增
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public int add(TemplateAdd request)
		{
			TemplateEntity entity = new TemplateEntity();
			entity.EmployeeId = request.EmployeeId;
			entity.TemplateName = request.TemplateName;
			entity.TemplateType = request.TemplateType == "1" ? "私有" : "公开";
			entity.Content = Newtonsoft.Json.JsonConvert.SerializeObject(request.Prescription);
			entity.WestCount = request.Prescription.Where(x => x.PreName == "西药处方")?.ToList().Count ?? 0;
			entity.EastCount = request.Prescription.Where(x => x.PreName == "中药处方")?.ToList().Count ?? 0;
			entity.ProjectCount = request.Prescription.Where(x => x.PreName == "项目处方")?.ToList().Count ?? 0;
			entity.TenantId = request.TenantId;
			entity.IsDelete = Enum.Delete.正常;
			entity.IsEnable = Enum.Enable.启用;
			return db.Create(entity);
			//request.TemplateType = request.TemplateType == "1" ? "私有" : "公开";			
			//request.Content = Newtonsoft.Json.JsonConvert.SerializeObject(request.Prescription);
			//return db.Create(request.DtoToEntity<TemplateEntity>());
		}
		///<summary> 
		///修改
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public void update(TemplateUpdate request)
		{			
			var entity = db.Get(request.TemplateId);
			//entity.TemplateId = request.TemplateId;
			//entity.EmployeeId = request.EmployeeId;
			entity.EmployeeId = request.EmployeeId;
			entity.TemplateName = request.TemplateName;
			entity.TemplateType = request.TemplateType == "1" ? "私有" : "公开";
			entity.Content = Newtonsoft.Json.JsonConvert.SerializeObject(request.Prescription);
			entity.WestCount = request.Prescription.Where(x => x.PreName == "西药处方")?.ToList().Count ?? 0;
			entity.EastCount = request.Prescription.Where(x => x.PreName == "中药处方")?.ToList().Count ?? 0;
			entity.ProjectCount = request.Prescription.Where(x => x.PreName == "项目处方")?.ToList().Count ?? 0;
			db.Update(entity);
			//var mod = request.DtoToEntity<TemplateEntity>();
			//request.TemplateType = request.TemplateType == "1" ? "私有" : "公开";
			//mod.Content = Newtonsoft.Json.JsonConvert.SerializeObject(request.Prescription);
			//db.Update(mod);
		}
		///<summary> 
		///删除
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public void delete(TemplateDelete request)
		{
			TemplateEntity entity = db.GetDelete(request.TemplateId);
			db.Delete(entity);
		}
	}
} 