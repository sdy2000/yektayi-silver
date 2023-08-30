using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Core.DTOs
{
    public class UserForAdminViewModel
    {
        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
    public class CreateUserForAdminViewModel
    {
        [Display(Name = "جنسیت")]
        public int? GenderId { get; set; }


        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string Email { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        [MinLength(6, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد!")]
        public string Password { get; set; }


        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? LastName { get; set; }


        [Display(Name = "شماره موبایل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? PhonNumber { get; set; }


        [Display(Name = "وضعیت")]
        public int? IsActive { get; set; }


        public IFormFile? UserAvatar { get; set; }


        [Display(Name = "کیف پول")]
        public int? AddWallet { get; set; }

    }
    public class EditUserForAdminViewModel
    {
        public int UserId { get; set; }


        [Display(Name = "جنسیت")]
        public int? GenderId { get; set; }


        [Display(Name = "نام کاربری")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? UserName { get; set; }


        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? Email { get; set; }


        [Display(Name = "کلمه عبور")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        [MinLength(6, ErrorMessage = "{0} نمیتواند کمتر از {1} باشد!")]
        public string? Password { get; set; }


        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? LastName { get; set; }


        [Display(Name = "شماره موبایل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? PhonNumber { get; set; }


        [Display(Name = "وضعیت")]
        public int? IsActive { get; set; }


        public IFormFile? UserAvatar { get; set; }


        public string OldUserAvatarName { get; set; }


        [Display(Name = "کیف پول")]
        public int? AddWallet { get; set; }


        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }

        public List<int>? UserRoles { get; set; }

    }

    public class DeleteUserForAdminViewModel
    {
        public int UserId { get; set; }


        [Display(Name = "جنسیت")]
        public string? Gender { get; set; }


        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string Email { get; set; }


        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? LastName { get; set; }


        [Display(Name = "شماره موبایل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? PhonNumber { get; set; }


        public List<int>? UserRoles { get; set; }


        public string UserAvatar { get; set; }


        public DateTime RegisterDate { get; set; }

    }
}
