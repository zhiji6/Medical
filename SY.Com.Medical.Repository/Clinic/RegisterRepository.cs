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
    public class RegisterRepository : BaseRepository<RegisterEntity> 
	{ 
        /// <summary>
        /// 挂号分页查询
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="searchkey"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Tuple<IEnumerable<RegisterEntity>, int> SearchPage(int tenantId,int pageSize,int pageIndex,string searchkey,DateTime? start,DateTime? end,string doctorname,int IsUsed = 0)
        {
            //string sql = @" Select * From Registers Where TenantId = @TenantId ";
            string where = "";
            if(!string.IsNullOrEmpty(searchkey))
            {
                where += " And SearchKey like '%"+ searchkey +"%' ";
            }
            if(start!=null)
            {
                where += " And CreateTime >= '"+ start.Value +"' ";
            }
            if (end != null)
            {
                where += " And CreateTime <= '" + end.Value + "' ";
            }
            if(IsUsed == -1)
            {
                where += " And IsUsed=-1 ";
            }else if(IsUsed == 1)
            {
                where += " And IsUsed=1 ";
            }

            if (doctorname != null)
            {
                where += " And DoctorName = '" + doctorname + "' ";

            }

            string sqlpage = @$" 
            Select  count(1) as nums From Registers Where TenantId = @TenantId And IsDelete = 1 {where}
            Select * From
            (
                Select  ROW_NUMBER() over(order by CreateTime desc) as row_id,* From Registers
                Where TenantId = @TenantId And IsDelete = 1 {where} 
            )t
            Where t.row_id between {(pageIndex - 1) * pageSize + 1} and { pageIndex * pageSize }
            ";
            var multi = _db.QueryMultiple(sqlpage, new { TenantId = tenantId });
            int count = multi.Read<int>().First();
            IEnumerable<RegisterEntity> datas = multi.Read<RegisterEntity>();
            Tuple<IEnumerable<RegisterEntity>, int> result = new Tuple<IEnumerable<RegisterEntity>, int>(datas, count);
            return result;
        }

        /// <summary>
        /// 退号
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public int Back(int tenantId,int registerId)
        {
            string sql = " Update Registers Set IsEnable = 2 Where TenantId = @TenantId And RegisterId = @RegisterId ";
            return _db.Execute(sql, new { TenantId = tenantId, RegisterId = registerId });

        }

        /// <summary>
        /// 获取医生id
        /// </summary>
        /// <param name="tenantid"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeEntity> getDoctorIds(int tenantid,List<string> names)
        {
            string sql = @" select * From Employees Where TenantId = @tenandid And EmployeeName in@names And IsDelete = 1 And IsEnable = 1 ";
            return _dbid.Query<EmployeeEntity>(sql, new { tenandid = tenantid, names = names });
        }

        ///<summary>
        ///获取医生姓名
        ///</summary>
        ///<param name="tenantid"></param>
        ///<param name="doctorid"></param>
        ///<returns></returns>
        public IEnumerable<EmployeeEntity> BackDoctorName(int tenantId, int doctorid)
        {
            string sql = " select * From Employees Where TenantId = @tenandid And EmployeeId in@doctorid  And IsEnable = 1";
            return _dbid.Query<EmployeeEntity>(sql, new { TenantId = tenantId, EmployeeId = doctorid });

        }

    }
} 