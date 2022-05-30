using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Model
{
    /// <summary>
    /// 菜单Model层
    /// </summary>

    public class MenuModel :BaseModel
    {

    }

    /// <summary>
    /// 一维菜单
    /// </summary>
    public class MenuResponse : BaseModel
    {
        /// <summary>
        /// 菜单Id    
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 菜单名称    
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单路由    
        /// </summary>
        public string MenuRoute { get; set; }

        /// <summary>
        /// 父路由,一级菜单为0    
        /// </summary>
        public int MenuParent { get; set; }
        /// <summary>
        /// 菜单名称/系统
        /// </summary>
        public string MenuSysName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 拥有与否
        /// true:拥有
        /// false:不拥有
        /// </summary>
        public bool Have { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }

    /// <summary>
    /// 一级菜单和二级菜单,二维树结构
    /// </summary>
    public class MenuTreeResponse : BaseModel
    {
        /// <summary>
        /// 菜单Id    
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 菜单名称    
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单路由    
        /// </summary>
        public string MenuRoute { get; set; }

        /// <summary>
        /// 父路由,一级菜单为0    
        /// </summary>
        public int MenuParent { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string MenuSysName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 是否拥有
        /// true:拥有
        /// false:不拥有
        /// </summary>
        public bool Have { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuResponse> SubMenu { get; set; }
    }

    /// <summary>
    /// 菜单键值对
    /// </summary>
    public class MenuKeyValueDto:BaseModel
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
    }



}
