using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class AdminUserRole
    {
        public int Id { get; set; }


        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public AdminUser User { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public AdminRole Role { get; set; }
    }
}
