
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Products
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }


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


        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string ProductTitle { get; set; }



        [Display(Name = "وزن محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Weight { get; set; }


        [Display(Name = "توضیحات مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(70, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string ProductShortDescription { get; set; }


        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(1500, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string ProductDescription { get; set; }


        [Display(Name = "موجودی محصول")]
        [Required(ErrorMessage = "لطفا تعداد {0} را وارد نمایید.")]
        public int ProductStock { get; set; }


        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int ProductPrice { get; set; }


        [Display(Name = "تک ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Tags { get; set; }


        [Display(Name = "تصویر اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string MainProductImage { get; set; }


        [Display(Name = "حذف شده")]
        public bool IsDelete { get; set; }


        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public DateTime CreateDate { get; set; } = DateTime.Now;


        [Display(Name = "تاریخ بروز رسانی")]
        public DateTime? UpdateDate { get; set; }





        #region RELATION

        [ForeignKey("StatusId")]
        public ProductStatus ProductStatus { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<UserProduct> UserProducts { get; set; }

        [ForeignKey("GroupId")]
        public ProductGroup Group { get; set; }

        [ForeignKey("SubGroupId")]
        public ProductGroup SubGroup { get; set; }

        [ForeignKey("ProductGenderId")]
        public ProductGender ProductGender { get; set; }

        #endregion
    }
}
