using SY.Com.Medical.Entity;
using SY.Com.Medical.Extension;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL.Clinic
{
    public class Report
    {
        private ReportRepository db;
        public Report()
        {
            db = new ReportRepository();
        }

        /// <summary>
        /// 查询时间段内收入和门诊量
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ReportModel TotalPrice(int tenantId, string startTime, string endTime)
        {
            var entity = db.TotalPrice(tenantId, startTime, endTime);
            if (entity == null) return new ReportModel();
            return EntityToDto<ReportModel, ReportEntity>(entity);
        }


        /// <summary>
        /// 查询时间段内的医生的统计情况
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<ReportModel> TotalDoctors(int tenantId, string startTime, string endTime)
        {
            var entity = db.TotalDoctors(tenantId, startTime, endTime);
            if (entity == null || entity.Count <= 0) return new List<ReportModel>();
            return EntityToDtoList<ReportModel, ReportEntity>(entity);
        }

        /// <summary>
        /// 项目统计
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="PreName"></param>
        /// <returns></returns>
        public List<ReportProjectModel> TotalProject(int tenantId, string startTime, string endTime, string PreName)
        {
            var entity = db.TotalProject(tenantId, startTime, endTime, PreName);
            if (entity == null) return new List<ReportProjectModel>();
            return EntityToDtoList<ReportProjectModel, ReportProjectEntity>(entity);
        }

        /// <summary>
        /// 获取机构时间段内所有的收费记录
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<ChargeRecordModel> getByTenantId(int tenantId, string startTime, string endTime, string chargeType)
        {
            var entity = db.getByTenantId(tenantId, startTime, endTime, chargeType);
            if (entity == null || entity.Count <= 0) return new List<ChargeRecordModel>();
            return  entity.EntityToDto<ChargeRecordModel>();
        }

        #region 负责数据装换,很恶心的东西
        public static D EntityToDto<D,E>(E entity) 
        {
            if (entity == null)
            {
                throw new Exception("未找到数据");
            }
            Type modtype = typeof(D);
            Type entitytype = entity.GetType();
            D dto = (D)Activator.CreateInstance(typeof(D));
            return (D)DeepCopyByReflection(entity, dto);
        }

        public static List<D> EntityToDtoList<D,E>(List<E> entity)
        {
            if (entity == null)
            {
                throw new Exception("未找到数据");
            }
            List<D> result = new List<D>();
            foreach (var item in entity)
            {
                result.Add(EntityToDto<D,E>(item));
            }
            return result;
        }

        private static Object DeepCopyByReflection(Object source, Object target)
        {
            //if (source is string || source.GetType().IsValueType)
            //{
            //    target = source;
            //    return target;
            //}                
            foreach (var sourceProp in source.GetType().GetProperties())
            {
                var targetProp = target.GetType().GetProperty(sourceProp.Name);
                if (targetProp == null)
                {
                    continue;
                }
                //引用类型继续递归,值类型复制值
                if (targetProp.PropertyType.IsClass && targetProp.PropertyType.FullName != "System.String")
                {
                    targetProp.SetValue(target, DeepCopyByReflection(sourceProp.GetValue(source), Activator.CreateInstance(targetProp.PropertyType)));
                }
                else if (sourceProp.PropertyType.FullName.IndexOf("Double") >= 0 && targetProp.PropertyType.FullName.IndexOf("Int") >= 0)
                {
                    //价格转换
                    targetProp.SetValue(target, Convert.ToInt64(Convert.ToDouble(sourceProp.GetValue(source).ToString()) * 1000));
                }
                else if (targetProp.PropertyType.FullName.IndexOf("Double") >= 0 && sourceProp.PropertyType.FullName.IndexOf("Int") >= 0)
                {
                    targetProp.SetValue(target, Convert.ToDouble(Convert.ToInt64(sourceProp.GetValue(source).ToString()) / 1000.00));
                }
                else
                {
                    targetProp.SetValue(target, sourceProp.GetValue(source));
                }
            }

            return target;
        }

        #endregion
    }
}
