using System.ComponentModel.DataAnnotations;


namespace DataLayer.Entities.Products
{
    public class ProductGender
    {
        [Key]
        public int ProductGenderId { get; set; }


        [Display(Name = "gender")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string ProductGenderTitle { get; set; }




        #region RELATION

        List<Product> Products { get; set; }

        #endregion
    }
}
