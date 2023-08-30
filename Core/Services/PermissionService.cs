using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Permissions;
using DataLayer.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Core.Services
{
    public class PermissionService : IPermissionService
    {
        private YektayiSilverContext _context;
        public PermissionService(YektayiSilverContext context)
        {
            _context = context;
        }

        // // // // // // // // // // ROLE

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();

            return role.RoleId;
        }

        public int UpdateRole(Role role)
        {
            Role updateRole = GetRoleById(role.RoleId);

            updateRole.RoleTitle = role.RoleTitle;
            updateRole.IsDelete = role.IsDelete;

            _context.Roles.Update(updateRole);
            _context.SaveChanges();

            return role.RoleId;
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public bool IsExsitRole(string roleTitle)
        {
            return _context.Roles.Any(r => r.RoleTitle == roleTitle && !r.IsDelete);
        }

        public List<Role> GetAllRoles(string roleNameFilter = "")
        {
            IQueryable<Role> result = _context.Roles;

            if (!string.IsNullOrEmpty(roleNameFilter))
            {
                result = result.Where(r => r.RoleTitle == roleNameFilter);
            }


            return result.ToList();
        }

        public string GetRoleTitleByRoleId(int roleId)
        {
            return _context.Roles.Find(roleId).RoleTitle;
        }

        public bool AddUserRole(List<int> selectedRole, int userId)
        {
            try
            {
                foreach (int role in selectedRole)
                {
                    _context.UserRoles.Add(new UserRole()
                    {
                        RoleId = role,
                        UserId = userId
                    });
                }
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateUesrRole(List<int> selectedRole, int userId)
        {
            try
            {
                _context.UserRoles
                    .Where(up => up.UserId == userId)
                    .ToList()
                    .ForEach(up => _context.UserRoles.Remove(up));

                return AddUserRole(selectedRole,userId);
            }
            catch
            {
                return false;
            }
        }


        // // // // // // // // // // PERMISSION


        public void AddPermissionToRole(List<int> permissionIds, int roleId)
        {
            foreach (int permissionId in permissionIds)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    PermissionId = permissionId,
                    RoleId = roleId
                });
            }

            _context.SaveChanges();
        }

        public void UpdateRolePermission(List<int> permissionIds, int roleId)
        {
            _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .ToList()
                .ForEach(rp => _context.RolePermissions.Remove(rp));

            AddPermissionToRole(permissionIds, roleId);
        }

        public List<Permission> GetAllPermissions()
        {
            return _context.Permissions.ToList();
        }

        public List<int> GetRolePermission(int roleId)
        {
            return _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.PermissionId)
                .ToList();
        }

        public List<string> GetRolePermissionTitle(int roleId)
        {
            return _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Include(rp => rp.Permission)
                .Select(p => p.Permission.PermissionTitle)
                .ToList();
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            List<int> userRole = _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToList();

            if (!userRole.Any())
                return false;

            List<int> rolePermission = _context.RolePermissions
                .Where(rp => rp.PermissionId == permissionId)
                .Select(rp => rp.RoleId)
                .ToList();

            return rolePermission.Any(p => userRole.Contains(p));
        }
    }
}
