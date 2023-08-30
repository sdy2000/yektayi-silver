using Core.Convertors;
using Core.DTOs;
using Core.Senders;
using Core.Services.Interfaces;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRender;
        public HomeController(IUserService userService, IViewRenderService viewRender)
        {
            _userService = userService;
            _viewRender = viewRender;
        }


        public IActionResult Index()
        {
            return View(_userService.GetUserInformation(User.Identity.Name));
        }

        #region EDIT PROFILE

        [Route("EditProfile")]
        public IActionResult EditProfile()
        {
            EditProfileViewModel editProfile = _userService.GetDataForEditProfile(User.Identity.Name);
            List<SelectListItem> genderlist = new List<SelectListItem>()
            {
                new SelectListItem(){Text="انتخاب کنید",Value="0"}
            }; genderlist.AddRange(_userService.GetGender());
            ViewData["Genders"] = new SelectList(genderlist, "Value", "Text", editProfile.GenderId);

            return View(editProfile);
        }

        [HttpPost]
        [Route("EditProfile")]
        public IActionResult EditProfile(EditProfileViewModel editProfile)
        {
            User user = _userService.GetUserByUserName(User.Identity.Name);
            string newEmail = FixedText.FixedEmail(editProfile.Email);

            #region VALIDATION

            if (!ModelState.IsValid)
            {
                List<SelectListItem> genderlist = new List<SelectListItem>()
                {
                    new SelectListItem(){Text="انتخاب کنید",Value="0"}
                }; genderlist.AddRange(_userService.GetGender());
                ViewData["Genders"] = new SelectList(genderlist, "Value", "Text", editProfile.GenderId);
                return View(editProfile);
            }

            #endregion

            if (newEmail != user.Email)
            {
                if (_userService.IsExistEmail(newEmail))
                {
                    List<SelectListItem> genderlist = new List<SelectListItem>()
                    {
                        new SelectListItem(){Text="انتخاب کنید",Value="0"}
                    }; genderlist.AddRange(_userService.GetGender());
                    ViewData["Genders"] = new SelectList(genderlist, "Value", "Text", editProfile.GenderId);
                    ModelState.AddModelError("Email", "ایمیل وارد شده تکراری میباشد !");
                    return View(editProfile);
                }

                #region SEND ACTIVATION EMAIL

                string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
                SendEmail.Send(newEmail, "تغیر ایمیل کاربری", body);

                #endregion

                //LOG OUT USER
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


                _userService.EditProfile(editProfile, User.Identity.Name);

                // REDIRECT TO LOG IN
                return Redirect("/Login?EditProfile=true");
            }

            _userService.EditProfile(editProfile, User.Identity.Name);
            return Redirect("/UserPanel");
        }

        #endregion

        #region EDIT PASSWORD

        [Route("EditPassword")]
        public IActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("EditPassword")]
        public IActionResult EditPassword(ChengePasswordViewModel chengePas)
        {
            #region VALIDATION 

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("RePassword", "عملیات با شکست مواجه شد !");
                return View(chengePas);
            }

            if (!_userService.CompareOldPassword(chengePas.OldPassword,User.Identity.Name))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی معتبر نم یباشد !");
                return View(chengePas);
            }

            #endregion

            _userService.ChengPassword(chengePas.Password, User.Identity.Name);

            //LOG OUT USER
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login?ChengPassword=true");
        }

        #endregion
    }
}
