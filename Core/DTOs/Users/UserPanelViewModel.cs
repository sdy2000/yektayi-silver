using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class InformationUserViewModel
    {
        public string FirstAndLastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UesrGender { get; set; }
        public DateTime RegisterDate { get; set; }
        public string PhonNumber { get; set; }
        public int Wallet { get; set; }
    }
    public class SideBarUserPanelViewModel
    {
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public DateTime RegisterDate { get; set; }
    }

    public class EditProfileViewModel
    {
        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? Email { get; set; }

        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? LastName { get; set; }


        [Display(Name = "شماره موبایل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? PhonNumber { get; set; }



        [Display(Name = "جنسیت")]
        public int? GenderId { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string UserAvatar { get; set; }


        public IFormFile? UserImage { get; set; }
    }
    public class ChengePasswordViewModel
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string OldPassword { get; set; }



        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        [MinLength(6, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد!")]
        public string Password { get; set; }


        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند!")]
        [Display(Name = "تکرار کلمه عبور جدبد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string RePassword { get; set; }
    }
}
