using Core.DTOs;
using Core.Services.Interfaces;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Convertors;

namespace Web.Pages.Admin.Users
{
    [PermissionChecker(4)]
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IViewRenderService _viewRender;
        private IPermissionService _permissionService;
        public EditUserModel(IUserService userService,
            IViewRenderService viewRender, IPermissionService permissionService)
        {
            _userService = userService;
            _viewRender = viewRender;
            _permissionService = permissionService;
        }

        [BindProperty]
        public EditUserForAdminViewModel EditUser { get; set; }
        public void OnGet(int id)
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
            EditUser = _userService.GetUserForEditInAdmin(id);
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            User user = _userService.GetUserById(EditUser.UserId);
            string newEmail = FixedText.FixedEmail(user.Email);

            #region VALIDATION

            if (!ModelState.IsValid || user == null)
            {
                ViewData["IsSuccess"] = false;
                ViewData["Roles"] = _permissionService.GetAllRoles();
                return Page();
            }
            if (EditUser.UserName != user.UserName)
            {
                if (_userService.IsExistUserName(EditUser.UserName))
                {
                    ModelState.AddModelError("EditUser.UserName", "نام کاربری وارد شده تکراری می باشد !");
                    ViewData["Roles"] = _permissionService.GetAllRoles();
                    return Page();
                }
            }
            if (newEmail != user.Email)
            {
                if (_userService.IsExistEmail(newEmail))
                {
                    ModelState.AddModelError("EditUser.Email", "ایمیل وارد شده تکراری می باشد !");
                    ViewData["Roles"] = _permissionService.GetAllRoles();
                    return Page();
                }
            }

            #endregion

            int userId = _userService.EditUserFromAdminPanel(EditUser);
            _permissionService.UpdateUesrRole(SelectedRoles, userId);// <== UPDATE USER ROLES


            if (EditUser.IsActive == 3)
            {
                return RedirectToAction("AddUserFromAdmin", "Account", new { id = EditUser.UserId });
            }

            return Redirect("/Admin/Users");
        }
    }
}
