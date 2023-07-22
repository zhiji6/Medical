using Dapper;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SY.Com.Medical.Repository.Clinic
{
    /// <summary>
    /// 数据访问层
    /// </summary>
    public class DiseaseRepository : BaseRepository<DiseaseEntity> 
	{ 


        /// <summary>
        /// 返回name搜索不分页数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<DiseaseEntity> getsNoPage(List<string > names)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < names.Count; i++)
            {
                if (i == 0)
                    sb.Append(" [DiseaseName] Like '%" + names[i] + "%' ");
                else
                    sb.Append(" or [DiseaseName] Like '%" + names[i] + "%' ");
            }
            string sql = " Select * From Disease Where 1=1 And( " + sb.ToString() + ")"; 
            return _db.Query<DiseaseEntity>(sql);
        }

    }
} 