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
        public IEnumerable<DiseaseEntity> getsNoPage(string name)
        {
            string sql = " Select * From Disease Where [DiseaseName] Like '%" + name + "%' ";
            return _db.Query<DiseaseEntity>(sql);
        }

    }
} 