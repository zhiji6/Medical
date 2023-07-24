using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Extension;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Clinic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
		public List<DiseaseModel> getsNoPage(List<string> names)
        {
			List<DiseaseModel> result = new List<DiseaseModel>();
			foreach(var name in names)
			{
                var resulttemp = db.getsNoPage(name);
                var datas = resulttemp?.EntityToDto<DiseaseModel>();
				if(datas != null)
				{
                    result.AddRange(datas.OrderBy(x => x.DiseaseName.Length).Take(10).ToList());
				}
			}
			return result.Distinct(new DiseaseComparer()).ToList();
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

	public class DiseaseComparer : IEqualityComparer<DiseaseModel>
	{
        public bool Equals(DiseaseModel x, DiseaseModel y)
		{
            if (x == null || y == null)
                return false;
            return x.DiseaseCode == y.DiseaseCode;
        }

        public int GetHashCode([DisallowNull] DiseaseModel obj)
		{
            if (obj == null)
                return 0;
            return obj.DiseaseCode.GetHashCode();
        }
    }
} 