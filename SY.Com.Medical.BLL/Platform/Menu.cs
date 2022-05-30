using SY.Com.Medical.Entity;
using SY.Com.Medical.Model;
using SY.Com.Medical.Repository.Platform;
using SY.Com.Medical.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.BLL.Platform
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu
    {
        private MenuRepository db;
        public Menu()
        {
            db = new MenuRepository();
        }

        /// <summary>
        /// 获取系统菜单一维
        /// </summary>
        /// <returns></returns>
        public List<MenuEntity> GetSystem()
        {
            List<MenuResponse> responses = new List<MenuResponse>();
            var entitys = db.getSystem();            
            return entitys.ToList();
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public List<MenuTreeResponse> GetSystemTree(IEnumerable<MenuEntity> entitys)
        {
            if (entitys == null || !entitys.Any())
            {
                entitys = new List<MenuEntity>();
            }
            
            List<MenuResponse> responses = new List<MenuResponse>();
            List<MenuTreeResponse> responses2 = new List<MenuTreeResponse>();
            var sysentitys = db.getSystem();
            if(sysentitys ==null || !sysentitys.Any())
            {
                throw new MyException("系统菜单为空");
            }


            sysentitys.ToList().ForEach(entity => responses.Add(entity.EntityToDto<MenuResponse>()));
            responses.ForEach(x =>
            {
                if (entitys.Any(m => m.MenuId == x.MenuId))
                {
                    x.Have = true;
                }
                else {
                    x.Have = false;
                }                
                if (x.MenuParent != 0)
                {
                    responses2.Find(y => y.MenuId == x.MenuParent).SubMenu.Add(x);
                }
                else
                {
                    responses2.Add(new MenuTreeResponse
                    {
                        Have = x.Have,
                        MenuId = x.MenuId,
                        MenuName = x.MenuName,
                        MenuRoute = x.MenuRoute,
                        MenuParent = x.MenuParent,
                        MenuSysName = x.MenuSysName,
                        Sort = x.Sort,
                        Icon = x.Icon,
                        SubMenu = new List<MenuResponse>()
                    }); ;
                }
            });
            return responses2;
        }


        /// <summary>
        /// 根据多个角色获取菜单
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<MenuTreeResponse> GetEmployee(List<Role> roles)
        {

            return new List<MenuTreeResponse>();
        }


    }
}
