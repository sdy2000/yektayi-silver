using Core.Services.Interfaces;
using DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.ProductGroups
{
    public class CreateGroupModel : PageModel
    {
        private IProductService _productService;
        public CreateGroupModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductGroup ProductGroups { get; set; }
        public void OnGet(int id)
        {
            ProductGroups = new ProductGroup()
            {
                ParentId = id
            };

        }

        public IActionResult OnPost()
        {
            if (ProductGroups.GroupTitle == null)
            {
                ModelState.AddModelError("ProductGroups.GroupTitle", "لطفا عنوان گروه را وارد کنید !");
                return Page();
            }
            if (ProductGroups.ParentId == 0)
            {
                ProductGroups.ParentId = null;
            }
            ProductGroups.IsDelete = false;
            _productService.AddProductGroup(ProductGroups);


            return Redirect("/Admin/ProductGroups");
        }
    }
}
