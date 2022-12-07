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
    /// 患者业务逻辑
    /// </summary>
    public class Patient
    {
        private PatientRepository db;
        public Patient()
        {
            db = new PatientRepository();
        }

        
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Tuple<List<PatientModel>,int> gets(PatientPage request)
        {            
            var entitypage= db.GetsPage(request.DtoToEntity<PatientEntity>(),request.PageSize, request.PageIndex);
            Tuple<List<PatientModel>, int> result = new Tuple<List<PatientModel>, int>(entitypage.Item1.EntityToDto<PatientModel>(), entitypage.Item2);
            return result;
        }

        /// <summary>
        /// 单个患者查询
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public PatientModel get(int patientId)
        {
            return db.Get(patientId).EntityToDto<PatientModel>();
        }

        public PatientModel getContainDelete(int patientId)
        {
            return db.GetDelete(patientId).EntityToDto<PatientModel>();
        }

        

        /// <summary>
        /// 添加患者
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int add(PatientAdddto request)
        {
            //拼音            
            request.PatientPinYin = request.PatientName.GetPinYinHead();
            //搜索字段
            request.SearchKey = request.PatientName + request.PatientPinYin + request.Phone;            
            return db.Create(request.DtoToEntity<PatientEntity>());
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public void update(PatientUpdatedto request)
        {
            //拼音
            request.PatientPinYin = request.PatientName.GetPinYinHead();
            //搜索字段
            request.SearchKey = request.PatientName + request.PatientPinYin + request.Phone;
            db.Update(request.DtoToEntity<PatientEntity>());
        }

        public void Update(PatientEntity patient)
        {
            if (patient == null || patient.PatientId == 0) return;
            db.Update(patient);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="patientId"></param>
        public void delete(int patientId)
        {
            var entity = db.Get(patientId);
            db.Delete(entity);
        }


        /// <summary>
        /// 通过医保编号查询患者信息
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="psn_no"></param>
        /// <returns></returns>
        public PatientEntity getBypsnNo(int tenantId, string psn_no)
        {
            return db.getBypsnNo(tenantId, psn_no);
        }
    }
}
