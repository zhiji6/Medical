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

        public CombinePrintDataModel getCombineData(int tenantId,int outpatientId)
        {
            var data = db.getOutpatient(tenantId, outpatientId);
            if (data == null) return new CombinePrintDataModel();
            if (data.Patient == null) return new CombinePrintDataModel();
            if (data.Doctor == null) return new CombinePrintDataModel();
            if (data.Prescriptions == null || data.Prescriptions.Count < 1) return new CombinePrintDataModel();
            CombinePrintDataModel result = new CombinePrintDataModel
            {
                Addr = data.Patient.Addr,
                CSRQ = data.Patient.CSRQ,
                CashierName = data.ChargeRecord?.CashierName ?? "",
                chrg_bchno = data.chrg_bchno,
                Cost = data.Cost,
                DepartmentName = data.Doctor.Department,
                Diagnosis = data.CaseBook?.Diagnosis ?? "",
                DoctorCode = data.Doctor.YBCode,
                DoctorName = data.Doctor.EmployeeName,
                EastCost = data.Prescriptions?.Where(w => w.PreName == "中药处方")?.Sum(s => s.Cost) ?? 0.0,
                WestCost = data.Prescriptions?.Where(w => w.PreName == "西药处方")?.Sum(s => s.Cost) ?? 0.0,
                ProjCost = data.Prescriptions?.Where(w => w.PreName == "项目处方")?.Sum(s => s.Cost) ?? 0.0,
                Pair = data.Prescriptions?.Where(w => w.PreName == "中药处方")?.FirstOrDefault().Pair ?? 0,
                mdtrt_id = data.mdtrt_id,
                OutpatienTime = data.CreateTime,
                PatientName = data.Patient.PatientName,
                PayTime = data.ChargeRecord?.CreateTime ?? DateTime.Now,
                PayYB = data.PayYB,
                Phone = data.Patient.Phone,
                psn_no = data.Patient.psn_no,
                setl_id = data.setl_id,
                Sex = data.Patient.Sex,
                SFZ = data.Patient.SFZ,
                TenantCode = data.TenantCode,
                TenantName = data.TenantName,
                Balc = data.PayYBAfter
            };
            result.Goods = new List<CombinePrintGoodModel>();
            foreach(var prescription in data.Prescriptions)
            {                
                foreach(var good in prescription.Details)
                {
                    CombinePrintGoodModel g = new CombinePrintGoodModel
                    {
                        PreName = prescription.PreName,
                        CustomerCode = good.CustomerCode,
                        GoodsName = good.GoodsName,
                        InsuranceCode = good.InsuranceCode,
                        Place = good.Place,
                        Single = good.SingleNum,
                        Total = good.GoodsNum,
                        Unit = good.GoodsSalesUnit,
                        Usage = good.GoodsUsage,
                        Price = good.GoodsPrice
                    };
                    result.Goods.Add(g);
                }
            }
            return result;

        }


    }
}
