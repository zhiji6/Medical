using Dapper;
using SY.Com.Medical.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Repository.Clinic
{
    public class PatientRepository : BaseRepository<PatientEntity>
    {
        /// <summary>
        /// 根据医保编码获取患者信息
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="psn_no"></param>
        /// <returns></returns>
        public PatientEntity getBypsnNo(int tenantId,string psn_no)
        {
            string sql = " Select * From Patients Where TenantId = @TenantId And psn_no=@Psn_no And IsDelete = 1 ";
            var mods = _db.Query<PatientEntity>(sql, new { TenantId = tenantId, Psn_no = psn_no });
            if(mods == null || !mods.Any())
            {
                return null;
            }
            return mods.FirstOrDefault();
        }

        public override int Create(PatientEntity t)
        {
            t.insuplc_admdvs = string.IsNullOrEmpty(t.insuplc_admdvs) ? "" : t.insuplc_admdvs;
            return base.Create(t);
        }


    }
}
