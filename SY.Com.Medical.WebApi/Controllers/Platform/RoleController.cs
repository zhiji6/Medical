using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.BLL.Platform;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SY.Com.Medical.WebApi.Controllers.Platform
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [Authorize]
    [Api_Tenant]
    [ApiController]
    public class RoleController : ControllerBase
    {
        Role bll = new Role();
        /// <summary>
        /// 获取租户的所有角色-不分页
        /// 一般机构角色在10个以内
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<RoleModel>> gets(BaseModel request)
        {
            BaseResponse<List<RoleModel>> result = new BaseResponse<List<RoleModel>>();
            result.Data =  bll.getTenantRoles(request.TenantId);            
            if(result.Data == null && !result.Data.Any())
            {
                return result.busExceptino(Enum.ErrorCode.数据为空, "结果为空");
            }
            return result;
        }


        /// <summary>
        /// 租户新增角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> insert(RoleUpdateMenuModel request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            result.Data = bll.InsertRole(request);
            return result;
        }

        /// <summary>
        /// 租户删除角色信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> delete(RoleOnlyIdModel request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            result.Data = bll.delete( request);
            return result;
        }

        /// <summary>
        /// 获取租户角色的菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<MenuTreeResponse>> getMenus(RoleOnlyIdModel request)
        {
            BaseResponse<List<MenuTreeResponse>> result = new BaseResponse<List<MenuTreeResponse>>();
            result.Data =  bll.getMenu(new List<RoleModel> { new RoleModel() { RoleId = request.RoleId } });
            if (result.Data == null && !result.Data.Any())
            {
                return result.busExceptino(Enum.ErrorCode.数据为空, "结果为空");
            }
            return result;
        }

        /// <summary>
        /// 租户修改角色的信息
        /// 包括基本信息:角色名称
        /// 包括菜单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> update(RoleUpdateMenuModel request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            result.Data = bll.updateMenu(new RoleModel() { RoleId = request.RoleId, RoleName = request.RoleName, TenantId = request.TenantId, UserId = request.UserId }
            , request.MenuIds);
            return result;
        }



    }
}
