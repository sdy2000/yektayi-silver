using Core.DTOs;
using Core.Security;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Users
{
    [PermissionChecker(2)]
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserForAdminViewModel UserForAdminViewModel { get; set; }
        public void OnGet(int PageId = 0, string filterUserName = "",
            string filterEmail = "", int genderId = 0, int take = 10)
        {
            UserForAdminViewModel = _userService.GetUserForAdmin();
        }
    }
}
