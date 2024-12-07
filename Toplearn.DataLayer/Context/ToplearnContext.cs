using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Toplearn.DataLayer.Entities.Courses;
using Toplearn.DataLayer.Entities.User;
using Toplearn.DataLayer.Entities.Wallet;
using Toplearn.DataLayer.Entities.Permissions;

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



        #region Permission Roles

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
            // Adding Categories
            var permissions = Enum.GetValues(typeof(Core.AllEnums.PermissionEnum))
                                  .Cast<Core.AllEnums.PermissionEnum>()
                                  .Select(permission => new
                                  {
                                      PermissionId = (int)permission, // اضافه کردن مقدار یکتا
                                      ParentID = (int?)null, // اگر Parent نیاز است، مقدار مناسب قرار دهید
                                      PermissionTitle = permission.ToString()
                                  })
                                  .ToList();

            modelBuilder.Entity<Permission>().HasData(permissions);


            // Example of Admin User
            Guid _adminUserId = new Guid("1eb79e08-e5ce-486d-9909-65997e781c53");
            modelBuilder.Entity<User>().HasQueryFilter(n => n.IsDeleted == false).HasData(new User()
            {
                Email = "admin@gmail.com",
                EmailLink = _adminUserId,
                ExpireEmailLink = DateTime.MinValue,
                RegisterDate = DateTime.MinValue,
                IsActive = true,
                IsDeleted = false,
                Password = EncodePasswordMd5("admin123456"),
                Phone = 9999999999,
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

            // Adding Role Categories
            var rolePermissions = permissions.Select(permission => new
            {
                RoleId = 1,
                PermissionId = permission.PermissionId,
                RP_Id = permission.PermissionId // مقدار منحصر به فرد برای هر Category
            }).ToList();

            modelBuilder.Entity<RolePermission>().HasData(rolePermissions);

            modelBuilder.Entity<RolePermission>().HasQueryFilter(rp => !rp.Role.IsDeleted);
            modelBuilder.Entity<UserRole>().HasQueryFilter(ur => !ur.Role.IsDeleted && !ur.User.IsDeleted);

			modelBuilder.Entity<CourseGroup>().HasQueryFilter(x => !x.IsDeleted);

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
