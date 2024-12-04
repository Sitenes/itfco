using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Toplearn.DataLayer.Entities.Courses;
using Toplearn.DataLayer.Entities.Permissions;
using Toplearn.DataLayer.Entities.User;
using Toplearn.DataLayer.Entities.Wallet;

namespace Toplearn.DataLayer.Context
{
    public class ToplearnContext:DbContext
    {
        public ToplearnContext(DbContextOptions<ToplearnContext> option):base(option)
        {

        }

        #region DBSets

        #region User Roles
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region Wallet
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletType { get; set; }
        #endregion



        #region Permissions Roles

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Courses
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<Episode> Episode { get; set; }
        #endregion

        #endregion

        #region ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adding Permissions
            var permissions = Enum.GetValues(typeof(Core.AllEnums.PermissionEnum))
                                  .Cast<Core.AllEnums.PermissionEnum>()
                                  .Select(permission => new
                                  {
                                      PermissionId = (int)permission,
                                      ParentID = (int?)null, // اگر Parent نیاز است، مقدار مناسب قرار دهید
                                      PermissionTitle = permission.ToString()
                                  })
                                  .ToList();

            modelBuilder.Entity<Permission>().HasData(permissions);

            // Example of Admin User
            Guid _adminUserId = Guid.NewGuid();
            modelBuilder.Entity<User>().HasQueryFilter(n => n.IsDeleted == false).HasData(new User()
            {
                Email = "Admin@gmail.com",
                EmailLink = Guid.NewGuid(),
                ExpireEmailLink = DateTime.Now,
                RegisterDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                Password = EncodePasswordMd5("admin123456"),
                Phone = 09013348988,
                UserAvatar = "",
                UserId = _adminUserId,
                UserName = "Admin",
                Wallet = 9999999999,
            });

            // Adding Role and assigning all permissions to it
            modelBuilder.Entity<Role>().HasQueryFilter(n => n.IsDeleted == false).HasData(new Role()
            {
                IsDeleted = false,
                RoleId = 1,
                Title = "مدیر"
            });

            modelBuilder.Entity<UserRole>().HasData(new UserRole()
            {
                RoleId = 1,
                UserId = _adminUserId,
                UR_Id = 1,
            });

            // Adding Role Permissions
            var rolePermissions = permissions.Select(permission => new
            {
                RoleId = 1,
                PermissionId = permission.PermissionId,
                RP_Id = permission.PermissionId // مقدار منحصر به فرد برای هر Permission
            }).ToList();

            modelBuilder.Entity<RolePermission>().HasData(rolePermissions);

            base.OnModelCreating(modelBuilder);
        }

        #endregion
        #region Password Encode
        public static string EncodePasswordMd5(string pass) //Encrypt using MD5   
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string   
            return BitConverter.ToString(encodedBytes);
        }
        #endregion
    }

}
