using Core.Convertors;
using Core.DTOs;
using Core.Generators;
using Core.Services.Interfaces;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Core.Security;
using Core.Senders;

namespace Web.Controllers
{
	public class AccountController : Controller
	{
        private IUserService _userService;
        private IViewRenderService _viewRender;
        public AccountController(IUserService userService,IViewRenderService viewRender)
        {
            _userService = userService;
            _viewRender = viewRender;
        }

        #region REGISTER

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            #region VALIDATION

            if (!ModelState.IsValid || register.Password != register.RePassword)
            {
                ModelState.AddModelError("Rules", "اطلاعات وارد شده معتبر نمی باشد !");
                return View(register);
            }
            if (!register.Rules)
            {
                ModelState.AddModelError("Rules", "لطفا قوانین سایت را مطاعه کرده و قبول کنید !");
                return View(register);
            }
            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد !");
                return View(register);
            }
            if (_userService.IsExistEmail(register.Email))
            {
                ModelState.AddModelError("Email", "ایمیل تکراری میباشد !");
                return View(register);
            }

            #endregion

            User user = new User()
            {
                UserName = register.UserName,
                Email = FixedText.FixedEmail(register.Email),
                Password=PasswordHelper.EncodePasswordMd5(register.Password),
                RegisterDate=DateTime.Now,
                ActiveCode=NameGenerator.GeneratorUniqCode(),
                UserAvatar= "Defult.png",
                Rulse=register.Rules
            };

            _userService.AddUser(user);

            #region SEND ACTIVE EMAIL

            var body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "ایمیل فعال سازی", body);

            #endregion

            return View("_SuccessRegister",user);
        }

        #endregion

        #region LOGIN

        [Route("Login")]
        public IActionResult Login(bool EditProfile=false,bool ChengPassword=false)
        {
            ViewBag.ChengPassword = ChengPassword;
            ViewBag.EditProfile = EditProfile;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            User user = _userService.LoginUser(login);

            #region VALIDATION

            if (!ModelState.IsValid || user == null)
            {
                ViewBag.Login = 3;
                return View(login);
            }
            if (!user.IsActive || user.IsDelete)
            {
                ViewBag.Login = 2;
                return View(login);
            }

            #endregion

            #region LOG IN USER

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };
            HttpContext.SignInAsync(principal, properties);

            #endregion

            ViewBag.Login = 1;
            return View();
        }

        #endregion

        #region LOGOUT

        [Route("Logout")]
        public IActionResult Logout()
        {
            #region LOGOUT USER

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            #endregion

            return Redirect("Login");
        }

        #endregion

        #region ACTIVE ACCOUNT

        [Route("ActiveAccount/{id}")]
        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }

        #endregion

        #region FORGOT PASSWORD

        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            string email = FixedText.FixedEmail(forgot.Email);
            User user = _userService.GetUserByEmail(email);

            #region VALIDATION

            if (user == null)
            {
                ModelState.AddModelError("Email", "حسابی با این مشخصات یافت نشد !");
                return View(forgot);
            }
            if (!ModelState.IsValid || user.IsDelete)
            {
                ViewBag.IsSuccess = false;
                return View(forgot);
            }
            if (!user.IsActive)
            {
                ModelState.AddModelError("Email", "حسابی کاربری شما فعال نمی باشد !");
                return View(forgot);
            }

            #endregion

            #region SEND RESET PASSWORD TO EMAIL

            var body = _viewRender.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.Send(email, "بازیابی کلمه عبور", body);

            #endregion

            user.IsActive = false;
            _userService.UpdateUser(user);

            ViewBag.IsSuccess = true;
            return View();
        }

        #endregion

        #region RESET PASSWORD 

        [Route("ResetPassword/{id}")]
        public IActionResult ResetPassword(string id)
        {
            User user = _userService.GetUserByActiveCode(id); 
            if(user == null || user.IsActive || user.IsDelete)
                return BadRequest();
            

            return View(new ResetPasswordVeiwModel()
            {
                ActiveCode = id
            });
        }

        [HttpPost]
        [Route("ResetPassword/{id}")]
        public IActionResult ResetPassword(ResetPasswordVeiwModel resetPas)
        {
            User user = _userService.GetUserByActiveCode(resetPas.ActiveCode);

            #region VALIDATION

            if (!ModelState.IsValid || resetPas.Password != resetPas.RePassword)
            {
                ViewBag.IsSuccess = false;
                return View(resetPas);
            }
            if(user == null)
            {
                return BadRequest();
            }

            #endregion

            string hashPassword = PasswordHelper.EncodePasswordMd5(resetPas.Password);
            user.Password = hashPassword;
            user.IsActive = true;
            _userService.UpdateUser(user);

            ViewBag.IsSuccess = true;
            return View();
        }

        #endregion

        #region ADD USER FROM ADMIN PANEL

        public IActionResult AddUserFromAdmin(int id)
        {
            var user = _userService.GetUserById(id);

            string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعال سازی", body);

            return Redirect("/Admin/Users");
        }

        #endregion
    }
}
