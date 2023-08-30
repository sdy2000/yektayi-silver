using Core.Convertors;
using Core.DTOs;
using Core.Services.Interfaces;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Users
{
    [PermissionChecker(3)]
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public CreateUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public CreateUserForAdminViewModel CreateUser { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
        }
        public IActionResult OnPost(List<int> SelectedRoles)
        {
            #region VALIDATION

            if (!ModelState.IsValid)
            {
                ViewData["IsSuccess"] = false;
                ViewData["Roles"] = _permissionService.GetAllRoles();
                return Page();
            }
            if (_userService.IsExistUserName(CreateUser.UserName))
            {
                ModelState.AddModelError("CreateUser.UserName", "نام کاربری موجود می باشد !");
                ViewData["Roles"] = _permissionService.GetAllRoles();
                return Page();
            }
            if (_userService.IsExistEmail(FixedText.FixedEmail(CreateUser.Email)))
            {
                ModelState.AddModelError("CreateUser.Email", "ایمیل وارد شده موجود می باشد !");
                ViewData["Roles"] = _permissionService.GetAllRoles();
                return Page();
            }

            #endregion

            int userId = _userService.AddUserFromAdminPanel(CreateUser);

            _permissionService.AddUserRole(SelectedRoles, userId);

            if (CreateUser.IsActive == 3)
            {
                return RedirectToAction("AddUserFromAdmin", "Account", new { id = userId });
            }

            return Redirect("/Admin/Users");
        }

    }
}
