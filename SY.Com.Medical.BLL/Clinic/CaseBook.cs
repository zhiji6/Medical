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
    /// 病历业务逻辑
    /// </summary>
    public class CaseBook 
    {
        private CaseBookRepository db;
        public CaseBook()
        {
            db = new CaseBookRepository();
        }

        /// <summary>
        /// 获取某个患者的所有病历-分页
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Tuple<IEnumerable<CaseBookModel>,int> gets(int patientId,int pageSize,int pageIndex)
        {
            var datas = db.getPages(new CaseBookEntity { PatientId = patientId }, pageSize, pageIndex);
            if(datas.Item1 != null && datas.Item1.Any())
            {
                Department depart = new Department();
                var departs = depart.getDepartment(new DepartmentModel() { TenantId = datas.Item1.First().TenantId });
                foreach (var data in datas.Item1)
                {
                    var empmod = new Employee().getEmployee(data.DoctorId);
                    data.DoctorName = empmod.EmployeeName;
                    data.DepartmentName = departs.Find(x => x.DepartmentId == data.DepartmentId) == null ? "" : departs.Find(x => x.DepartmentId == data.DepartmentId).DepartmentName; ;
                }
            }
            Tuple<IEnumerable<CaseBookModel>, int> result = new (datas.Item1.EntityToDto<CaseBookModel>(), datas.Item2);
            return result;
        }

        /// <summary>
        /// 获取病历详情
        /// </summary>
        /// <param name="casebookId"></param>
        /// <returns></returns>
        public CaseBookModel get(int casebookId)
        {
            var result = db.getOne(casebookId).EntityToDto<CaseBookModel>();
            Department depart = new Department();
            var departs = depart.getDepartment(new DepartmentModel() { TenantId = result.TenantId });
            int departid = 0;
            int.TryParse(result.DepartmentName, out departid);
            result.DepartmentId = departs.Find(f => f.DepartmentId == departid)?.DepartmentId ?? 0;
            result.DepartmentName = departs.Find(f => f.DepartmentId == departid)?.DepartmentName ?? "全科";//departs.Find(x => x.DepartmentId == mod.DepartmentId) == null ? "" : departs.Find(x => x.DepartmentId == mod.DepartmentId).DepartmentName; ;                    
            return result;
        }

        /// <summary>
        /// 手动新增病历-无门诊
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int add(CaseBookAdd request)
        {
            CaseBookEntity entity = request.DtoToEntity<CaseBookEntity>();
            //处理数据
            entity.CaseBookBH = db.getBH(1);
            Employee employee = new Employee();
            var empmod = employee.getEmployee(entity.DoctorId);
            entity.DoctorName = empmod.EmployeeName;
            entity.DepartmentName = empmod.Departments;
            return db.Create(entity);
        }

        /// <summary>
        /// 手动修改病历-无门诊
        /// </summary>
        /// <param name="request"></param>
        public void update(CaseBookUpdate request)
        {
            CaseBookEntity entity = request.DtoToEntity<CaseBookEntity>();
            if(entity.DoctorId != 0)
            {
                Employee employee = new Employee();
                var empmod = employee.getEmployee(entity.DoctorId);
                entity.DoctorName = empmod.EmployeeName;
                entity.DepartmentName = empmod.Departments;
            }
            var oldentity = get(entity.CaseBookId);
            entity.PatientId = oldentity.PatientId;
            db.Update(entity);
        }
        
        /// <summary>
        /// 删除模型
        /// </summary>
        /// <param name="request"></param>
        public void delete(CaseBookDelete request)
        {
            CaseBookEntity entity = request.DtoToEntity<CaseBookEntity>();
            db.Delete(entity);
        }

        /// <summary>
        /// 获取医生下拉框
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public List<CaseBookDoctorDepart> getDoctors(int tenantId)
        {
            List<CaseBookDoctorDepart> result = new List<CaseBookDoctorDepart>();
            Employee employeebll = new Employee();
            var tuple = employeebll.getEmployees(tenantId);
            Department depart = new Department();

            var departs = depart.getDepartment(new DepartmentModel() { TenantId = tenantId });
            var mods = tuple.Item1;
            if (mods != null && mods.Any())
            {
                foreach (var node in mods)
                {
                    CaseBookDoctorDepart mod = new CaseBookDoctorDepart();
                    mod.DoctorId = node.EmployeeId;
                    mod.DoctorName = node.EmployeeName;
                    int departid = 0;
                    int.TryParse(node.Departments, out departid);
                    mod.DepartmentId = departs.Find(f=> f.DepartmentId == departid)?.DepartmentId ?? 0;
                    mod.Departments = departs.Find(f => f.DepartmentId == departid)?.DepartmentName ?? "全科";//departs.Find(x => x.DepartmentId == mod.DepartmentId) == null ? "" : departs.Find(x => x.DepartmentId == mod.DepartmentId).DepartmentName; ;                    
                    result.Add(mod);
                }
            }
            return result;
        }

    }
}
