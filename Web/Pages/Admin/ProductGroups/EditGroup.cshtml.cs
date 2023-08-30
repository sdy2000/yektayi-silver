using Core.Services.Interfaces;
using DataLayer.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.ProductGroups
{
    public class EditGroupModel : PageModel
    {
        private IProductService _productService;
        public EditGroupModel(IProductService productService)
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
            if (ProductGroups.GroupTitle == null)
            {
                ModelState.AddModelError("ProductGroups.GroupTitle", "لطفا عنوان گروه را وارد کنید !");
                return Page();
            }

            _productService.UpdateProductGroup(ProductGroups);


            return Redirect("/Admin/ProductGroups");
        }
    }
}
