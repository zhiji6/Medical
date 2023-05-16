using Newtonsoft.Json.Converters;
using SY.Com.Medical.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model
{
    /// <summary>
    /// 租户
    /// </summary>
    public class TenantModel : BaseModel
    {
    }


    /// <summary>
    /// 获取用户租户列表入参
    /// </summary>
    public class UserTenantRequest
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
    }

    /// <summary>
    /// 获取租户列表出参
    /// </summary>
    public class UserTenantResponse : BaseModel
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public new int TenantId { get; set; }

        /// <summary>
        /// 租户名称    
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 租户类型,枚举    
        /// </summary>        
        //public TenantType TenantType { get; set; }
        public int TenantType { get; set; }

        /// <summary>
        /// 服务开始时间    
        /// </summary>        
        public DateTime? TenantServiceStart { get; set; }

        /// <summary>
        /// 服务结束时间    
        /// </summary>            
        public DateTime? TenantServiceEnd { get; set; }

        /// <summary>
        /// 医保编码    
        /// </summary>
        public string YBCode { get; set; }

        /// <summary>
        /// 医保名称    
        /// </summary>
        public string YBName { get; set; }

        /// <summary>
        /// 医保程序路径    
        /// </summary>
        public string YBUrl { get; set; }

        /// <summary>
        /// 是否老板
        /// </summary>
        
        public int IsBoss {get;set;}
        /// <summary>
        /// 租户图片
        /// </summary>
        public string LogoImg { get; set; }
        /// <summary>
        /// 是否过期,true:过期,false:未过期
        /// </summary>
        public bool IsExpire { get; set; }


    }

    /// <summary>
    /// 创建租户入参
    /// </summary>
    public class TenentCreateRequest : BaseModel
    {
        /// <summary>
        /// 租户名称    
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 租户类型,枚举    
        /// </summary>
        public int TenantType { get; set; }

        /// <summary>
        /// 图片路径(相对),通过图片上传接口返回的路径
        /// </summary>
        public string LogoImg { get; set; }


    }

    /// <summary>
    /// 后端创建诊所模型
    /// </summary>
    public class TenentCreateAdminRequest
    {
        /// <summary>
        /// 创建账户
        /// </summary>
        [Required]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// 租户名称    
        /// </summary>
        [Required]
        public string TenantName { get; set; }

        /// <summary>
        /// 租户类型,枚举    
        /// </summary>
        [Required]
        public int TenantType { get; set; }

        /// <summary>
        /// 图片路径(相对),通过图片上传接口返回的路径
        /// </summary>
        public string LogoImg { get; set; }


    }



    /// <summary>
    /// 租户模型入参
    /// </summary>
    public class TenantRequest : BaseModel
    {

        /// <summary>
        /// 租户名称    
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 租户类型,枚举    
        /// </summary>
        public int TenantType { get; set; }

        /// <summary>
        /// 服务开始时间    
        /// </summary>
        public DateTime? TenantServiceStart { get; set; }

        /// <summary>
        /// 服务结束时间    
        /// </summary>
        public DateTime? TenantServiceEnd { get; set; }

        /// <summary>
        /// 医保编码    
        /// </summary>
        public string YBCode { get; set; }

        /// <summary>
        /// 医保名称    
        /// </summary>
        public string YBName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LogoImg { get; set; }

        /// <summary>
        /// 医保程序路径    
        /// </summary>
        public string YBUrl { get; set; }

        /// <summary>
        /// 是否可用:枚举    
        /// </summary>        
        public Enable IsEnable { get; set; }

        /// <summary>
        /// 启用禁用
        /// </summary>        
        public Delete IsDelete { get; set; }
        /// <summary>
        /// 记录创建时间
        /// </summary>        
        public DateTime? CreateTime { get; set; }
        
    }
    
    /// <summary>
    /// 租户模型出参
    /// </summary>
    public class TenantResponse : BaseModel
    {

    }

    /// <summary>
    /// 邀请员工模型入参
    /// </summary>
    public class TenantInviteRequest : BaseModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 角色字符串
        /// </summary>
        public string Roles { get; set; }
    }

    /// <summary>
    /// 邀请员工出参
    /// </summary>
    public class TenantInivteResponse : BaseModel
    {

    }

    /// <summary>
    /// 全网搜索租户信息
    /// </summary>
    public class TenantAllSearchRequest
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }
        /// <summary>
        /// 诊所编号,多个编号使用空格隔开
        /// </summary>
        public string TenantIds { get; set; }
        /// <summary>
        /// 老板名称
        /// </summary>
        public string BossName { get; set; }
        /// <summary>
        /// 服务到期时间-开始
        /// </summary>
        public DateTime? TenantServiceEndStart { get; set; }
        /// <summary>
        /// 服务到期时间-结束
        /// </summary>
        public DateTime? TenantServiceEndEnd { get; set; }
        /// <summary>
        /// 创建时间-开始
        /// </summary>
        public DateTime? CreateTimeStart { get; set; }
        /// <summary>
        /// 创建时间-结束
        /// </summary>
        public DateTime? CreateTimeEnd { get; set; }
        /// <summary>
        /// 创建者账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        [Required]
        [Range(1, 200)]
        public int PageSize { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        [Required]
        [Range(1, 100000)]
        public int PageIndex { get; set; }

    }
    /// <summary>
    /// 全网搜索租户信息列表返回
    /// </summary>
    public class TenantAllSearchResponse
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }
        /// <summary>
        /// 创建者名称
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 创建者账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 创建者密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 创建者验证码
        /// </summary>
        public string YZM { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary> 
        /// 到期日期
        /// </summary>
        public DateTime TenantServiceEnd { get; set; }
        /// <summary>
        /// 到期日期
        /// </summary>
        public DateTime TenantServiceStart { get; set; }
        /// <summary>
        /// 状态:1:启动,0:禁用
        /// </summary>
        public int IsEnable { get; set; }
    }

    /// <summary>
    /// 禁用启用
    /// </summary>
    public class TernantOperationEnable:BaseModel
    {
        /// <summary>
        /// 1:启用,0:禁用
        /// </summary>
        public int isEnable { get; set; }
    }

    /// <summary>
    /// 租户充值
    /// </summary>
    public class TenantBuyRequest : BaseModel
    {
        /// <summary>
        ///  充值时间
        /// </summary>
        [Range(1,100)]
        public int BuyTime { get; set; }
        /// <summary>
        /// 时间单位,年、月、日
        /// </summary>
        [Required]
        public string TimeUnit { get; set; }
        /// <summary>
        ///  充值人
        /// </summary>
        [Required]
        public string BuyUser { get; set; }
        /// <summary>
        /// 充值员工
        /// </summary>
        [Required]
        public string BuyStaff { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        [Range(0,1000000)]
        public decimal Price { get; set; }
        /// <summary>
        /// 充值方式
        /// </summary>        
        public string Way { get; set; }
        /// <summary>
        ///  充值编号
        /// </summary>
        public string Code { get; set; }

    }

}
