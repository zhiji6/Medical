using SY.Com.Medical.Extension;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL.Clinic.Print
{
    public class CaseBookPrint
    {
        public CaseBookResponseModel Print(int tenantId, int caseBookId)
        {
            CaseBookRepository cbrep = new CaseBookRepository();
            CaseBookResponseModel resp = new CaseBookResponseModel();
            var mod = cbrep.getOne(caseBookId).EntityToDto<CaseBookModel>();
            resp.CaseOrder = mod.CaseOrder;
            resp.Complaint = mod.Complaint;
            resp.CreateTime = mod.CreateTime;
            resp.DepartmentName = mod.DepartmentName;
            resp.Diagnosis = mod.Diagnosis;
            resp.Disease = mod.Disease;
            resp.DoctorName = mod.DoctorName;
            resp.HistoryCase = mod.HistoryCase;
            resp.Opinions = mod.Opinions;
            resp.PastCase = mod.PastCase;
            resp.PatientName = mod.Patient.PatientName;
            resp.Sex = mod.Patient.Sex;
            resp.Phone = mod.Patient.Phone;
            resp.Physical = mod.Physical;
            resp.Place = mod.Place;
            resp.CSRQ = mod.Patient.CSRQ;
            resp.SFZ = mod.Patient.SFZ;
            resp.ViewPath = new PrintTemplate().ChooseFile(tenantId, 9).FilePath;
            resp.Temp = new List<PrintTemp>() { new PrintTemp { tempid = 1, tempname = "a" } };
            return resp;
        }

        public CaseBookResponseModel View(int tenantId, int registerId)
        {
            CaseBookResponseModel mod = new CaseBookResponseModel
            {
                CaseOrder = "多喝热水,多运动,多休息", SFZ="383402812934823943", Sex=1, Complaint="主诉如下,最近不舒服", CreateTime = DateTime.Now
                ,Disease="暂时未有疾病", CSRQ = DateTime.Now.AddYears(-20), DepartmentName="全科", Diagnosis="诊断如下,感冒,低烧,咳嗽", DoctorName="王医生"
                , HistoryCase="未有历史疾病", Opinions="先吃点药观察下,过几日来复诊", PastCase="没有现病史", PatientName="王五", Phone="12312312321"
                , Physical="未做体检", Place="无部位",
            };
            mod.Temp = new List<PrintTemp>() { new PrintTemp { tempid = 1, tempname = "a" } };
            mod.ViewPath = new PrintTemplate().ChooseFile(tenantId, 9).FilePath;
            return mod;
        }
    }
}
