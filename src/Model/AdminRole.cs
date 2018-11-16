using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class AdminRole
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 状态 默认0
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建状态
        /// </summary>
        public DateTime StatusTime { get; set; } = DateTime.Now;
        public List<AdminUserRole> UserRoles { get; set; }
        public List<AdminRoleMenu> RoleMenus { get; set; }
    }
}
