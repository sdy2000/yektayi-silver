using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Products
{
    [PermissionChecker(13)]
    public class IndexModel : PageModel
    {
        private IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<ShowProductForAdminPanel> ShowProduct { get; set; }
        public void OnGet()
        {
            ShowProduct = _productService.GetAllProductForShowAdminPanel();
        }
    }
}
