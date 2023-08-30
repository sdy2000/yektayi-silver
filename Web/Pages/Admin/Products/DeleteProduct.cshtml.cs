using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Products
{
    public class DeleteProductModel : PageModel
    {
        private IProductService _productService;
        public DeleteProductModel(IProductService productService)
        {
            _productService = productService;
        }


        [BindProperty]
        public DeleteProductViewModel DeleteProduct { get; set; }
        public void OnGet(int id)
        {
            DeleteProduct = _productService.GetProductForShowInDelete(id);
        }

        public IActionResult OnPost()
        {
            _productService.DeleteProductFromAdmin(DeleteProduct.ProductId);

            return Redirect("/Admin/Products");
        }
    }
}
