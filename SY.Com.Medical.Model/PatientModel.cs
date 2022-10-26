using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model
{
    /// <summary>
    /// 患者dto模型
    /// </summary>
    public class PatientModel : BaseModel
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        public int PatientId { get; set; }


        /// <summary>
        /// 患者姓名    
        /// </summary>    
        public string PatientName { get; set; }

        /// <summary>
        /// 姓名拼音    
        /// </summary> 
        public string PatientPinYin { get; set; }

        /// <summary>
        /// 电话（手机号）    
        /// </summary>       
        public string Phone { get; set; }

        /// <summary>
        /// 性别    
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 出生日期    
        /// </summary>
        public DateTime CSRQ { get; set; }

        /// <summary>
        /// 身份证号    
        /// </summary>     
        public string SFZ { get; set; }

        /// <summary>
        /// 医保电脑号    
        /// </summary>  
        public string YBDNH { get; set; }

        /// <summary>
        /// 医保卡号    
        /// </summary>      
        public string YBKH { get; set; }

        /// <summary>
        /// 地址    
        /// </summary>      
        public string Addr { get; set; }
        /// <summary>
        /// 搜索
        /// </summary>
        public string SearchKey { get; set; }
        /// <summary>
        /// 年龄
        /// 使用出生日期计算
        /// 大于等于24个月，只显示岁，岁用总月数整除12向下取整
        /// 小于24个月,显示岁和月，岁依然是月数整除12向下取整，月为月数对12求余        
        /// 小于6月的显示天
        /// </summary>
        public string Age
        {
            get {
                var totalMonth = (DateTime.Now.Year - CSRQ.Year) * 12 - CSRQ.Month + DateTime.Now.Month;
                if (totalMonth < 2)
                {
                    return $"{(DateTime.Now - CSRQ).Days}天";
                }else if(totalMonth >= 24)
                {
                    return $"{totalMonth / 12}岁";
                }
                else {
                    if (totalMonth >= 12)
                    {
                        return $"{totalMonth / 12}岁{totalMonth % 12}月";
                    }
                    else {
                        return $"{totalMonth}月";
                    }                    
                }
            }
        }


    }

    /// <summary>
    /// 患者分页查询模型
    /// </summary>
    public class PatientPage : PageModel
    {
        /// <summary>
        /// 搜索   
        /// </summary>    
        public string SearchKey { get; set; }

        /// <summary>
        /// 电话（手机号）    
        /// </summary>       
        public string Phone { get; set; }
    }

    /// <summary>
    /// 患者分页查询模型
    /// </summary>
    public class PatientSearchPage : PageModel
    {
        /// <summary>
        /// 搜索   
        /// </summary>    
        public string SearchKey { get; set; }

    }

    /// <summary>
    /// 添加
    /// </summary>
    public class PatientAdddto : BaseModel
    {
        /// <summary>
        /// 患者姓名    
        /// </summary>    
        public string PatientName { get; set; }

        /// <summary>
        /// 姓名拼音    
        /// </summary> 
        [JsonIgnore]
        public string PatientPinYin { get; set; }

        /// <summary>
        /// 电话（手机号）    
        /// </summary>       
        public string Phone { get; set; }

        /// <summary>
        /// 性别    
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 出生日期    
        /// </summary>
        public DateTime? CSRQ { get; set; }

        /// <summary>
        /// 身份证号    
        /// </summary>     
        public string SFZ { get; set; }

        /// <summary>
        /// 医保电脑号    
        /// </summary>          
        public string YBDNH { get; set; }

        /// <summary>
        /// 医保卡号    
        /// </summary>              
        public string YBKH { get; set; }

        /// <summary>
        /// 地址    
        /// </summary>              
        public string Addr { get; set; }
        /// <summary>
        /// 人员医保编号
        /// </summary>
        [JsonIgnore]
        public string psn_no { get; set; }
        /// <summary>
        /// 搜索
        /// </summary>
        [JsonIgnore]
        public string SearchKey { get; set; }

    }

    /// <summary>
    /// 修改
    /// </summary>
    public class PatientUpdatedto : BaseModel
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// 患者姓名    
        /// </summary>    
        public string PatientName { get; set; }

        /// <summary>
        /// 姓名拼音    
        /// </summary> 
        [JsonIgnore]
        public string PatientPinYin { get; set; }

        /// <summary>
        /// 电话（手机号）    
        /// </summary>       
        public string Phone { get; set; }

        /// <summary>
        /// 性别    
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 出生日期    
        /// </summary>
        public DateTime? CSRQ { get; set; }

        /// <summary>
        /// 身份证号    
        /// </summary>     
        public string SFZ { get; set; }

        /// <summary>
        /// 医保电脑号    
        /// </summary>          
        public string YBDNH { get; set; }

        /// <summary>
        /// 医保卡号    
        /// </summary>              
        public string YBKH { get; set; }
        /// <summary>
        /// 人员医保编号
        /// </summary>
        [JsonIgnore]
        public string psn_no { get; set; }

        /// <summary>
        /// 地址    
        /// </summary>              
        public string Addr { get; set; }
        /// <summary>
        /// 搜索
        /// </summary>
        [JsonIgnore]
        public string SearchKey { get; set; }

    }

    /// <summary>
    /// 删除患者dto
    /// </summary>
    public class PatientDel:BaseModel
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        public int PatientId { get; set; }
    }

    /// <summary>
    /// 患者dto模型
    /// </summary>
    public class PatientYBModel : BaseModel
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        public int PatientId { get; set; }


        /// <summary>
        /// 患者姓名    
        /// </summary>    
        public string PatientName { get; set; }

        /// <summary>
        /// 姓名拼音    
        /// </summary> 
        public string PatientPinYin { get; set; }

        /// <summary>
        /// 电话（手机号）    
        /// </summary>       
        public string Phone { get; set; }

        /// <summary>
        /// 性别    
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 出生日期    
        /// </summary>
        public DateTime CSRQ { get; set; }

        /// <summary>
        /// 身份证号    
        /// </summary>     
        public string SFZ { get; set; }

        /// <summary>
        /// 医保电脑号    
        /// </summary>  
        public string YBDNH { get; set; }

        /// <summary>
        /// 医保卡号    
        /// </summary>      
        public string YBKH { get; set; }

        /// <summary>
        /// 地址    
        /// </summary>      
        public string Addr { get; set; }
        /// <summary>
        /// 搜索
        /// </summary>
        public string SearchKey { get; set; }
        /// <summary>
        /// 年龄
        /// 使用出生日期计算
        /// 大于等于24个月，只显示岁，岁用总月数整除12向下取整
        /// 小于24个月,显示岁和月，岁依然是月数整除12向下取整，月为月数对12求余        
        /// 小于6月的显示天
        /// </summary>
        public string Age
        {
            get
            {
                var totalMonth = (DateTime.Now.Year - CSRQ.Year) * 12 - CSRQ.Month + DateTime.Now.Month;
                if (totalMonth < 2)
                {
                    return $"{(DateTime.Now - CSRQ).Days}天";
                }
                else if (totalMonth >= 24)
                {
                    return $"{totalMonth / 12}岁";
                }
                else
                {
                    if (totalMonth >= 12)
                    {
                        return $"{totalMonth / 12}岁{totalMonth % 12}月";
                    }
                    else
                    {
                        return $"{totalMonth}月";
                    }
                }
            }
        }
        /// <summary>
        /// 医保号
        /// </summary>
        public string psn_no { get; set; }        
        /// <summary>
        /// 医保信息
        /// </summary>
        public List<YBinsuinfo> ybinfo { get; set; }


    }

    /// <summary>
    /// 医保人员的部分信息
    /// </summary>
    public class YBinsuinfo
    {
        /// <summary>
        /// 余额
        /// </summary>
        public string balc { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string insutype { get; set; }
    }

}
