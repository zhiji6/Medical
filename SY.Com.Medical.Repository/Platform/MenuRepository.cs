using Dapper;
using SY.Com.Medical.Entity;
using SY.Com.Medical.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Repository.Platform
{
    /// <summary>
    /// 菜单数据访问层
    /// </summary>
    public class MenuRepository : BaseRepository<MenuEntity>
    {

        /// <summary>
        /// 返回系统菜单
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MenuEntity> getSystem()
        {
            string sql = @" Select * From Menus
                            Where IsEnable=@IsEnable And IsDelete=@IsDelete
                            Order By Sort Desc ";
            return _db.Query<MenuEntity>(sql, new { IsEnable = (int)Enable.启用, IsDelete = (int)Enum.Delete.正常 });
        }

        /// <summary>
        /// 返回某一个租户的菜单
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public IEnumerable<MenuEntity> getTenant(int TenantId)
        {
            string sql = @" Select A.* From Menus as A
                            Inner Join TenantMenus as B On A.MenuId = B.MenuId 
                            Where B.TenantId = @TenantId And A.IsEnable=@IsEnable And A.IsDelete=@IsDelete And B.IsEnable=@IsEnable And B.IsDelete=@IsDelete";
            return _db.Query<MenuEntity>(sql, new { TenantId= TenantId,  IsEnable = (int)Enable.启用, IsDelete = (int)Enum.Delete.正常 });
        }

        /// <summary>
        /// 初始换租户的菜单
        /// </summary>
        /// <param name="TenantId"></param>
        public void InitTenantMenu(int TenantId)
        {
            string sql = @" Insert Into TenantMenus(MenuId,TenantId,CreateTime,IsEnable,IsDelete)
                            Select MenuId,@TenantId,getdate(),IsEnable,IsDelete 
                            From Menus 
                            Where IsEnable=@IsEnable And IsDelete=@IsDelete";
            _db.Execute(sql, new { TenantId = TenantId, IsEnable = (int)Enable.启用, IsDelete = (int)Enum.Delete.正常 });
        }

        /// <summary>
        /// 根据MenuIds查询菜单
        /// </summary>
        /// <param name="MenuIds"></param>
        /// <returns></returns>
        public IEnumerable<MenuEntity> MenuByIds(List<int> MenuIds)
        {
            string sql = @" Select * From Menus
                            Where MenuId in @MenuId IsEnable = @IsEnable And IsDelete=@IsDelete  ";
            return _db.Query<MenuEntity>(sql, new { MenuId = MenuIds.ToArray(), IsEnable = (int)Enable.启用, IsDelete = (int)Enum.Delete.正常 });
        }


    }
}
