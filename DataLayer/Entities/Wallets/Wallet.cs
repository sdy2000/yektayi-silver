using DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataLayer.Entities.Wallets
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }


        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int TypeId { get; set; }


        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int UserId { get; set; }


        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Amount { get; set; }


        [Display(Name = "توضاحات")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد!")]
        public string Description { get; set; }


        [Display(Name = "تایید شده")]
        public bool IsPay { get; set; }


        [Display(Name = "تاریخ")]
        public DateTime CreateDate { get; set; }





        #region RELATION

        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("TypeId")]
        public WalletType WalletType { get; set; }

        #endregion

    }
}
