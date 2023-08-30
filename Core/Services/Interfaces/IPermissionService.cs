using DataLayer.Entities.Permissions;
using DataLayer.Entities.Users;

namespace Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region ROLE

        int AddRole(Role role);
        int UpdateRole(Role role);
        Role GetRoleById(int roleId);
        bool IsExsitRole(string roleTitle);
        List<Role> GetAllRoles(string roleNameFilter = "");
        string GetRoleTitleByRoleId(int roleId);
        bool AddUserRole(List<int> selectedRole, int userId);
        bool UpdateUesrRole(List<int> selectedRole, int userId);

        #endregion

        #region PERMISSION

        void AddPermissionToRole(List<int> permissionIds, int roleId);
        void UpdateRolePermission(List<int> permissionIds, int roleId);
        List<Permission> GetAllPermissions();
        List<int> GetRolePermission(int roleId);
        List<string> GetRolePermissionTitle(int roleId);
        bool CheckPermission(int permissionId, string userName);

        #endregion
    }
}
