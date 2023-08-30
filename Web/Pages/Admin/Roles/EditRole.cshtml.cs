using Core.Services.Interfaces;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Roles
{
    [PermissionChecker(11)]
    public class EditRoleModel : PageModel
    {
        private IPermissionService _permissionServise;
        public EditRoleModel(IPermissionService permissionServise)
        {
            _permissionServise = permissionServise;
        }

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int id)
        {
            ViewData["Permissions"] = _permissionServise.GetAllPermissions();
            ViewData["RolePermissions"] = _permissionServise.GetRolePermission(id);
            Role = _permissionServise.GetRoleById(id);
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            #region VALIDATION 

            if (!ModelState.IsValid)
            {
                ViewData["Permissions"] = _permissionServise.GetAllPermissions();
                ViewData["RolePermissions"] = _permissionServise.GetRolePermission(Role.RoleId);
                ModelState.AddModelError("Role.RoleTitle", "عنوان نقش الزامی میباشد !");
                return Page();
            }

            if (Role.RoleTitle != _permissionServise.GetRoleTitleByRoleId(Role.RoleId))
            {
                if (_permissionServise.IsExsitRole(Role.RoleTitle))
                {
                    ViewData["Permissions"] = _permissionServise.GetAllPermissions();
                    ViewData["RolePermissions"] = _permissionServise.GetRolePermission(Role.RoleId);
                    ModelState.AddModelError("Role.RoleTitle", "عنوان نقش تکراری میباشد !");
                    return Page();
                }
            }

            #endregion
            
            int roleId = _permissionServise.UpdateRole(Role);

            _permissionServise.UpdateRolePermission(SelectedPermission, roleId);

            return Redirect("/Admin/Roles");
        }
    }
}
