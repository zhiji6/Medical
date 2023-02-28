using SY.Com.Medical.BLL.Platform;
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
        public CaseBookResponseModel getCaseBookOne(int id)
        {
            CaseBookRepository cbrep = new CaseBookRepository();
            CaseBookResponseModel resp = new CaseBookResponseModel();
            var mod = cbrep.getOne(id).EntityToDto<CaseBookModel>();
            resp.CaseBookBH = mod.CaseBookBH;
            resp.CaseBookId = mod.CaseBookId;
            resp.CaseBookTypeId = mod.CaseBookTypeId;
            resp.CaseOrder = mod.CaseOrder;
            resp.Complaint = mod.Complaint;
            resp.CreateTime = mod.CreateTime;
            resp.DepartmentId = mod.DepartmentId;
            resp.DepartmentName = mod.DepartmentName;
            resp.Diagnosis = mod.Diagnosis;
            resp.Disease = mod.Disease;
            resp.DiseaseCode = mod.DiseaseCode;
            resp.DoctorId = mod.DoctorId;
            resp.DoctorName = mod.DoctorName;
            resp.HistoryCase = mod.HistoryCase;
            resp.Opinions = mod.Opinions;
            resp.OutPatientDate = mod.OutPatientDate;
            resp.OutPatientId = mod.OutPatientId;
            resp.PastCase = mod.PastCase;
            resp.Patient = mod.Patient;
            resp.PatientId = mod.PatientId;
            resp.Physical = mod.Physical;
            resp.Place = mod.Place;
            resp.Tooth = mod.Tooth;
            return resp;

        }

        public CombinePrintDataModel getCombineData(int tenantId,int outpatientId)
        {
            var data = db.getOutpatient(tenantId, outpatientId);
            if (data == null) return new CombinePrintDataModel();
            if (data.Patient == null) return new CombinePrintDataModel();
            if (data.Doctor == null) return new CombinePrintDataModel();
            if (data.Prescriptions == null || data.Prescriptions.Count < 1) return new CombinePrintDataModel();
            CombinePrintDataModel result = new CombinePrintDataModel();
            result.Addr = data.Patient.Addr;
            result.CSRQ = data.Patient.CSRQ;
            result.CashierName = data.ChargeRecord?.CashierName ?? "";
            result.chrg_bchno = data.chrg_bchno;
            result.Cost = data.Cost;
            result.DepartmentName = data.Doctor.Department;
            result.Diagnosis = data.CaseBook?.Diagnosis ?? "";
            result.DoctorCode = data.Doctor.YBCode;
            result.DoctorName = data.Doctor.EmployeeName;
            result.EastCost = data.Prescriptions?.Where(w => w.PreName == "中药处方")?.Sum(s => s.Cost) ?? 0.0;
            result.WestCost = data.Prescriptions?.Where(w => w.PreName == "西药处方")?.Sum(s => s.Cost) ?? 0.0;
            result.ProjCost = data.Prescriptions?.Where(w => w.PreName == "项目处方")?.Sum(s => s.Cost) ?? 0.0;
            result.Pair = data.Prescriptions?.Where(w => w.PreName == "中药处方")?.FirstOrDefault()?.Pair ?? 0;
            result.mdtrt_id = data.mdtrt_id;
            result.OutpatienTime = data.CreateTime;
            result.PatientName = data.Patient.PatientName;
            result.PayTime = data.ChargeRecord?.CreateTime ?? DateTime.Now;
            result.PayYB = data.PayYB;
            result.Phone = data.Patient.Phone;
            result.psn_no = data.Patient.psn_no;
            result.setl_id = data.setl_id;
            result.Sex = data.Patient.Sex;
            result.SFZ = data.Patient.SFZ;
            result.TenantCode = data.TenantCode;
            result.TenantName = data.TenantName;
            result.Balc = data.PayYBAfter;
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
