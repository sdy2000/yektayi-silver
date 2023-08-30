
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Products
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }


        [Required]
        public int ProductId { get; set; }


        [Display(Name = "عنوان تصویر")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string? ImageTitle { get; set; }


        [Display(Name = "نام تصویر")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string ImageName { get; set; }




        #region RELATION

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        #endregion
    }
}
