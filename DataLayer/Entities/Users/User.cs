﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entities.Wallets;
using DataLayer.Entities.Products;

namespace DataLayer.Entities.Users
{
	public class User
    {
        [Key]
        public int UserId { get; set; }


        [Display(Name = "جنسیت")]
        public int? GenderId { get; set; }


        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string UserName { get; set; }


        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? LastName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }


        [Display(Name = "شماره موبایل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? PhonNumber { get; set; }


        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Password { get; set; }


        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string UserAvatar { get; set; }


        [Display(Name = "کد فعال سازی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string ActiveCode { get; set; }


        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }


        [Display(Name = "حذف شده است؟")]
        public bool IsDelete { get; set; }


        [Display(Name = "قوانین و مقررات")]
        public bool Rulse { get; set; }


        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;











        #region RELATIONS

        [ForeignKey("GenderId")]
        public UserGender UserGender { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Wallet> Wallets { get; set; }
        public List<UserProduct> UserProducts { get; set; }

        #endregion
    }
}
