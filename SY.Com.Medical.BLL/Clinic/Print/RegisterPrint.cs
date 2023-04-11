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
    public class RegisterPrint
    {
        private PrintPreViewRepository db;
        public RegisterPrint()
        {
            db = new PrintPreViewRepository();
        }
        public PrintRegisterResponseModel Print(int tenantId,int registerId)
        {
            PrintRegisterResponseModel mod = db.getRegisterData(registerId).EntityToDto<PrintRegisterResponseModel>();
            mod.Temp = new List<PrintTemp>() { new PrintTemp { tempid = 1, tempname = "a" } };
            return mod;
        }

        public PrintRegisterResponseModel View(int tenantId, int registerId)
        {
            PrintRegisterResponseModel mod = new PrintRegisterResponseModel
            {
                TenantName = "测试机构",
                Addr = "广东省深圳市南山区xxxxxxx",
                CreateTime = DateTime.Now,
                CSRQ = DateTime.Now.AddYears(-18),
                DepartmentName = "全科",
                DoctorName = "张医生",
                GoodsName = "",
                GoodsPrice = 10,
                ipt_otp_no = "",
                IsUsed = 1,
                mdtrt_id = "",
                Name = "张三",
                PatientId = 0,
                Phone = "12345612312",
                psn_no="4324234",
                RegisterId=0,
                SearchKey="",
                Sex=1,
                SFZH="394102839283290313",
                TenantId=tenantId,
            };
            mod.Temp = new List<PrintTemp>() { new PrintTemp { tempid = 1, tempname = "a" } };
            return mod;
        }
    }


}


