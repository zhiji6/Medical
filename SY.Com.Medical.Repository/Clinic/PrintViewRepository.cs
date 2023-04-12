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

        public List<PrintViewEntity> getUserTemplates(int tenantId)
        {
            string sql = " Select * From PrintViews Where  TenantId = @tenant And IsDelete = 1 ";
            var result = _db.Query<PrintViewEntity>(sql, new { tenant = tenantId });
            if(result != null && result.Any())
            {
                return result.ToList();
            }
            return null;
        }

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
            string sql = @" Insert into PrintViews(PrintViewId, TenantId, PrintViewName, Style,PrintPathName,IsUse, IsDelete, IsEnable)
                            Values(@id,@tenangid,@name,@style,@printPathName,@isuse,1,1) ";
            int rows =_db.Execute(sql, new { id = id, tenangid = entity.TenantId, name = entity.PrintViewName, style = entity.Style, printPathName=entity.PrintPathName, isuse = entity.IsUse });
            return rows <= 0 ? 0 : id;
        }

        public PrintViewEntity getViewById(int tenantId,int printViewId)
        {
            string sql = " Select * From PrintViews Where PrintViewId=@PrintViewId And TenantId = @tenantid And IsDelete = 1 ";
            var result = _db.Query<PrintViewEntity>(sql, new { PrintViewId = printViewId, tenantid = tenantId }).FirstOrDefault();
            return result;
        }

        public PrintViewEntity getViewOnlyId(int printViewId)
        {
            string sql = " Select * From PrintViews Where PrintViewId=@PrintViewId  And IsDelete = 1 ";
            var result = _db.Query<PrintViewEntity>(sql, new { PrintViewId = printViewId }).FirstOrDefault();
            return result;
        }

        public bool SetCustomerUse(int tenantId,int printViewId)
        {
            string sql = " Select * From PrintViews Where PrintViewId=@PrintViewId And TenantId = @tenantid And IsDelete = 1 ";
            var result = _db.Query<PrintViewEntity>(sql, new { PrintViewId = printViewId, tenantid = tenantId }).FirstOrDefault();
            string sql2 = @" Update PrintViews Set IsUse = 1 Where PrintViewId=@PrintViewId And TenantId = @tenantid And IsDelete = 1;
                            Update PrintViews Set IsUse = 0 Where TenantId = @tenantid And IsDelete = 1 And Style=@style And PrintViewId<>@PrintViewId ";
            return _db.Execute(sql2, new { PrintViewId = printViewId, tenantid = tenantId, style = result.Style }) > 0;
        }

        public bool SetSystemUse(int tenantId,int styleId)
        {
            string sql = " Update PrintViews Set IsUse = 0 Where IsDelete = 1 And Style=@style And TenantId = @tenantid ";
            return _db.Execute(sql, new {  tenantid = tenantId, style = styleId }) > 0;
        }

    }
} 