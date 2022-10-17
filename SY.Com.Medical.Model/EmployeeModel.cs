using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model
{
    /// <summary>
    /// 员工模型
    /// </summary>
    public class EmployeeModel : BaseModel
    {
        /// <summary>
        /// 员工Id    
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }


        /// <summary>
        /// 员工姓名    
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 员工电话    
        /// </summary>
        public string EmployeePhone { get; set; }

        /// <summary>
        /// 员工医保编码    
        /// </summary>
        public string YBCode { get; set; }

        /// <summary>
        /// 员工角色    
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string Departments { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string LogoImg { get; set; }
        /// <summary>
        /// 启用禁用:1:启用,2:禁用
        /// </summary>
        public int IsEnable { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
    }

    /// <summary>
    /// 获取员工信息
    /// </summary>
    public class EmployeeGetModel : BaseModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public new int UserId { get; set; }
    }

    /// <summary>
    /// 获取员工列表入参
    /// </summary>
    public class EmployeeGetsModel:PageModel
    {
        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string searchKey { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string Department { get; set; }
    }

    /// <summary>
    /// 邀请员工dto
    /// </summary>
    public class EmployeeInvity : BaseModel
    {
        /// <summary>
        /// 角色
        /// </summary>
        public string Roles { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string EmployeeName { get; set; }
    }

    /// <summary>
    /// 启用禁用员工DTO
    /// </summary>
    public class EmployeeOpenClose : BaseModel
    {
        /// <summary>
        /// 员工Id    
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 1:启用,2:禁用
        /// </summary>
        public int OpenClose { get; set; }

    }


}
