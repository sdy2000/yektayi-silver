
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Products
{
    public class ProductStatus
    {
        [Key]
        public int StatusId { get; set; }


        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string StatusTitel { get; set; }



        #region RELATION

        public List<Product> Products { get; set; }

        #endregion
    }
}
