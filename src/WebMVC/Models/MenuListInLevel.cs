using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
namespace WebCore.Models
{
    public class ViewMenu
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 类型 1:菜单文件夹; 2:一级页面菜单; 3:二级菜单(功能button)
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 父Id 默认0
        /// </summary>
        public int ParentId { get; set; } = 0;
        /// <summary>
        /// 排序号 默认0
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 是否显示 默认显示
        /// </summary>
        public bool IsDisplay { get; set; } = true;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 状态 默认0
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 是否拥有此菜单权限 默认false
        /// </summary>
        public bool Own { get; set; } = false;


        public List<ViewMenu> SubMenus { get; set; }
    }

   
}
