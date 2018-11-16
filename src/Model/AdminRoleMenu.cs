using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class AdminRoleMenu
    {
        [Key]
        public int Id { get; set; }

        
        public int MenuId { get; set; }
        [ForeignKey("MenuId")]
        public AdminMenu Menu { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public AdminRole Role { get; set; }

        /// <summary>
        /// 修改权限
        /// </summary>
        public bool? Modify { get; set; } 

        /// <summary>
        /// 查询权限
        /// </summary>
        public bool? Query { get; set; }

        /// <summary>
        /// 删除权限
        /// </summary>
        public bool? Delete { get; set; }
    }
}
