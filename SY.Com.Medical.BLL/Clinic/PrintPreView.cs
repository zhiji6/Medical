using SY.Com.Medical.Entity;
using SY.Com.Medical.Extension;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL.Clinic
{
    /// <summary>
    /// 获取打印数据和打印模板
    /// </summary>
    public class PrintPreView
    {
        private PrintPreViewRepository db;
        public PrintPreView()
        {
            db = new PrintPreViewRepository();
        }

        /// <summary>
        /// 获取打印文件
        /// </summary>
        /// <param name="style">打印类型</param>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        public string getViewPath(int style, int tenantId)
        { 
            return db.getViewPath(style, tenantId);
        }

        /// <summary>
        /// 获取挂号和退号打印数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public PrintRegisterResponseModel getRegisterData(int registerId)
        {
            PrintRegisterResponseModel mod = new PrintRegisterResponseModel();
            return db.getRegisterData(registerId).EntityToDto<PrintRegisterResponseModel>();
        }

        /// <summary>
        /// 获取门诊处方/治疗单打印信息
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="outpatientId"></param>
        /// <returns></returns>
        public OutpatientStructure getOutpatient(int tenantId, int outpatientId)
        {
            return db.getOutpatient(tenantId, outpatientId);
        }


        /// <summary>
        /// 获取病历记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CaseBookModel getCaseBookOne(int id)
        {
            CaseBookRepository cbrep = new CaseBookRepository();
            return cbrep.getOne(id).EntityToDto<CaseBookModel>();
        }


    }
}
