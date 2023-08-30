using Core.Services.Interfaces;
using DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.ProductGroups
{
    public class IndexModel : PageModel
    {
        private IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<ProductGroup> ProductGroup { get; set; }
        public void OnGet()
        {
            ProductGroup = _productService.GetAllProductGroups();
        }
    }
}
