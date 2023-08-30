using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private IUserService _userService;

        public WalletController(IUserService userService)
        {
            _userService = userService;
        }


        [Route("Wallet")]
        public IActionResult Index()
        {
            ViewBag.ListWallet = _userService.GetUserWallet(User.Identity.Name);
            return View();
        }

        [HttpPost]
        [Route("Wallet")]
        public IActionResult Index(ChargeWalletViewModel chargeWallet)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListWallet = _userService.GetUserWallet(User.Identity.Name);
                return View(chargeWallet);
            }
            int walletId = _userService.ChargeWallet(User.Identity.Name, chargeWallet.Amount, "واریز به حساب");

            #region ONLINE PAYMENT

            var pament = new ZarinpalSandbox.Payment(chargeWallet.Amount);
            var res = pament.PaymentRequest("شارژ کیف پول", "https://localhost:7128/OnlinePayment/" + walletId, "sajjad.darvish.yektayi@gmail.com", "09370776595");
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }

            #endregion

            ViewBag.ListWallet = _userService.GetUserWallet(User.Identity.Name);
            return View();
        }
    }
}
