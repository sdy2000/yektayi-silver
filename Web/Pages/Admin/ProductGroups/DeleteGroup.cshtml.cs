using Core.Services.Interfaces;
using DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.ProductGroups
{
    public class DeleteGroupModel : PageModel
    {
        private IProductService _productService;
        public DeleteGroupModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductGroup ProductGroups { get; set; }
        public void OnGet(int id)
        {
            ProductGroups = _productService.GetProductGroupById(id);

        }

        public IActionResult OnPost()
        {
            _productService.DeleteProductGroup(ProductGroups.GroupId);


            return Redirect("/Admin/ProductGroups");
        }
    }
}
