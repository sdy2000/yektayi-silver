using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin.Products
{
    public class CreateProductModel : PageModel
    {
        private IProductService _productService;
        public CreateProductModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public CreateProductViewModel CreateProduct { get; set; }
        public void OnGet()
        {
            List<SelectListItem> group = new List<SelectListItem>()
            {
                new SelectListItem(){Text="انتخاب کنید",Value="" }
            }; group.AddRange(_productService.GetProductGroup());
            ViewData["Groups"] = new SelectList(group, "Value", "Text");

            List<SelectListItem> subGroup = new List<SelectListItem>()
            {
                new SelectListItem(){Text="انتخاب کنید",Value="" }
            }; ViewData["SubGroups"] = new SelectList(subGroup, "Value", "Text");


            List<SelectListItem> seller = new List<SelectListItem>()
            {
                new SelectListItem(){Text="انتخاب کنید",Value="" }
            }; seller.AddRange(_productService.GetSeller());
            ViewData["Seller"] = new SelectList(seller, "Value", "Text");

        }

        public IActionResult OnPost(IFormFile ImgeUp)
        {
            #region VALIDATION

            if (!ModelState.IsValid)
            {
                List<SelectListItem> group = new List<SelectListItem>()
                {
                    new SelectListItem(){Text="انتخاب کنید",Value="" }
                }; group.AddRange(_productService.GetProductGroup());
                    ViewData["Groups"] = new SelectList(group, "Value", "Text");

                    List<SelectListItem> subGroup = new List<SelectListItem>()
                {
                    new SelectListItem(){Text="انتخاب کنید",Value="" }
                }; ViewData["SubGroups"] = new SelectList(subGroup, "Value", "Text");


                    List<SelectListItem> seller = new List<SelectListItem>()
                {
                    new SelectListItem(){Text="انتخاب کنید",Value="" }
                }; seller.AddRange(_productService.GetSeller());
                ViewData["Seller"] = new SelectList(seller, "Value", "Text");
                ViewData["IsSuccess"] = false;
                return Page();
            }

            #endregion

            int productId = _productService.AddProduct(CreateProduct, ImgeUp);


            return Redirect("/Admin/Products");
        }
    }
}
