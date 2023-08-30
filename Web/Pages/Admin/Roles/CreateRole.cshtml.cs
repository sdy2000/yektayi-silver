using Core.Services.Interfaces;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Roles
{
    [PermissionChecker(10)]
    public class CreateRoleModel : PageModel
    {
        private IPermissionService _permissionServise;
        public CreateRoleModel(IPermissionService permissionServise)
        {
            _permissionServise = permissionServise;
        }

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet()
        {
            ViewData["Permissions"] = _permissionServise.GetAllPermissions();
        }
        public IActionResult OnPost(List<int> SelectedPermission)
        {
            #region VALIDATION 

            if (!ModelState.IsValid)
            {
                ViewData["Permissions"] = _permissionServise.GetAllPermissions();
                ModelState.AddModelError("Role.RoleTitle", "عنوان نقش الزامی میباشد !");
                return Page();
            }
            if (_permissionServise.IsExsitRole(Role.RoleTitle))
            {
                ViewData["Permissions"] = _permissionServise.GetAllPermissions();
                ModelState.AddModelError("Role.RoleTitle", "عنوان نقش تکراری میباشد !");
                return Page();
            }

            #endregion

            int roleId = _permissionServise.AddRole(Role);

            _permissionServise.AddPermissionToRole(SelectedPermission, roleId);

            return Redirect("/Admin/Roles");
        }
    }
}
