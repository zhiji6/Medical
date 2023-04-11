using SY.Com.Medical.Repository.Clinic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL.Clinic.Print
{
    /// <summary>
    /// 打印插件文件
    /// 具体某个打印插件文件的抽象,当操作打印插件文件时，都是调用此类下的方法
    /// 目前只使用了锐浪打印报表，将来大概率也不会变化，所以就用具体类，如果需要扩展不同的打印插件
    /// 则此类应该变为接口的功能，然后通过具体类来实现具体业务
    /// </summary>
    public class PrintFile
    {
        private PrintViewRepository db = new PrintViewRepository();
        /// <summary>
        /// 文件路径(相对)
        /// </summary>
        public string FilePath { get { return $"/Print/PrintView/{TemplatePath}/{FileId}.grf"; } }
        /// <summary>
        /// 文件唯一Id
        /// </summary>
        public int FileId { get; set; }
        /// <summary>
        /// 类型Id
        /// </summary>
        public int TemplateId { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TemplateName { get; set; }   
        /// <summary>
        /// 类型缩写
        /// </summary>
        public string TemplatePath { get; set; }
        /// <summary>
        /// 是否系统文件
        /// </summary>
        public bool IsSystem { get; set; }
        /// <summary>
        /// 是否用于打印
        /// </summary>
        public bool IsUse { get; set; }
        private int TenantId { get; set; }
        public PrintFile(int tenantId,int templateId,int fileId,string templateName,string templatePath,bool isSystem,bool isUse)
        {
            TenantId = tenantId;
            TemplateId = templateId;
            FileId = fileId;
            TemplateName = templateName;
            TemplatePath = templatePath;
            IsSystem = isSystem;
            IsUse = IsUse;
        }
        public PrintFile(int tenantId,int fileId)
        {
            TenantId = tenantId;
            var view = db.getViewById(tenantId, fileId);
            FileId = view.PrintViewId;
            TemplateId = view.Style;
            TemplateName = view.PrintViewName;
            IsSystem = view.TenantId == 0;
            IsUse = view.IsUse;
            TemplatePath = view.PrintPathName;
        }

        public PrintFile(int fileId)
        {
            var view = db.getViewOnlyId(fileId);
            FileId = view.PrintViewId;
            TemplateId = view.Style;
            TemplateName = view.PrintViewName;
            IsSystem = view.TenantId == 0;
            IsUse = view.IsUse;
            TemplatePath = view.PrintPathName;
            TenantId = view.TenantId;
        }

        /// <summary>
        /// 克隆一个PrintFile
        /// </summary>
        /// <param name="tenantId">克隆后的tenantId</param>
        /// <returns></returns>
        public PrintFile Clone(int tenantId)
        {
            int id = SaveNewdb();
            var newprintfile = new PrintFile(tenantId, id);
            try
            {
                newprintfile.Save(FilePath);
            }catch
            {
                DeleteDB();
            }
            return newprintfile;
        }



        private void Save(string oldfilePath)
        {
            if (!File.Exists(SystemEnvironment.GetRootDirector() + oldfilePath))
                throw new MyException("打印模板文件丢失");
            var bytes = File.ReadAllBytes(SystemEnvironment.GetRootDirector() + oldfilePath);
            if (File.Exists(SystemEnvironment.GetRootDirector() + FilePath))
            {
                File.Delete(SystemEnvironment.GetRootDirector() + FilePath);
                //更新
                SaveNewFile(bytes);
            }
            else
            {
                //新增
                SaveNewFile(bytes);
            }            

        }

        private void SaveNewFile(byte[] bytes)
        {
            //保存文件
            using (var fs = File.Create(SystemEnvironment.GetRootDirector() + FilePath))
            {
                fs.Write(bytes, 0, bytes.Count());
            }
        }

        private int SaveNewdb()
        {            
            return db.Add(new Entity.PrintViewEntity { TenantId = TenantId, PrintViewName = TemplateName, PrintPathName = TemplatePath, Style = TemplateId, IsUse = false });
        }



        public PrintFile Update(byte[] bytes)
        {
            if (TenantId == 0) throw new MyException("系统模板不能修改");
            File.Delete(SystemEnvironment.GetRootDirector() + FilePath);
            //更新
            SaveNewFile(bytes);
            return this;
        }

        public void Delete()
        {
            if (TenantId == 0) throw new MyException("系统模板不能删除");
            DeleteFile();
            DeleteDB();
        }

        private void DeleteFile()
        {
            File.Delete(SystemEnvironment.GetRootDirector() + FilePath);
        }

        private void DeleteDB()
        {
            db.Delete(new Entity.PrintViewEntity { PrintViewId = FileId });
        }


        public bool SetUse(int tenantId)
        {
            if (IsUse) return true;
            if(IsSystem)
            {
                return db.SetSystemUse(tenantId, TemplateId);
            }
            else
            {
                return db.SetCustomerUse(tenantId, FileId);
            }
        }


    }
}
