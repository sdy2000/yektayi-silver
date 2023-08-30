using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Products
{
    public class ProductGroup
    {
        [Key]
        public int GroupId { get; set; }


        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string GroupTitle { get; set; }


        [Display(Name = "عنوان زیر گروه")]
        public int? ParentId { get; set; }


        [Display(Name = "حذف شده")]
        public bool IsDelete { get; set; }



        #region RELATEION

        [ForeignKey("ParentId")]
        public List<ProductGroup> ProductGroups { get; set; }

        [InverseProperty("Group")]
        public List<Product> Groups { get; set; }

        [InverseProperty("SubGroup")]
        public List<Product> SubGroups { get; set; }

        #endregion
    }
}
