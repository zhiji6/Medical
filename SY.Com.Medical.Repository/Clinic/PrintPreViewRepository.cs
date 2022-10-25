using Dapper;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Infrastructure;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Repository.Clinic
{
    /// <summary>
    /// 获取打印数据和打印模板
    /// </summary>
    public class PrintPreViewRepository : PrintDataRepository
    {

        /// <summary>
        /// 获取打印文件
        /// </summary>
        /// <param name="style">打印类型</param>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        public string getViewPath(int style,int tenantId)
        {
            string sql = " Select top 1 TenantId From PrintViews Where Style="+ style +" And TenantId In("+ tenantId +",0) Order By TenantId Desc ";
            var tenantid = _db.Query<int>(sql).First();
            return "PrintView/"+ style +"/"+ tenantid + "/Print.grf";
        }

        /// <summary>
        /// 获取挂号和退号打印数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public RegisterEntity getRegisterData(int registerId)
        {
            string sql = " Select * From Registers Where RegisterId = @id  ";
            return _db.Query<RegisterEntity>(sql, new { id = registerId }).First();
        }

        /// <summary>
        /// 获取门诊处方/治疗单打印信息
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="outpatientId"></param>
        /// <returns></returns>
        public OutpatientStructure getOutpatient(int tenantId, int outpatientId)
        {


            OutpatientStructure result = new OutpatientStructure();
            string sql = @" Select * From Outpatients Where TenantId = @TenantId And OutpatientId=@OutpatientId And IsDelete = 1 ";
            var entitys = _db.Query<OutpatientEntity>(sql, new { TenantId = tenantId, OutpatientId = outpatientId });
            if (entitys != null && entitys.Any())
            {
                var entity = entitys.ToList().FirstOrDefault();
                PatientRepository patient_db = new PatientRepository();
                var patient_entity = patient_db.Get(entity.PatientId);
                EmployeesRepository emp_db = new EmployeesRepository(ReadConfig.GetConfigSection("Medical_Platform"));
                var doc_entity = emp_db.Get(entity.DoctorId);
                CaseBookRepository case_db = new CaseBookRepository();
                var case_entity = case_db.Get(entity.CaseBookId);
                PrescriptionRepository prescription_db = new PrescriptionRepository();
                var pres_entitys = prescription_db.getByOutpatientId(tenantId, outpatientId);
                if (pres_entitys == null || pres_entitys.Count < 1)
                {
                    throw new Exception("该门诊没有处方,数据错误");
                }
                #region 组装数据
                result.OutpatientId = entity.OutpatientId;
                result.TenantId = entity.TenantId;
                result.mdtrt_id = entity.mdtrt_id;
                result.chrg_bchno = entity.chrg_bchno;
                result.RegisterId = entity.RegisterId;
                result.PrescriptionCount = entity.PrescriptionCount;
                result.IsPay = entity.IsPay;
                result.IsBack = entity.IsBack;
                result.PayYB = Math.Round(entity.PayYB / 1000.00, 3);
                result.PayCash = Math.Round(entity.PayCash / 1000.00, 3);
                result.PayYBBefor = Math.Round(entity.PayYBBefor / 1000.00, 3);
                result.PayYBAfter = Math.Round(entity.PayYBAfter / 1000.00, 3);
                result.Cost = Math.Round(entity.Cost / 1000.00, 3);
                result.RealCost = result.Cost;
                result.DiscountCost = 0.00;
                result.Patient = new PatientStructure
                {
                    PatientId = patient_entity.PatientId,
                    PatientName = patient_entity.PatientName,
                    Phone = patient_entity.Phone,
                    CSRQ = patient_entity.CSRQ,
                    Sex = (int)patient_entity.Sex,
                    SFZ = patient_entity.SFZ,
                    Addr = patient_entity.Addr,
                    psn_no = patient_entity.psn_no
                };
                result.Doctor = new DoctorStructure
                {
                    EmployeeId = doc_entity.EmployeeId,
                    EmployeeName = doc_entity.EmployeeName,
                    Department = doc_entity.Departments,
                    YBCode = doc_entity.YBCode
                };
                result.CaseBook = new CaseBookStructure
                {
                    CaseBookId = case_entity.CaseBookId,
                    Complaint = case_entity.Complaint,
                    Diagnosis = case_entity.Diagnosis,
                    Disease = case_entity.Disease,
                    CaseOrder = case_entity.CaseOrder,
                    PastCase = case_entity.PastCase,
                    HistoryCase = case_entity.HistoryCase,
                    Physical = case_entity.Physical,
                    Opinions = case_entity.Opinions,
                    Tooth = case_entity.Tooth,
                    Place = case_entity.Place
                };
                Dictionary<string, List<PrescriptionDetailStructure>> predic = new Dictionary<string, List<PrescriptionDetailStructure>>();
                foreach (var item in pres_entitys)
                {
                    if (!predic.ContainsKey(item.PreNo + "|" + item.PreName + "|" + item.Pair.ToString()))
                    {
                        predic.Add(item.PreNo + "|" + item.PreName + "|" + item.Pair.ToString(), new List<PrescriptionDetailStructure>());
                    }
                    predic[item.PreNo + "|" + item.PreName + "|" + item.Pair.ToString()].Add(new PrescriptionDetailStructure
                    {
                        GoodsId = item.GoodsId,
                        GoodsName = item.GoodsName,
                        GoodsNum = item.GoodsNum,
                        GoodsNorm = item.GoodsNorm,
                        GoodsPrice = Math.Round(item.GoodsPrice / 1000.00, 3),
                        GoodsCost = Math.Round(item.GoodsCost / 1000.00, 3),
                        GoodsUsage = item.GoodsUsage,
                        GoodsEveryDay = item.GoodsEveryDay,
                        GoodsDays = item.GoodsDays,
                        GoodsSalesUnit = item.GoodsSalesUnit,
                        InsuranceCode = item.InsuranceCode,
                        CustomerCode = item.CustomerCode,
                        Place = item.Place
                    });
                }
                result.Prescriptions = new List<PrescriptionStructure>();
                foreach (var item in predic)
                {
                    PrescriptionStructure prestru = new PrescriptionStructure();
                    prestru.PreNo = int.Parse(item.Key.Split('|')[0]);
                    prestru.PreName = item.Key.Split('|')[1];
                    prestru.Pair = int.Parse(item.Key.Split('|')[2]);
                    prestru.Cost = item.Value.Sum(x => x.GoodsCost);
                    prestru.Details = item.Value;
                    result.Prescriptions.Add(prestru);
                }
                #endregion
            }
            return result;
            
        }

        /// <summary>
        /// 获取收费退费打印明细
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="outpatientId"></param>
        /// <param name="chargetype"></param>
        /// <returns></returns>
        public ChargeRecordEntity getByOutpatientId(int tenantId, int outpatientId, string chargetype)
        {
            string sql = @" Select * From ChargeRecords 
                            Where TenantId=@TenantId And SeeDoctorId=@SeeDoctorId And ChargeType= '" + chargetype + "' ";
            var mods = _db.Query<ChargeRecordEntity>(sql, new { TenantId = tenantId, SeeDoctorId = outpatientId });
            if (mods != null && mods.Any())
            {
                return mods.FirstOrDefault();
            }
            return null;
        }



    }
}
