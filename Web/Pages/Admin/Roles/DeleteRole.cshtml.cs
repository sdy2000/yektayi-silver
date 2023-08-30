using Core.Services.Interfaces;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace Web.Pages.Admin.Roles
{
    [PermissionChecker(12)]
    public class DeleteRoleModel : PageModel
    {
        private IPermissionService _permission;
        public DeleteRoleModel(IPermissionService permission)
        {
            _permission = permission;
        }

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int id)
        {
            ViewData["PermissionTitle"] = _permission.GetRolePermissionTitle(id);
            Role=_permission.GetRoleById(id);
        }
        public IActionResult OnPost()
        {
            Role role = _permission.GetRoleById(Role.RoleId);
            if (role == null)
                return BadRequest();

            role.IsDelete = true;
            _permission.UpdateRole(role);

            return Redirect("/Admin/Roles");
        }
    }
}
