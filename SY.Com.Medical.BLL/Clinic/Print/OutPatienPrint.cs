using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL.Clinic.Print
{

    /// <summary>
    /// 获取门诊信息进行打印
    /// 处方、收费等都已此为数据蓝本
    /// </summary>
    public class OutPatienPrint
    {
        private PrintPreViewRepository db;
        public OutPatienPrint()
        {
            db = new PrintPreViewRepository();
        }

        public CombinePrintDataModel Print(int tenantId, int outpatientId)
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
            foreach (var prescription in data.Prescriptions)
            {
                foreach (var good in prescription.Details)
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

        public CombinePrintDataModel View(int tenantId, int outpatientId)
        {
            var mod = Print(9, 17);
            if (string.IsNullOrEmpty(mod.DoctorName))
            {
                mod = Print(25, 6);
            }
            return mod;
        }


        public void EastPrescription(int tenantId, CombinePrintDataModel data, UserTenantResponse tenantmodel, List<CombinePrintDataModel> result)
        {
            Prescription(tenantId, data, tenantmodel, result, "中药处方",templateId : 3);
        }

        public void WestPrescription(int tenantId, CombinePrintDataModel data, UserTenantResponse tenantmodel, List<CombinePrintDataModel> result)
        {
            Prescription(tenantId, data, tenantmodel, result, "西药处方", templateId: 4);
        }

        public void ProjPrescription(int tenantId, CombinePrintDataModel data, UserTenantResponse tenantmodel, List<CombinePrintDataModel> result)
        {
            if (data == null || data.Goods == null) throw new MyException("未找到处方明细");
            int templateId = 5;
            if(data.Goods.Where(w => w.PreName == "项目处方").Any())
            {
                if(data.Goods.Where(w => w.DepartMent == "口腔").Any())
                {
                    templateId = 10;
                }
                if(data.Goods.Where(w => w.DepartMent == "中医").Any())
                {
                    templateId = 11;
                }
            }
            Prescription(tenantId, data, tenantmodel, result, "项目处方", templateId: templateId);
        }

        public void Prescription(int tenantId, CombinePrintDataModel data, UserTenantResponse tenantmodel, List<CombinePrintDataModel> result,string prename,int templateId)
        {
            if (data == null || data.Goods == null) throw new MyException("未找到处方明细");
            if (data.Goods.Where(w => w.PreName == prename).Any())
            {
                CombinePrintDataModel west = data.Clone();
                west.Goods = west.Goods.Where(w => w.PreName == prename).ToList();
                west.ViewPath = new PrintTemplate(tenantId).ChooseFile(templateId).FilePath;
                west.TenantName = tenantmodel.TenantName;
                west.TenantCode = tenantmodel.YBCode;
                result.Add(west);
            }
        }

    }
}
