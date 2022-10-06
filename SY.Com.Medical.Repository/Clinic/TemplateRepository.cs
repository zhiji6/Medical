using Dapper;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Model;
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
    public class TemplateRepository : BaseRepository<TemplateEntity> 
	{
        public Tuple<List<TemplateEntity>, int> gets(TemplateRequest request)
        {
            string where = "";
            if (!string.IsNullOrEmpty(request.TemplateName))
            {
                where += " And TemplateName Like '%" + request.TemplateName + "%' ";
            }
            if(request.EmployeeId != 0)
            {
                where += " And EmployeeId = " + request.EmployeeId + " ";
            }
            if (!string.IsNullOrEmpty(request.TemplateType))
            {
                switch (request.TemplateType)
                {
                    case "1":where += " And TemplateType='私有' ";break;
                    case "2":where += " And TemplateType='公开' "; break;
                    default:break;
                }
            }
            string sqlpage = @$" 
            Select  count(1) as nums From Template Where TenantId = @TenantId And IsDelete = 1 {where}
            Select * From
            (
                Select  ROW_NUMBER() over(order by CreateTime desc) as row_id,* From Template
                Where TenantId = @TenantId And IsDelete = 1 {where}
            )t
            Where t.row_id between {(request.PageIndex - 1) * request.PageSize + 1} and { request.PageIndex * request.PageSize }
            ";

            var multi = _db.QueryMultiple(sqlpage, new { TenantId = request.TenantId });
            int count = multi.Read<int>().First();
            List<TemplateEntity> datas = multi.Read<TemplateEntity>()?.ToList();
            Tuple<List<TemplateEntity>, int> result = new Tuple<List<TemplateEntity>, int>(datas, count);
            return result;
        }
        

    }
} 