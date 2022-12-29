using Dapper;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Extension;
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
    /// 数据访问层
    /// </summary>
    public class OutpatientRepository : BaseRepository<OutpatientEntity> 
	{ 
        //查询挂单处方
        public Tuple<List<OutpatientEntity>, int> getNoPaid(int tenantId,int pageSize,int pageIndex,string searchKey,DateTime? start,DateTime? end,int doctorId=0)
        {
            return getList(tenantId, pageSize, pageIndex, searchKey, start, end,doctorId,1);
        }

        //查询历史处方
        public Tuple<List<OutpatientEntity>, int> getHistoryPaid(int tenantId,int pageSize,int pageIndex,string searchKey,DateTime? start,DateTime? end,int doctorId=0)
        {
            return getList(tenantId, pageSize, pageIndex, searchKey, start, end,doctorId,2);
        }


        //调用退费处方
        public Tuple<List<OutpatientEntity>, int> getBackPaid(int tenantId, int pageSize, int pageIndex, string searchKey, DateTime? start, DateTime? end, int doctorId = 0)
        {
            //string sql = @" Select * From Outpatients Where TenantId = @TenantId And IsBack = 1 ";
            string where = "";
            if (string.IsNullOrEmpty(searchKey))
            {
                where += " And SearchKey like '%" + searchKey + "%' ";
            }
            if (start != null)
            {
                where += " And BackTime >= '" + start.Value + "' ";
            }
            if (end != null)
            {
                where += " And BackTime <= '" + end.Value + "' ";
            }
            if (doctorId != 0)
            {
                where += " And DoctorId = " + doctorId + " ";
            }
            string sqlpage = @$" 
            Select  count(1) as nums From Outpatients Where TenantId = @TenantId And IsDelete = 1 And IsBack = 1 {where}
            Select * From
            (
                Select  ROW_NUMBER() over(order by CreateTime desc) as row_id,* From Outpatients
                Where TenantId = @TenantId And IsDelete = 1 And IsBack = 1 {where}
            )t
            Where t.row_id between {(pageIndex - 1) * pageSize + 1} and { pageIndex * pageSize }
            ";
            var multi = _db.QueryMultiple(sqlpage, new { TenantId = tenantId });
            int count = multi.Read<int>().First();
            List<OutpatientEntity> datas = multi.Read<OutpatientEntity>()?.ToList();
            Tuple<List<OutpatientEntity>, int> result = new Tuple<List<OutpatientEntity>, int>(datas, count);
            return result;
        }

        /// <summary>
        /// 分页查找
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="searchKey"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="isNoPaid"></param>
        /// <returns></returns>
        private Tuple<List<OutpatientEntity>,int> getList(int tenantId, int pageSize, int pageIndex, string searchKey, DateTime? start, DateTime? end,int doctorId = 0,int isNoPaid = 0 )
        {            
            string where = "";
            if (!string.IsNullOrEmpty(searchKey))
            {
                where += " And SearchKey like '%"+ searchKey +"%' ";
            }
            if(start != null)
            {
                where += " And CreateTime >= '"+ start.Value +"' ";
            }
            if(end != null)
            {
                where += " And CreateTime <= '"+ end.Value +"' ";
            }
            if(isNoPaid == 1)
            {
                where += " And IsPay = -1 ";
            }else if(isNoPaid == 2)
            {
                where += " And IsPay = 1 And IsBack = -1 ";
            }
            if(doctorId != 0)
            {
                where += " And DoctorId = "+ doctorId +" ";
            }
            string sqlpage = @$" 
            Select  count(1) as nums From Outpatients Where TenantId = @TenantId And IsDelete = 1 {where}
            Select * From
            (
                Select  ROW_NUMBER() over(order by CreateTime desc) as row_id,* From Outpatients
                Where TenantId = @TenantId And IsDelete = 1 {where}
            )t
            Where t.row_id between {(pageIndex - 1) * pageSize + 1} and { pageIndex * pageSize }
            ";
            var multi = _db.QueryMultiple(sqlpage, new { TenantId = tenantId });
            int count = multi.Read<int>().First();
            List<OutpatientEntity> datas = multi.Read<OutpatientEntity>()?.ToList();
            Tuple<List<OutpatientEntity>, int> result = new Tuple<List<OutpatientEntity>, int>(datas, count);            
            return result;
        }

        /// <summary>
        /// 获取单个门诊信息
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="outpatientId"></param>
        /// <returns></returns>
        public  OutpatientStructure getStructure(int tenantId,int outpatientId)
        {
            OutpatientStructure result = new OutpatientStructure();
            string sql = @" Select * From Outpatients Where TenantId = @TenantId And OutpatientId=@OutpatientId And IsDelete = 1 ";
            var entitys = _db.Query<OutpatientEntity>(sql, new { TenantId = tenantId, OutpatientId = outpatientId });
            if(entitys != null && entitys.Any())
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
                if(pres_entitys == null || pres_entitys.Count < 1)
                {
                    throw new Exception("该门诊没有处方,数据错误");
                }
                #region 组装数据
                result.OutpatientId = entity.OutpatientId;
                result.TenantId = entity.TenantId;
                result.mdtrt_id = entity.mdtrt_id;
                result.setl_id = entity.setl_id;
                result.chrg_bchno = entity.chrg_bchno;
                result.RegisterId = entity.RegisterId;
                result.PrescriptionCount = entity.PrescriptionCount;
                result.IsPay = entity.IsPay;
                result.IsBack = entity.IsBack;
                result.PayYB = Math.Round(entity.PayYB / 1000.00,3);
                result.PayCash = Math.Round(entity.PayCash / 1000.00,3);
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
                if(case_entity != null)
                {
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
                }
                Dictionary<string, List<PrescriptionDetailStructure>> predic = new Dictionary<string, List<PrescriptionDetailStructure>>();                
                foreach(var item in pres_entitys)
                {
                    if(!predic.ContainsKey( item.PreName + "|" + item.Pair.ToString()))
                    {
                        predic.Add( item.PreName + "|" + item.Pair.ToString(), new List<PrescriptionDetailStructure>());
                    }
                    predic[ item.PreName + "|" + item.Pair.ToString()].Add(new PrescriptionDetailStructure
                    {
                        GoodsId = item.GoodsId,
                        GoodsName = item.GoodsName,
                        GoodsNum = item.GoodsNum,
                        GoodsNorm = item.GoodsNorm,
                        GoodsPrice = Math.Round(item.GoodsPrice / 1000.00,3),
                        GoodsCost = Math.Round(item.GoodsCost / 1000.00,3),
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
                    prestru.PreNo = 1;//int.Parse(item.Key.Split('|')[0]);
                    prestru.PreName = item.Key.Split('|')[0];
                    prestru.Pair = int.Parse(item.Key.Split('|')[1]);
                    prestru.Cost = item.Value.Sum(x => x.GoodsCost);
                    prestru.Details = item.Value;
                    result.Prescriptions.Add(prestru);
                }
                #endregion
            }
            return result;
        }
        public OutpatientAddStructure getStructure2(int tenantId, int outpatientId)
        {
            OutpatientAddStructure result = new OutpatientAddStructure();
            string sql = @" Select * From Outpatients Where TenantId = @TenantId And OutpatientId=@OutpatientId And IsDelete = 1 ";
            var entitys = _db.Query<OutpatientEntity>(sql, new { TenantId = tenantId, OutpatientId = outpatientId });
            if (entitys != null && entitys.Any())
            {
                var json = getOutpatientJson(outpatientId, tenantId);
                try
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<OutpatientAddStructure>(json);
                }
                catch (Exception ex)
                {
                    throw new Exception("json转换失败:" +ex.Message);
                }
            }
            return result;
        }



        /// <summary>
        /// 插入门诊相关信息
        /// </summary>
        /// <param name="structure"></param>
        /// <returns>返回门诊id</returns>
        public int AddStructure(OutpatientAddStructure structure)
        {
            var dbidstr = _dbid.ConnectionString;
            //插入或修改Patient
            PatientRepository patient_db = new PatientRepository();
            structure.Patient.TenantId = structure.TenantId;
            structure.CaseBook.TenantId = structure.TenantId;
            if (structure.Patient.PatientId == 0)
            {                
                var patientId = patient_db.Create(TypeConvert.DeepCopyByReflection(structure.Patient, new PatientEntity()));
                structure.Patient.PatientId = patientId;
            }
            else {
                patient_db.Update(TypeConvert.DeepCopyByReflection(structure.Patient, new PatientEntity()));
            }
            //插入或修改病历
            CaseBookRepository case_db = new CaseBookRepository();
            structure.CaseBook.OutPatientId = structure.OutpatientId;
            structure.CaseBook.PatientId = structure.Patient.PatientId;
            structure.CaseBook.DoctorId = structure.DoctorId;            
            if (structure.CaseBook.CaseBookId == 0)
            {                
                var caseboodId = case_db.Create(TypeConvert.DeepCopyByReflection(structure.CaseBook, new CaseBookEntity()));
                structure.CaseBook.CaseBookId = caseboodId;
            }
            else
            {
                case_db.Update(TypeConvert.DeepCopyByReflection(structure.CaseBook, new CaseBookEntity()));
            }


            //获取机构信息
            TenantRepository tenant_db = new TenantRepository(ReadConfig.GetConfigSection("Medical_Platform"));
            var tenant_entity = tenant_db.getById(structure.TenantId);
            //获取Doctor信息
            EmployeesRepository emp_db = new EmployeesRepository(ReadConfig.GetConfigSection("Medical_Platform"));
            var doc_entity = emp_db.Get(structure.DoctorId);
            //插入OutPatient
            OutpatientEntity entity = new OutpatientEntity()
            {
                OutpatientId = structure.OutpatientId,
                TenantId = structure.TenantId,
                PatientId = structure.Patient.PatientId,
                RegisterId = structure.RegisterId,
                CaseBookId = structure.CaseBook.CaseBookId,
                DoctorId = structure.DoctorId,
                DoctorName = doc_entity.EmployeeName,
                mdtrt_id = structure.mdtrt_id,                
                chrg_bchno = tenant_entity.YBCode + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "1",
                PrescriptionCount = structure.Prescriptions.Count,
                IsPay = -1,
                IsBack = -1,
                PayYB = 0,
                PayCash = 0,
                PayYBBefor = 0,
                PayYBAfter = 0,
                Cost = Convert.ToInt64(structure.Prescriptions.Sum(x => x.Details.Sum(y=> (y.GoodsPrice * y.GoodsNum) * 1000 ))),
                SearchKey = structure.Patient.PatientName + "|" + structure.Patient.PatientName.GetPinYin() + "|" + structure.Patient.Phone
            };
            var outpatientId = Create(entity);
            SaveOutpatientJson(outpatientId, structure.TenantId, Newtonsoft.Json.JsonConvert.SerializeObject(structure));
            //更新病历
            structure.CaseBook.OutPatientId = outpatientId;
            var departs = new DepartmentRepository(dbidstr).getTenantDepartment(structure.TenantId);
            var depid = 0;
            if(string.IsNullOrEmpty(doc_entity.Departments) || !int.TryParse(doc_entity.Departments,out depid))
            {
                structure.CaseBook.DepartmentId = 0;
            }
            else
            {
                structure.CaseBook.DepartmentId = departs.ToList().Find(x => x.DepartmentId == int.Parse(doc_entity.Departments))?.DepartmentId ?? 0;
            }            
            case_db.Update(TypeConvert.DeepCopyByReflection(structure.CaseBook, new CaseBookEntity()));

            //插入Prescriptions
            PrescriptionRepository pres_db = new PrescriptionRepository();
            DicRepository dic_db = new DicRepository();
            List<PrescriptionEntity> pres_entitys = new List<PrescriptionEntity>();
            int preno = 0;
            foreach(var item in structure.Prescriptions)
            {
                preno++;
                foreach (var node in item.Details)
                {
                    PrescriptionEntity pres_entity = new PrescriptionEntity();
                    pres_entity.PreNo = preno;//item.PreNo;
                    pres_entity.TenantId = structure.TenantId;
                    pres_entity.OutpatientId = outpatientId;                    
                    pres_entity.PreName = item.PreName;
                    pres_entity.GoodsId = node.GoodsId;
                    pres_entity.GoodsName = node.GoodsName;
                    pres_entity.GoodsNorm = node.GoodsNorm;
                    pres_entity.GoodsPrice = Convert.ToInt64(node.GoodsPrice * 1000);
                    pres_entity.GoodsNum = node.GoodsNum;
                    pres_entity.GoodsCost = pres_entity.GoodsNum * pres_entity.GoodsPrice;
                    string secondkey = "";
                    if(item.PreName.IndexOf("西药") > -1 || item.PreName.IndexOf("中成药") > -1)
                    {
                        secondkey = "西药";
                    }else if(item.PreName.IndexOf("中药") > -1)
                    {
                        secondkey = "中药";
                    }
                    else if (item.PreName.IndexOf("项目") > -1)
                    {
                        secondkey = "诊疗项目";
                    }
                    //枚举1: 西药,2:中成药,3:中药,4:诊疗项目,5:材料
                    pres_entity.GoodsUsage = dic_db.getValueById(structure.TenantId, node.GoodsUsage, "Usage", secondkey);
                    pres_entity.GoodsEveryDay = dic_db.getValueById(structure.TenantId, node.GoodsEveryDay, "EveryDay", "");
                    pres_entity.GoodsDays = node.GoodsDays;
                    pres_entity.GoodsSalesUnit = node.GoodsSalesUnit;
                    pres_entity.Place = node.Place;
                    pres_entity.Pair = item.Pair;
                    pres_entity.InsuranceCode = node.InsuranceCode;
                    pres_entity.CustomerCode = node.CustomerCode;
                    pres_entitys.Add(pres_entity);
                }
            }
            pres_db.InsertBulk(pres_entitys);
            //修改register为已使用            
            string regupdate = " Update Registers Set IsUsed = 1 Where TenantId = @TenantId And RegisterId=@RegisterId ";
            _db.Execute(regupdate, new { TenantId = structure.TenantId, RegisterId = structure.RegisterId });
            return outpatientId;
        }

        /// <summary>
        /// 修改门诊相关信息
        /// </summary>
        /// <param name="structure"></param>
        /// <returns></returns>
        public int UpdateStructure(OutpatientAddStructure structure)
        {
            //插入或修改Patient
            PatientRepository patient_db = new PatientRepository();

            if (structure.Patient.PatientId == 0)
            {
                var patientId = patient_db.Create(TypeConvert.DeepCopyByReflection(structure.Patient, new PatientEntity()));
                structure.Patient.PatientId = patientId;
            }
            else
            {
                var old_patient = patient_db.Get(structure.Patient.PatientId);
                if(old_patient != null && old_patient.TenantId == structure.TenantId)
                {
                    old_patient.Addr = structure.Patient.Addr;
                    old_patient.CSRQ = structure.Patient.CSRQ;
                    old_patient.PatientName = structure.Patient.PatientName;
                    old_patient.Phone = structure.Patient.Phone;
                    old_patient.psn_no = structure.Patient.psn_no;
                    old_patient.Sex = (Enum.Sex)structure.Patient.Sex;
                    old_patient.SFZ = structure.Patient.SFZ;
                    patient_db.Update(old_patient);
                }
            }
            //插入或修改病历
            CaseBookRepository case_db = new CaseBookRepository();
            if (structure.CaseBook.CaseBookId == 0)
            {
                var caseboodId = case_db.Create(TypeConvert.DeepCopyByReflection(structure.CaseBook, new CaseBookEntity()));
                structure.CaseBook.CaseBookId = caseboodId;
            }
            else
            {
                case_db.Update(TypeConvert.DeepCopyByReflection(structure.CaseBook, new CaseBookEntity()));
            }
            //获取Doctor信息
            EmployeesRepository emp_db = new EmployeesRepository(ReadConfig.GetConfigSection("Medical_Platform"));
            var doc_entity = emp_db.Get(structure.DoctorId);
            string sql = @" Update Outpatients 
                            Set PatientId=@PatientId,DoctorId=@DoctorId,DoctorName=@DoctorName,mdtrt_id=@mdtrt_id,PrescriptionCount=@PrescriptionCount
                                ,Cost=@Cost,SearchKey=@SearchKey
                            Where TenantId=@TenantId And OutpatientId = @OutpatientId ";
            _db.Execute(sql,new { TenantId=structure.TenantId, OutpatientId= structure.OutpatientId,PatientId= structure.Patient.PatientId
                ,DoctorId = structure.DoctorId,DoctorName= doc_entity.EmployeeName,
                mdtrt_id= structure.mdtrt_id,
                PrescriptionCount= structure.Prescriptions.Count,
                Cost= Convert.ToInt64(structure.Prescriptions.Sum(x => x.Details.Sum(y => (y.GoodsPrice * y.GoodsNum) * 1000))),
                SearchKey= structure.Patient.PatientName + structure.Patient.PatientName.GetPinYin() + "|" + structure.Patient.Phone
            });
            var outpatientId = structure.OutpatientId;

            //修改Prescriptions
            PrescriptionRepository pres_db = new PrescriptionRepository();
            DicRepository dic_db = new DicRepository();
            List<PrescriptionEntity> pres_entitys = new List<PrescriptionEntity>();
            int preno = 1;
            foreach (var item in structure.Prescriptions)
            {
                foreach (var node in item.Details)
                {
                    PrescriptionEntity pres_entity = new PrescriptionEntity();
                    pres_entity.TenantId = structure.TenantId;
                    pres_entity.OutpatientId = outpatientId;
                    pres_entity.PreNo = preno++;//item.PreNo;
                    pres_entity.PreName = item.PreName;
                    pres_entity.GoodsId = node.GoodsId;
                    pres_entity.GoodsName = node.GoodsName;
                    pres_entity.GoodsNorm = node.GoodsNorm;
                    pres_entity.GoodsPrice = Convert.ToInt64(node.GoodsPrice * 1000);
                    pres_entity.GoodsNum = node.GoodsNum;
                    pres_entity.GoodsCost = pres_entity.GoodsNum * pres_entity.GoodsPrice;
                    pres_entity.InsuranceCode = node.InsuranceCode;
                    pres_entity.CustomerCode = node.CustomerCode;
                    string secondkey = "";
                    if (item.PreName.IndexOf("西药") > -1 || item.PreName.IndexOf("中成药") > -1)
                    {
                        secondkey = "西药";
                    }
                    else if (item.PreName.IndexOf("中药") > -1)
                    {
                        secondkey = "中药";
                    }
                    else if (item.PreName.IndexOf("项目") > -1)
                    {
                        secondkey = "诊疗项目";
                    }
                    //枚举1: 西药,2:中成药,3:中药,4:诊疗项目,5:材料
                    pres_entity.GoodsUsage = dic_db.getValueById(structure.TenantId, node.GoodsUsage, "Usage", secondkey);
                    pres_entity.GoodsEveryDay = dic_db.getValueById(structure.TenantId, node.GoodsEveryDay, "EveryDay", "");
                    pres_entity.GoodsDays = node.GoodsDays;
                    pres_entity.GoodsSalesUnit = node.GoodsSalesUnit;
                    pres_entity.Place = node.Place;
                    pres_entity.Pair = item.Pair;
                    pres_entity.InsuranceCode = node.InsuranceCode;
                    pres_entity.CustomerCode = node.CustomerCode;
                    pres_entitys.Add(pres_entity);
                }
            }
            pres_db.UpdateBulk(pres_entitys);
            return outpatientId;
        }        

        /// <summary>
        /// 支付时修改状态
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="outpatientId"></param>
        public void UpdateIsPay(int tenantId,int outpatientId,string setl_id,long balance,long PayYB)
        {
            string sql = @" Update Outpatients Set IsPay = 1,setl_id=@Setl_id,PayYBAfter=@PayYBAfter,PayYB=@YBcost Where TenantId = @TenantId And OutpatientId = @OutpatientId ";
            _db.Execute(sql, new { TenantId = tenantId, OutpatientId = outpatientId, Setl_id= setl_id, PayYBAfter=balance, YBcost=PayYB });
        }

        /// <summary>
        /// 退费时修改状态
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="outpatientId"></param>
        public void UpdateIsBack(int tenantId, int outpatientId)
        {
            string sql = @" Update Outpatients Set IsBack = 1,BackTime=getdate() Where TenantId = @TenantId And OutpatientId = @OutpatientId ";
            _db.Execute(sql, new { TenantId = tenantId, OutpatientId = outpatientId });
        }

        /// <summary>
        /// 门诊费用明细撤销时修改chrg_bchno
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="outpatientId"></param>
        public void Updatechrgbchno(int tenantId, int outpatientId)
        {
            //获取机构信息
            TenantRepository tenant_db = new TenantRepository(ReadConfig.GetConfigSection("Medical_Platform"));
            var tenant_entity = tenant_db.getById(tenantId);
            string sql = @" Update Outpatients Set chrg_bchno = @chrg_bchno Where TenantId = @TenantId And OutpatientId = @OutpatientId ";
            _db.Execute(sql, new { TenantId = tenantId, OutpatientId = outpatientId, chrg_bchno= tenant_entity.YBCode + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "1" });
        }

        public void SaveOutpatientJson(int outpatientId,int tenantId,string json)
        {
            string sql = @" if exists( select OutpatientId From  OutpatientsJson Where OutpatientId = @id And TenantId=@TenantId  )
                            Begin
	                            Update OutpatientsJson
	                            Set Context = @json
	                            Where OutpatientId = @id
                            End else begin
	                            Insert Into OutpatientsJson(OutpatientId,TenantId,Context)
	                            Values(@id,@TenantId,@json)
                            end  ";
            _db.Execute(sql, new { TenantId = tenantId, id = outpatientId, json = json });
        }

        public string getOutpatientJson(int outpatientId, int tenantId)
        {
            string sql = @" Select Context From OutpatientsJson Where OutpatientId = @id And TenantId=@TenantId ";
            var result = _db.Query<string>(sql, new { TenantId = tenantId, id = outpatientId });
            if(result != null && result.Any())
            {
                return result.First();
            }
            else
            {
                return null;
            }
        }

    }
} 