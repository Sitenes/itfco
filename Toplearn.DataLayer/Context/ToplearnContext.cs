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
            //Guid _adminUserId = Guid.NewGuid();
            //modelBuilder.Entity<User>().HasQueryFilter(n => n.IsDeleted==false).HasData(new User()
            //{
            //    Email="Admin@gmail.com",
            //    EmailLink=Guid.NewGuid(),
            //    ExpireEmailLink=DateTime.Now,
            //    RegisterDate=DateTime.Now,
            //    IsActive=true,
            //    IsDeleted=false,
            //    Password= EncodePasswordMd5("admin123456"),
            //    Phone=09013348988,
            //    UserAvatar="Default.png",
            //    UserId= _adminUserId,
            //    UserName="Admin",
            //    Wallet=9999999999,
                
            //});

            //modelBuilder.Entity<Role>().HasQueryFilter(n => n.IsDeleted==false).HasData(new Role()
            //{
            //    IsDeleted=false,
            //    RoleId=1,
            //    Title = "مدیر"
            //});

            //modelBuilder.Entity<UserRole>().HasData(new UserRole()
            //{
            //    RoleId = 1,
            //    UserId = _adminUserId,
            //    UR_Id=1,
            //});

            ////modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            ////{
            ////    RoleId = 1,
            ////    PermissionId=1,
            ////},new RolePermission()
            ////{
            ////    RoleId = 1,
            ////    PermissionId=2,
            ////},new RolePermission()
            ////{
            ////    RoleId = 1,
            ////    PermissionId=3,
            ////},new RolePermission()
            ////{
            ////    RoleId = 1,
            ////    PermissionId=4,
            ////},new RolePermission()
            ////{
            ////    RoleId = 1,
            ////    PermissionId=5,
            ////},new RolePermission()
            ////{
            ////    RoleId = 1,
            ////    PermissionId=6,
            ////},new RolePermission()
            ////{
            ////    RoleId = 1,
            ////    PermissionId=7,
            ////},new RolePermission()
            ////{
            ////    RoleId = 1,
            ////    PermissionId=8,
            ////},new RolePermission()
            ////{
            ////    RoleId = 1,
            ////    PermissionId=9,
            ////});
            //modelBuilder.Entity<Permission>().HasData(new Permission()
            //{
            //    PermissionId = 1,
            //    ParentID = null,
            //    PermissionTitle = "مدیریت",
                
            //}) ;


            //modelBuilder.Entity<WalletType>().HasData(new WalletType()
            //{
            //    Title = "واریز"
            //    , TypeId = 1,
            //},new WalletType
            //{
            //    Title = "برداشت",
            //    TypeId = 2
            //}
            //);
            base.OnModelCreating(modelBuilder);
        }
        #endregion
        #region Password Encode
        //public static string EncodePasswordMd5(string pass) //Encrypt using MD5   
        //{
        //    Byte[] originalBytes;
        //    Byte[] encodedBytes;
        //    MD5 md5;
        //    //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
        //    md5 = new MD5CryptoServiceProvider();
        //    originalBytes = ASCIIEncoding.Default.GetBytes(pass);
        //    encodedBytes = md5.ComputeHash(originalBytes);
        //    //Convert encoded bytes back to a 'readable' string   
        //    return BitConverter.ToString(encodedBytes);
        //}
        #endregion
    }

}
