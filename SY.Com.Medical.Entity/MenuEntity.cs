using SY.Com.Medical.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.Entity
{

    /// <summary>
    ///  菜单    
    /// </summary>
    [DB_Table("Menus")]
    [DB_Key("MenuId")]
    public class MenuEntity : BaseEntity
    {
        /// <summary>
        /// 菜单Id    
        /// </summary>
        [DB_Key("MenuId")]
        public int MenuId { get; set; }

        /// <summary>
        /// 菜单名称/中文
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
        /// 名称/系统
        /// </summary>
        public string MenuSysName { get; set; }
        /// <summary>
        /// 图标名称
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

    }
}
