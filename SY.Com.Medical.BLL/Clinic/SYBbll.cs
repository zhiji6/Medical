using SY.Com.Medical.BLL.Platform;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Infrastructure;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SY.Com.Medical.Model.SYBCommon;

namespace SY.Com.Medical.BLL.Clinic
{
    public class SYBbll
    {
        private SYBRepository _repository = new SYBRepository();
        /// <summary>
        /// 获取公共的入参结构
        /// </summary>
        /// <param name="fixmedins_code">机构编码</param>
        /// <param name="fixmedins_name">机构名称</param>
        /// <param name="opter"></param>
        /// <param name="opter_name"></param>
        /// <param name="sign_no"></param>
        /// <returns></returns>
        public InCommon getComm(string fixmedins_code, string fixmedins_name, string opter, string opter_name, string sign_no)
        {
            InCommon comm = new InCommon();
            comm.mdtrtarea_admvs = "440300"; //就医地医保区划 医保绑定，存储在库中。
            comm.insuplc_admdvs = "440300";  //参保地医保区划 字符型 6，读卡读到的
            comm.recer_sys_code = "PQ";// 接收方系统代码 字符型 10      Y  PQ
            comm.dev_no = "";// 设备编号 字符型 100
            comm.dev_safe_info = ""; //设备安全信息  字符型 2000
            comm.cainfo = "";// 数字签名信息  字符型 1024
            comm.signtype = "";// 签名类型    字符型 10  
            comm.infver = "V1.0";// 接口版本号   字符型 6       Y      暂时V1.0，配置文件
            comm.msgid = fixmedins_code + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "1";
            comm.opter_type = "1";//  经办人类别       3   Y Y
            comm.opter = opter;//经办人 字符型 30      Y
            comm.opter_name = opter_name;//  经办人姓名 字符型 50      Y
            comm.inf_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//    交易时间 日期时间型   19      Y
            comm.fixmedins_code = fixmedins_code;//  定点医药机构编号 字符型 12      Y
            comm.fixmedins_name = fixmedins_name;//  定点医药机构名称 字符型 20      Y
            comm.sign_no = sign_no;
            return comm;
        }

        public InCommon getComm(int tenantId,int opterId)
        {
            var tenantmod = getTenantById(tenantId);
            var optermod = getEmployeeById(opterId);
            InCommon comm = new InCommon();
            comm.mdtrtarea_admvs = "440300"; //就医地医保区划 医保绑定，存储在库中。
            comm.insuplc_admdvs = "440300";  //参保地医保区划 字符型 6，读卡读到的
            comm.recer_sys_code = "PQ";// 接收方系统代码 字符型 10      Y  PQzsy
            comm.dev_no = "";// 设备编号 字符型 100
            comm.dev_safe_info = ""; //设备安全信息  字符型 2000
            comm.cainfo = "";// 数字签名信息  字符型 1024
            comm.signtype = "";// 签名类型    字符型 10  
            comm.infver = "V1.0";// 接口版本号   字符型 6       Y      暂时V1.0，配置文件
            comm.msgid =tenantmod.YBCode + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "1";
            comm.opter_type = "1";//  经办人类别       3   Y Y
            comm.opter = optermod.YBCode;//经办人 字符型 30      Y
            comm.opter_name = optermod.EmployeeName;//  经办人姓名 字符型 50      Y
            comm.inf_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//    交易时间 日期时间型   19      Y
            comm.fixmedins_code = tenantmod.YBCode;//  定点医药机构编号 字符型 12      Y
            comm.fixmedins_name = tenantmod.YBName;//  定点医药机构名称 字符型 20      Y
            comm.sign_no = getSign(tenantId,opterId);
            comm.departname = new Department().getDetail(int.Parse(optermod.Departments))?.DepartmentName ?? "";
            return comm;
        }

        /// <summary>
        /// 获取挂号信息通过id
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public RegisterEntity getRegisterById(int registerId)
        {
            Register registerbll = new Register();
            var entity = registerbll.get(registerId);
            return entity;
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public UserTenantResponse getTenantById(int tenantId)
        {
            Tenant tenantbll = new Tenant(ReadConfig.GetConfigSection("Medical_Platform"));
            var mod = tenantbll.getById(tenantId);
            if(mod == null)
            {
                return null;
            }
            return mod;
        }

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public EmployeeModel getEmployeeById(int employeeId)
        {
            Employee employeebll = new Employee();
            var mod = employeebll.getEmployee(employeeId);
            if(mod == null)
            {
                return null;
            }
            int dpid = 1;
            if (int.TryParse(mod.Departments,out dpid))
            {
                mod.Departments = dpid.ToString();
            }
            else
            {
                mod.Departments = "1";
            }
            return mod;
        }

        public string getSign(int tenantId,int employeeId)
        {
            return _repository.GetSign(tenantId, employeeId);
        }

        /// <summary>
        /// 获取医保科室
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public YBDepartment getYBDepartment(string name)
        {
            YBDepartment result = new YBDepartment();
            if (SYBKS.departmentdic.ContainsKey(name))
            {
                result.name = name;
                result.code = SYBKS.departmentdic[name];                
            }else if(name.IndexOf("口腔") != -1)
            {
                result.name = "口腔科";
                result.code = "1200";
            }else
            {
                result.code = "5000";
                result.name = "中医科";
            }
            return result;
        }

        public int SignIn(int tenantId, int employeeId, string signno)
        {
            return _repository.SignIn(tenantId, employeeId, signno);
        }

        public int SignOut(int tenantId, int employeeId)
        {
            return _repository.SignOut(tenantId, employeeId);
        }

        /// <summary>
        /// 获取代码
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string getDiagnosisCode(string name)
        {            
            string code = "B90.251";
            return code;
        }


    }

    public class YBDepartment
    {
        public string code { get; set; }
        public string name { get; set; }
    }

}
