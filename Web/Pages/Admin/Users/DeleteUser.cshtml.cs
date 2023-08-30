using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Users
{
    [PermissionChecker(5)]
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;
        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public DeleteUserForAdminViewModel DeleteUser { get; set; }
        public void OnGet(int id)
        {
            DeleteUser = _userService.GetUserForDeleteFromAdmin(id);
        }

        public IActionResult OnPost()
        {
            _userService.DeleteUser(DeleteUser.UserId);

            return Redirect("/Admin/Users");
        }
    }
}
