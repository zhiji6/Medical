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
    public class PrintViewRepository : BaseRepository<PrintViewEntity> 
	{ 
        /// <summary>
        /// 获取系统的打印视图
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public PrintViewEntity getSystemView(PrintViewEntity entity)
        {
            string sql = " Select * From PrintViews Where Style=@style And TenantId = 0 And IsDelete = 1 ";
            return _db.Query<PrintViewEntity>(sql, new { style = entity.Style }).FirstOrDefault();
        }

        /// <summary>
        /// 获取租户的打印视图
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public PrintViewEntity getTenantView(PrintViewEntity entity)
        {
            string sql = " Select * From PrintViews Where Style=@style And TenantId = @tenantid And IsDelete = 1 ";
            var result = _db.Query<PrintViewEntity>(sql, new { style = entity.Style,tenantid = entity.TenantId }).FirstOrDefault();
            if(result == null)
            {
                var result2 = getSystemView(entity);
                if(result2 !=null)
                {
                    return result2;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取某一类型的打印视图列表,没有则获取0的
        /// </summary>
        /// <param name="style"></param>
        /// <param name="tenantid"></param>
        /// <returns></returns>
        public List<PrintViewEntity> getViews(int style,int tenantid)
        {
            string sql = " Select * From PrintViews Where Style = @style And TenantId = @tenant And IsDelete = 1 ";
            var result = _db.Query<PrintViewEntity>(sql, new { style = style, tenant = tenantid });
            if(result == null || !result.Any())
            {
                string sql2 = " Select * From PrintViews Where Style = @style And TenantId = 0 And IsDelete = 1 ";
                var result2 = _db.Query<PrintViewEntity>(sql2, new { style = style });
                if(result2 != null && result2.Any())
                {
                    return result2.ToList();
                }
                return null;
            }
            return result.ToList();
        }

        /// <summary>
        /// 获取某一类型的打印视图列表
        /// </summary>
        /// <param name="style"></param>
        /// <param name="tenantid"></param>
        /// <returns></returns>
        public List<PrintViewEntity> getViews2(int style, int tenantid)
        {
            string sql = " Select * From PrintViews Where Style = @style And TenantId = @tenant And IsDelete = 1 ";
            var result = _db.Query<PrintViewEntity>(sql, new { style = style, tenant = tenantid });
            if (result == null) return null;
            return result.ToList();
        }

        public int Add(PrintViewEntity entity)
        {
            int id = getID("PrintViews");
            string sql = @" Insert into PrintViews(PrintViewId, TenantId, PrintViewName, Style, IsDelete, IsEnable)
                            Values(@id,@tenangid,@name,@style,1,1) ";
            return _db.Execute(sql, new { id = id, tenangid = entity.TenantId, name = entity.PrintViewName,style = entity.Style });
        }

	}
} 