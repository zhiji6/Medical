using SY.Com.Medical.Entity;
using SY.Com.Medical.Repository.Clinic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL.Clinic.Print
{
    /// <summary>
    /// 打印插件类型管理
    /// 用户所拥有的打印插件文件，目前的管理逻辑比较简单，直接实现即可，如果未来逻辑比较复杂则需要重构此类
    /// </summary>
    public class PrintTemplate
    {
        private PrintViewRepository db = new PrintViewRepository();

        /// <summary>
        /// 打印类型Id
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>
        /// 打印类型名称
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 打印类型下的打印文件
        /// </summary>
        public List<PrintFile> PrintFiles { get; set; }
        /// <summary>
        /// 获取用户(Tenant)的所有打印文件,按照分类规整好
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public List<PrintTemplate> getTemplates(int tenantId)
        {
            List<PrintViewEntity> dbViews = new List<PrintViewEntity>();
            if (tenantId == 0 )
            {
                dbViews = db.getUserTemplates(tenantId);
            }
            else
            {
                dbViews = db.getUserTemplates(0);
                var printviews = db.getUserTemplates(tenantId);
                if(printviews != null)
                {
                    dbViews.AddRange(printviews);
                }
            }
            return ConstructionTemplate(dbViews);
        }

        /// <summary>
        /// 某个用户创建一个自定义打印文件
        /// </summary>
        /// <param name="tenantId">用户Id</param>
        /// <param name="templateId">打印文件类型Id</param>
        /// <returns></returns>
        public PrintFile AddPrintFile(int tenantId,int templateId)
        {
            if (tenantId == 0) throw new MyException("系统模板不能新增");
            var tenantTempaltes = db.getViews2(templateId, tenantId);
            if (tenantTempaltes == null) tenantTempaltes = new List<Entity.PrintViewEntity>();
            if (tenantTempaltes.Count > 1) throw new MyException("最多只能添加2张打印模板");
            var systemTemplate = db.getSystemView(new Entity.PrintViewEntity() { Style = templateId, TenantId = 0 });
            var sysPrintFile = new PrintFile(systemTemplate.TenantId, templateId, systemTemplate.PrintViewId, systemTemplate.PrintViewName, systemTemplate.PrintPathName, false, false);
            var newPrintFile = sysPrintFile.Clone(tenantId);
            return newPrintFile;
        }

        /// <summary>
        /// 选择用来做打印的打印文件
        /// </summary>
        public PrintFile ChooseFile(int tenantId, int templateId)
        {
            List<PrintTemplate> templates = new PrintTemplate().getTemplates(tenantId);
            var template = templates.Find(f => f.TemplateId == templateId);
            if (template == null) throw new MyException("找不到所需要的打印类型");
            var file = template.PrintFiles.Find(f => f.IsUse);
            if (file == null) throw new MyException("没有设置打印类型下所使用的具体打印文件,请先进行设置");
            return file;
        }



        private List<PrintTemplate> ConstructionTemplate(List<PrintViewEntity> printViews)
        {
            List<PrintTemplate> result = new List<PrintTemplate>();
            Hashtable hashtable = new Hashtable();
            foreach(var view in printViews)
            {
                if (!hashtable.ContainsKey(view.Style))
                {
                    hashtable.Add(view.Style, new PrintTemplate() { TemplateName = view.PrintViewName, TemplateId = view.Style
                        , PrintFiles = new List<PrintFile>() { new PrintFile(view.TenantId,view.Style,view.PrintViewId,view.PrintViewName,view.PrintPathName,view.TenantId == 0,view.IsUse) } });
                }
                else
                {
                    var pt = (PrintTemplate)hashtable[view.Style];
                    pt.PrintFiles.Add(new PrintFile(view.TenantId, view.Style, view.PrintViewId, view.PrintViewName, view.PrintPathName, view.TenantId == 0, view.IsUse));
                }
            }
            var keys = hashtable.Keys;
            foreach(string key in keys)
            {
                var pt = (PrintTemplate)hashtable[key];
                if(!pt.PrintFiles.Exists(w=>w.IsUse == true))
                {
                    pt.PrintFiles.Find(f => f.IsSystem).IsUse = true;
                }
                result.Add(pt);
            }
            return result;
        }





    }
}
