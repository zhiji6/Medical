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
    public class Dic 
	{
		private DicRepository db;
		public Dic()
		{
			db = new DicRepository();
		}

		public List<DicKeyValueModel> getValueByKey(int tenantid,string keyFirst,string keySecond,string searchKey)
        {			
			List<DicKeyValueModel> result = new List<DicKeyValueModel>();
			var entitys = db.getDic(tenantid, keyFirst, keySecond, searchKey);
			if(entitys == null || !entitys.Any())
            {
				return null;
            }
			entitys.ForEach(x =>
			{
				DicKeyValueModel mod = new DicKeyValueModel();
				mod.Id = x.DicId;
				mod.Value = x.DicValue;
				result.Add(mod);
			});
			return result;
		}

		public string getValueById(int tenantId, int id, string keyFirst, string keySecond)
        {
			return db.getValueById(tenantId, id, keyFirst, keySecond);
        }

		public int getIdByValue(int tenantId, string value, string keyFirst, string keySecond)
        {
			var result = db.getIdByValue(tenantId, value, keyFirst, keySecond);
			if(result == 0)
            {
				var id = Insert(tenantId, value, keyFirst, keySecond);
				return id;
			}
			return result;
        }

		public int Insert(int tenantId,string value,string keyFirst,string keySecond)
        {
			return db.Add(tenantId, value, keyFirst, keySecond);
        }

		public int Delete(int dicid)
        {			
			return db.Delete(dicid);
        }

	}
} 