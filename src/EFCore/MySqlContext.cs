using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;


namespace EFCore
{
    public class MySqlContext:DbContext
    {

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
        }
        //private string _connectionString;
        //public MySqlContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseMySql(_connectionString);
        //    optionsBuilder.UseMySql("server=5940b72c83b87.sh.cdb.myqcloud.com;port=11131;uid=root;pwd=iuEKm1vK+Oqd5TQ=;database=AdminSystem;Min Pool Size=0;Pooling=true;connect timeout=120;CharSet=utf8");
        //}

        public DbSet<AdminMenu> AdminMenus { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<AdminRoleMenu> AdminRoleMenus { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<AdminUserRole> AdminUserRoles { get; set; }
        public DbSet<ZJ_User> ZJ_Users { get; set; }
        public DbSet<ZJ_Chat> ZJ_Chats { get; set; }
        public DbSet<ZJ_Balance> ZJ_Balances { get; set; }
        public DbSet<ZJ_WXCommon_User> ZJ_WXCommon_Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminMenu>().ToTable("Admin_Menu");
            modelBuilder.Entity<AdminRole>().ToTable("Admin_Role");
            modelBuilder.Entity<AdminRoleMenu>().ToTable("Admin_RoleAndMenu");
            modelBuilder.Entity<AdminUser>().ToTable("Admin_User");
            modelBuilder.Entity<AdminUserRole>().ToTable("Admin_UserAndRole");

            modelBuilder.Entity<ZJ_User>().ToTable("zj_user");
            modelBuilder.Entity<ZJ_Chat>().ToTable("zj_chat");
            modelBuilder.Entity<ZJ_Balance>().ToTable("zj_balance");
            modelBuilder.Entity<ZJ_WXCommon_User>().ToTable("zj_wxcommon_user");
        }


    }

    
}
