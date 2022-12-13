using SY.Com.Medical.Entity;
using SY.Com.Medical.Infrastructure;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Platform;
using SY.Com.Medical.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL.Platform
{
    /// <summary>
    /// 员工
    /// </summary>
    public class Employee
    {
        private EmployeesRepository db;
        public Employee()
        {
            db = new EmployeesRepository();
        }

        /// <summary>
        /// 获取诊所的员工信息(启用)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Tuple<List<EmployeeModel>,int>  getEmployees(int tenantId,int pageSize=100,int pageIndex=1,string searchKey ="",string department="")
        {
            var tuple = db.getTenant(tenantId, pageSize, pageIndex, searchKey, department);
            Tuple<List<EmployeeModel>, int> result = new Tuple<List<EmployeeModel>, int>(tuple.Item1.EntityToDto<EmployeeModel>(), tuple.Item2);
            return result;
        }

        /// <summary>
        /// 获取诊所的员工信息(禁用)
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="searchKey"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public Tuple<List<EmployeeModel>, int> getEmployeesClose(int tenantId, int pageSize = 100, int pageIndex = 1, string searchKey = "", string department = "")
        {
            var tuple = db.getTenantClose(tenantId, pageSize, pageIndex, searchKey, department);
            Tuple<List<EmployeeModel>, int> result = new Tuple<List<EmployeeModel>, int>(tuple.Item1.EntityToDto<EmployeeModel>(), tuple.Item2);
            return result;
        }
        

        /// <summary>
        /// 获取诊所员工信息(禁用)
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public List<EmployeeModel> getEmployeesClose(int tenantId)
        {
            List<EmployeeModel> result = new List<EmployeeModel>();
            var entitys = db.getTenantClose(tenantId);
            return entitys.EntityToDto<EmployeeModel>();
        }

        /// <summary>
        /// 获取单个员工信息
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        public EmployeeModel getEmployee(int employeeid)
        {
            EmployeeModel result = new EmployeeModel();
            var entity = db.Get(employeeid, "Employees", "EmployeeId");
            if(entity != null && entity.EmployeeId > 0)
            {
                CloneClass.Clone<EmployeeEntity, EmployeeModel>(entity, result);
            }            
            return result;
        }

        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="employee"></param>
        public void updateEmployee(EmployeeModel employee,int OpenClose)
        {
            var entity = employee.DtoToEntity<EmployeeEntity>();
            if (OpenClose == 1)
            {
                entity.IsEnable = Enum.Enable.启用;
            }
            else {
                entity.IsEnable = Enum.Enable.禁用;                
            }
            db.Update(entity);
        }

        /// <summary>
        /// //邀请用户成为员工
        /// 如果是用户了直接邀请生成员工
        /// 如果还不是用户,自动注册,密码和验证码默认123456
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="TenantId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public int invite(string Account,int TenantId,string roles,string employeeName)
        {
            User us = new User();
            bool exists = us.ExistsAccount(Account);
            Department dt = new Department();
            var departs = dt.getDepartment(new DepartmentModel { TenantId = TenantId });
            if (exists)
            {
                //
                var user = us.getByAccount(Account);                
                var em = db.getByUser(new UserEntity { UserId = user.UserId }, TenantId);
                if (em != null && em.EmployeeId != 0)
                {
                    em.IsEnable = Enum.Enable.启用;
                    em.IsDelete = Enum.Delete.正常;
                    db.Update(em);
                    return em.EmployeeId;
                }
                else {
                    EmployeeModel mod = new EmployeeModel()
                    {
                        UserId = user.UserId,
                        Roles = roles,
                        TenantId = TenantId,
                        Sex = (int)Enum.Sex.男,
                        EmployeeName = employeeName,
                        Departments = departs.FirstOrDefault().DepartmentId.ToString()
                    };
                    return createEmployee(mod);
                }                
            }
            else
            {
                RegisterRequest mod = new RegisterRequest() { Account = Account, Pwd = "123456", TenantId = TenantId, YZM = "123456" };
                var res = us.Register(mod);
                Tenant tbll = new Tenant();
                tbll.CreateUserTenant(res.UserId, TenantId, Enum.IsBoss.不是);
                EmployeeModel mod2 = new EmployeeModel()
                {
                    UserId = res.UserId,
                    Roles = roles,
                    TenantId = TenantId,
                    Sex = (int)Enum.Sex.男,
                    EmployeeName = employeeName,
                    Departments = departs.FirstOrDefault().DepartmentId.ToString()
                };
                return createEmployee(mod2);
            }
        }

        /// <summary>
        /// 邀请员工最后一步创建员工信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int createEmployee(EmployeeModel request)
        {
            var entity = request.DtoToEntity<EmployeeEntity>();
            return db.Create(entity);
        }

        /// <summary>
        /// 根据用户信息获取员工信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public EmployeeModel getEmployee(EmployeeGetModel request)
        {
            return db.getByUser(new UserEntity { UserId = request.UserId },request.TenantId).EntityToDto<EmployeeModel>();
        }


        public EmployeeModel getEmployeeByUser(int tenantid ,int userid)
        {
            return db.getByUser(new UserEntity { UserId = userid }, tenantid).EntityToDto<EmployeeModel>();
        }
        

    }
}
