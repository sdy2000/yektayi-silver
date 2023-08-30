

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class ShowProductForAdminPanel
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }

        public string Status { get; set; }

        public int ProductPrice { get; set; }

        public string MainProductImage { get; set; }
    }
    public class CreateProductViewModel
    {
        [Display(Name = "جنسیت استفاده کننده")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int ProductGenderId { get; set; }

        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int GroupId { get; set; }


        public int? SubGroupId { get; set; }


        [Display(Name = "فروشنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int SellerId { get; set; }


        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int StatusId { get; set; }



        [Display(Name = "وزن محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Weight { get; set; }



        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string ProductTitle { get; set; }


        [Display(Name = "موجودی محصول")]
        [Required(ErrorMessage = "لطفا تعداد {0} را وارد نمایید.")]
        public int ProductStock { get; set; }


        [Display(Name = "توضیحات مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(70, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string ProductShortDescription { get; set; }


        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(1500, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string ProductDescription { get; set; }


        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int ProductPrice { get; set; }


        [Display(Name = "تک ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Tags { get; set; }


        [Display(Name = "تصویر اصلی")]
        public IFormFile? MainProductImage { get; set; }


        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
    public class EditProductViewModel
    {
        [Display(Name = "جنسیت استفاده کننده")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int ProductGenderId { get; set; }

        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int GroupId { get; set; }


        public int? SubGroupId { get; set; }


        [Display(Name = "فروشنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int SellerId { get; set; }


        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int StatusId { get; set; }



        [Display(Name = "وزن محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Weight { get; set; }



        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string ProductTitle { get; set; }


        [Display(Name = "موجودی محصول")]
        [Required(ErrorMessage = "لطفا تعداد {0} را وارد نمایید.")]
        public int ProductStock { get; set; }


        [Display(Name = "توضیحات مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(70, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string ProductShortDescription { get; set; }


        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(1500, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string ProductDescription { get; set; }


        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int ProductPrice { get; set; }


        [Display(Name = "تک ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Tags { get; set; }


        [Display(Name = "تصویر اصلی")]
        public IFormFile? MainProductImage { get; set; }


        // // // DONT SHOW IN PAGE
        public int ProductId { get; set; }
        public string OldMainProductImage { get; set; }
    }
    public class DeleteProductViewModel
    {
        public int ProductId { get; set; }

        public string MainProductImage { get; set; }

        public string Seller { get; set; }

        public string Status { get; set; }

        public int Weight { get; set; }

        public int ProductPrice { get; set; }

        public int ProductStock { get; set; }

        public string ProductTitle { get; set; }

        public string ProductShortDescription { get; set; }

        public string ProductDescription { get; set; }

        public string Tags { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
