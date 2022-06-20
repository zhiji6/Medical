using Dapper;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SY.Com.Medical.Repository.Clinic
{
    /// <summary>
    /// 数据访问层
    /// </summary>
    public class DicRepository : BaseRepository<DicEntity> 
	{ 
        public List<DicEntity> getDic(int tenantId,string keyFirst)
        {
            return getDic(tenantId, keyFirst, null, null);
        }

        public List<DicEntity> getDic(int tenantId, string keyFirst,string keySecond)
        {
           return getDic(tenantId, keyFirst, keySecond, null);
        }

        public List<DicEntity> getDic(int tenantId, string keyFirst,string keySecond,string searchKey)
        {
            string sql = " Select * From Dics Where IsEnable=1 And IsDelete = 1 And (TenantId = 0 Or TenantId = @TenantId ) ";
            string sqlwhere = "";
            if(!string.IsNullOrEmpty(keyFirst))
            {
                sqlwhere += " And KeyFirst= '"+ keyFirst +"' ";
            }
            if (!string.IsNullOrEmpty(keySecond))
            {
                sqlwhere += " And KeySecond= '"+ keySecond +"' ";
            }
            if (!string.IsNullOrEmpty(searchKey))
            {
                sqlwhere += " And SearchKey like '%"+ searchKey +"%' ";
            }
            sql = sql + sqlwhere;
            var mod = _db.Query<DicEntity>(sql, new { TenantId = tenantId });
            if (mod != null && mod.Any())
            {
                return mod.ToList();
            }
            return null;
        }

        public string getValueById(int tenantId, int id, string keyFirst,string keySecond)
        {
            string sql = " Select DicValue From Dics Where IsEnable=1 And IsDelete = 1 And (TenantId = 0 Or TenantId = @TenantId ) ";
            string sqlwhere = "";
            if (!string.IsNullOrEmpty(keyFirst))
            {
                sqlwhere += " And KeyFirst= '" + keyFirst + "' ";
            }
            if (!string.IsNullOrEmpty(keySecond) && keySecond != "无")
            {
                sqlwhere += " And KeySecond= '" + keySecond + "' ";
            }
            if (id > 0)
            {
                sqlwhere += " And DicId = " + id + " ";
            }
            sql = sql + sqlwhere;
            var result = _db.Query<string>(sql, new { TenantId = tenantId });
            if(result != null && result.Any())
            {
                var first = result.FirstOrDefault();
                string sqlupdate = " Update Dics Set IsReference = 1 Where DicId = @id ";
                _db.Execute(sqlupdate, new { id = id });
                return first;
            }
            return "";
        }

        public int getIdByValue(int tenantId, string value, string keyFirst,string keySecond)
        {
            string sql = " Select DicId From Dics Where IsEnable=1 And IsDelete = 1 And (TenantId = 0 Or TenantId = @TenantId ) ";
            string sqlwhere = "";
            if (!string.IsNullOrEmpty(keyFirst))
            {
                sqlwhere += " And KeyFirst= '" + keyFirst + "' ";
            }
            if (!string.IsNullOrEmpty(keySecond) && keySecond != "0")
            {
                sqlwhere += " And KeySecond= '" + keySecond + "' ";
            }
            if (!string.IsNullOrEmpty(value))
            {
                sqlwhere += " And DicValue = '" + value + "' ";
            }
            sql = sql + sqlwhere;
            var result = _db.Query<int>(sql, new { TenantId = tenantId });
            if (result != null && result.Any())
            {
                return result.FirstOrDefault();
            }
            return 0;
        }

        public int Add(int tenantId, string dicValue, string keyFirst)
        {
            return Add(tenantId, dicValue, keyFirst, "");
        }

        public int Add(int tenantId, string dicValue,string keyFirst,string keySecond)
        {
            //判断是否存在
            var exists = getIdByValue(tenantId, dicValue, keyFirst, keySecond);
            if(exists != 0)
            {
                return 0;
            }
            DicEntity mod = new DicEntity();
            mod.TenantId = tenantId;
            mod.KeyFirst = keyFirst;
            mod.KeySecond = keySecond;
            mod.DicValue = dicValue;
            mod.SearchKey = dicValue + dicValue.GetPinYinHead();
            mod.IsEnable = Enum.Enable.启用;
            mod.IsDelete = Enum.Delete.正常;
            mod.CreateTime = DateTime.Now;
            return Create(mod);
        }

        public int Delete(int dicid)
        {
            string sql = " Delete From Dics Where Id = @id And IsReference = 0  ";
            return _db.Execute(sql, new { id = dicid });
        }

        public int Update(int dicid,string dicvalue)
        {
            string sql = " Update  Dics Set DicValue= @dicvalue  Where DicId = @id  ";
            return _db.Execute(sql, new { id = dicid, dicvalue=dicvalue });
        }

    }
} 