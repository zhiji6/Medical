using SY.Com.Medical.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Attribute
{
    /// <summary>
    /// db给默认值
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DB_DefaultAttribute : DBBaseAttribute
    {
        public object value;
        public DB_DefaultAttribute(Type t) 
        {
            if (t == typeof(Int32))
            {
                value = "0";
            }else if(t == typeof(string))
            {
                value = "";
            }
            else if(t == typeof(DateTime))
            {
                value = DateTime.Now;
            }else if(t == typeof(DateTime?))
            {
                value = DateTime.Now;
            }
            else if(t == typeof(Enable))
            {
                value = (int)Enable.启用;
            }else if(t == typeof(Delete))
            {
                value = (int)Delete.正常;
            }
            else if (t == typeof(IsBoss))
            {
                value = (int)IsBoss.不是;
            }else if(t == typeof(Sex))
            {
                value = (int)Sex.男;
            }else if(t == typeof(TenantType))
            {
                value = (int)TenantType.中医诊所;
            }else if(t == typeof(GoodType))
            {
                value = (int)GoodType.中成药;
            }else  if(t == typeof(DicType))
            {
                value = (int)DicType.物品_材料;
            }
        }

        public object getDefault() => value;
    }
}
