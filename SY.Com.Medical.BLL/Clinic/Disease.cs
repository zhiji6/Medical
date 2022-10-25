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
    public class Disease 
	{
		private DiseaseRepository db;
		public Disease()
		{
			db = new DiseaseRepository();
		}
		///<summary> 
		///获取详情记录
		///</summary> 
		///<param name="id"></param>
		/// <returns></returns>
		public DiseaseModel get(int id)
		{
			return db.Get(id).EntityToDto<DiseaseModel>();
		}


		/// <summary>
		/// 根据名称搜索疾病
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public List<DiseaseModel> getsNoPage(string name)
        {
			var result = db.getsNoPage(name);
			var datas = result?.EntityToDto<DiseaseModel>();
			return datas?.OrderBy(x => x.DiseaseName.Length).Take(10)?.ToList();
		}

		///<summary> 
		///新增
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public int add(DiseaseAdd request)
		{
			return db.Create(request.DtoToEntity<DiseaseEntity>());
		}
		///<summary> 
		///修改
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public void update(DiseaseUpdate request)
		{
			db.Update(request.DtoToEntity<DiseaseEntity>());
		}
		///<summary> 
		///删除
		///</summary> 
		///<param name="request"></param>
		/// <returns></returns>
		public void delete(DiseaseDelete request)
		{
			db.Delete(request.DtoToEntity<DiseaseEntity>());
		}
	}
} 