using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model
{
    /// <summary>
    /// 用户模型
    /// </summary>
    public class UserModel : BaseModel
    {


        /// <summary>
        /// 账号    
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码md5小写    
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 验证码    
        /// </summary>
        public string YZM { get; set; }
        /// <summary>
        /// 头像图片
        /// </summary>
        public string LogoImg { get; set; }
    }

    /// <summary>
    /// 登录入参
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码(md5小写)
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        ///  验证码有效性Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// 登录出参
    /// </summary>
    public class LoginResponse : BaseModel
    { 
        /// <summary>
        /// Jwt验证token,后续请求中放入Header的Authorization中
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 头像路径全路径
        /// </summary>
        public string LogoImg { get; set; }
        /// <summary>
        /// 用户Id,通过用户Id和诊所Id换取员工id(EmployeeId)
        /// </summary>
        public int Uid { get; set; }

    }

    /// <summary>
    /// 注册入参
    /// </summary>
    public class RegisterRequest : BaseModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string YZM { get; set; }        
    }


    /// <summary>
    /// 注册出参
    /// </summary>
    public class RegisterResponse
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
    }

    /// <summary>
    /// 重置密码入参
    /// </summary>
    public class ResetRequest : BaseModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        public string Pwd2 { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string YZM { get; set; }
    }


    /// <summary>
    /// 重置密码出参
    /// </summary>
    public class ResetResponse
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
    }

    /// <summary>
    ///  修改密码
    /// </summary>
    public class ChangeRequst : BaseModel
    {        
        /// <summary>
        /// 新密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 新密码确认
        /// </summary>

        public string PwdConfirm { get; set; }
    }

    /// <summary>
    /// 图片验证码
    /// </summary>
    public class VerifyCode
    {
        /// <summary>
        ///  验证码有效性Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// 图片验证码Token结构
    /// </summary>
    public class VerifyToken
    {
        /// <summary>
        /// 创建时间Unix时间戳
        /// </summary>
        public long CreateOn { get; set; }

        /// <summary>
        /// 生成的唯一UUID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 生成的随机密码
        /// </summary>
        public string Code { get; set; }
    }


}
