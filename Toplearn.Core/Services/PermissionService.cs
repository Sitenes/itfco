using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.Permissions;
using Toplearn.DataLayer.Entities.User;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Toplearn.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ToplearnContext _context;
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;
        public PermissionService(ToplearnContext context, IMemoryCache cache, IUserService userService)
        {
            _context = context;
            _cache = cache;
            _userService = userService;
        }

        #region Roles
        public Role GetRole(int Id)
        {
            return _context.Roles.SingleOrDefault(n => n.RoleId == Id);
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.Select(n => n).Include(n=>n.RolePermissions).ThenInclude(n=>n.Permission).ToList();
        }

        public bool AddRole(Guid UserId, int Role)
        {
            try
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = Role,
                    UserId = UserId
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<int> GetUserRoles(Guid UserId)
        {
            return _context.UserRoles.Where(n => n.UserId == UserId).Select(n => n.RoleId).ToList();
        }

        public bool RemoveRole(int roleId)
        {
            try
            {
                Role role = _context.Roles.Find(roleId);
                role.IsDeleted = true;
                _context.UserRoles.Where(n => n.RoleId == roleId).ToList().ForEach(n => _context.UserRoles.Remove(n));
                _context.Roles.Update(role);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateRole(Role role)
        {
            if (role.RoleId == 0)
                return false;
            try
            {
                _context.Roles.Update(role);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<int> AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await SaveChangesAsync();
            return role.RoleId;
        }

        #endregion
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return ;
        }


        #region Permissions
        public List<Permission> GetPermissions()
        {
            var Permissions = _cache.GetOrCreate<List<Permission>>("Permissions", n =>
            {
                n.SetSlidingExpiration(TimeSpan.FromMinutes(1));
                var permissions = _context.Permissions.ToList();
                n.SetValue(permissions);
                return permissions;
            });
            return Permissions;
        }

        public async Task<bool> AddPermissionToRoleAsync(int permission,int role)
        {
            try
            {
                await _context.RolePermissions.AddAsync(new RolePermission()
                {
                    PermissionId = permission,
                    RoleId = role
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<int> PermissionsRole(int roleId)
        {
            return _context.RolePermissions.Where(n => n.RoleId == roleId).Select(n=>n.PermissionId).ToList();
        }

        public bool ResetPermissionsOfRole(int roleId)
        {

            try
            {
                var All = _context.RolePermissions;
                foreach (var rolepermission in All)
                {
                    if(rolepermission.RoleId == roleId)
                    {
                        _context.Remove(rolepermission);
                    }
                }
                
                return true;
            }
            catch (Exception)
            {
                return false ;
            }
        }
        public async Task<bool> CheckPermission(Enum.Permission permission, Guid userId)
        {
            User logedUser = await _userService.GetUserAsync(userId);
            if (logedUser == null)
                return false;

            List<int> rolesOfPermissions = await _context.RolePermissions.Where(n => n.PermissionId == ((int)permission)).Select(n=>n.RoleId).ToListAsync();
            List<int> rolesOfUser = await _context.UserRoles.Where(n => n.UserId == userId).Select(n=>n.RoleId).ToListAsync();
            if (rolesOfUser.Count == 0)
                return false;

            return rolesOfUser.Any(n=>rolesOfPermissions.Contains(n));
        }


        #endregion
    }
}
