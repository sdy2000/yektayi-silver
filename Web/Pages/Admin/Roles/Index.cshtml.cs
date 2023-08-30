using Core.Services.Interfaces;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Roles
{
    [PermissionChecker(9)]
    public class IndexModel : PageModel
    {
        private IPermissionService _permissionService;
        public IndexModel(IPermissionService permissionSErvise)
        {
            _permissionService = permissionSErvise;
        }

        [BindProperty]
        public List<Role> RoleList { get; set; }
        public void OnGet(string roleFilter="")
        {
            RoleList = _permissionService.GetAllRoles(roleFilter);
        }
    }
}
