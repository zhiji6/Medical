using SY.Com.Medical.Entity;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Extension
{
    public static class TypeConvert
    {
        /// <summary>
        /// Dto转Entity
        /// </summary>
        /// <typeparam name="D">一个BaseModel</typeparam>
        /// <typeparam name="E">一个BaseEntity</typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static E DtoToEntity<E>(this BaseModel dto)  where E : BaseEntity
        {
            Type modtype = dto.GetType();
            Type entitytype = typeof(E);
            E entity = (E)Activator.CreateInstance(typeof(E));
            return (E)DeepCopyByReflection(dto, entity);
            //foreach (var prop in modtype.GetProperties())
            //{
            //    var pro = entitytype.GetProperty(prop.Name);
            //    if (pro != null)
            //    {
            //        pro.SetValue(entity, prop.GetValue(dto));
            //    }
            //}
            //return entity;
        }

        /// <summary>
        /// Dto转Entity
        /// List-->List
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<E> DtoToEntity<E>(this List<BaseModel> dto) where E : BaseEntity
        {
            List<E> result = new List<E>();
            foreach (var item in dto)
            {
                result.Add(item.DtoToEntity<E>());
            }
            return result;
        }

        /// <summary>
        /// Dto转Entity
        /// IEnumerable-->List
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<E> DtoToEntity<E>(this IEnumerable<BaseModel> dto) where E : BaseEntity
        {
            List<E> result = new List<E>();
            foreach (var item in dto)
            {
                result.Add(item.DtoToEntity<E>());
            }
            return result;
        }

        /// <summary>
        /// Entity转Dto
        /// </summary>
        /// <typeparam name="E">一个BaseEntity</typeparam>
        /// <typeparam name="D">一个BaseModel</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static D EntityToDto<D>(this BaseEntity entity) where D : BaseModel
        {
            if(entity == null)
            {
                throw new Exception("未找到数据");
            }
            Type modtype = typeof(D);
            Type entitytype = entity.GetType();
            D dto = (D)Activator.CreateInstance(typeof(D));
            return (D)DeepCopyByReflection(entity, dto);
            //foreach (var prop in entitytype.GetProperties())
            //{                
            //    var pro = modtype.GetProperty(prop.Name);
            //    if (pro != null)
            //    {
            //        pro.SetValue(dto, prop.GetValue(entity));                  
            //    }
            //}
            //return dto;
        }


        /// <summary>
        /// 深度拷贝并转换数据类型,暂时不兼容Struct
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static Object DeepCopyByReflection(Object source,Object target)
        {
            //if (source is string || source.GetType().IsValueType)
            //{
            //    target = source;
            //    return target;
            //}                
            foreach (var sourceProp in source.GetType().GetProperties())
            {
                var targetProp = target.GetType().GetProperty(sourceProp.Name);
                if(targetProp == null)
                {
                    continue;
                }
                //引用类型继续递归,值类型复制值
                if ( targetProp.PropertyType.IsClass && targetProp.PropertyType.FullName != "System.String")
                {
                    targetProp.SetValue(target, DeepCopyByReflection(sourceProp.GetValue(source), Activator.CreateInstance(targetProp.PropertyType)));
                }
                else if (sourceProp.PropertyType.FullName.IndexOf("Double") >= 0 && targetProp.PropertyType.FullName.IndexOf("Int") >= 0)
                {
                    //价格转换
                    targetProp.SetValue(target, Convert.ToInt64(Convert.ToDouble(sourceProp.GetValue(source).ToString()) * 1000));
                }else if(targetProp.PropertyType.FullName.IndexOf("Double") >= 0 && sourceProp.PropertyType.FullName.IndexOf("Int") >= 0)
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

        public static T DeepCopyByReflection<S,T>(S source, T target)
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
                else if(sourceProp.PropertyType.FullName.IndexOf("Double") >= 0 && targetProp.PropertyType.FullName.IndexOf("Int") >= 0)
                {
                    //价格转换
                    targetProp.SetValue(target,Convert.ToInt64( Convert.ToDouble(sourceProp.GetValue(source).ToString()) * 1000));
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

        /// <summary>
        /// Entity转Dto
        /// List-->List
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<D> EntityToDto<D>(this List<BaseEntity> entity) where D : BaseModel
        {
            if (entity == null)
            {
                throw new Exception("未找到数据");
            }
            List<D> result = new List<D>();
            foreach(var item in entity)
            {
                result.Add(item.EntityToDto<D>());
            }
            return result;
        }

        /// <summary>
        /// Entity转Dto
        /// IEnumerable-->List
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<D> EntityToDto<D>(this IEnumerable<BaseEntity> entity) where D : BaseModel
        {
            if (entity == null)
            {
                throw new Exception("未找到数据");
            }
            List<D> result = new List<D>();
            foreach (var item in entity)
            {
                result.Add(item.EntityToDto<D>());
            }
            return result;
        }

        /// <summary>
        /// BaseModel映射到BaseModel
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static D Mapping<D>(this BaseModel mod) where D : BaseModel
        {
            D dto = (D)Activator.CreateInstance(typeof(D));
            return (D)DeepCopyByReflection(mod, dto);
        }

        /// <summary>
        /// BaseModel对另外的BaseModel进行List映射
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static List<D> Mapping<D>(this List<BaseModel> mod) where D : BaseModel
        {
            List<D> result = new List<D>();
            foreach (var item in mod)
            {
                result.Add(item.Mapping<D>());
            }
            return result;
        }

        /// <summary>
        /// BaseModel对另外的BaseModel进行IEnumerable映射
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static List<D> Mapping<D>(this IEnumerable<BaseModel> mod) where D : BaseModel
        {
            List<D> result = new List<D>();
            foreach (var item in mod)
            {
                result.Add(item.Mapping<D>());
            }
            return result;
        }

    }
}
