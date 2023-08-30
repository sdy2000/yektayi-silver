
using DataLayer.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Products
{
    public class UserProduct
    {
        [Key]
        public int UP_Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }




        #region RELATION

        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        #endregion
    }
}
