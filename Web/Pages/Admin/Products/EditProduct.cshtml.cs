using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin.Products
{
    public class EditProductModel : PageModel
    {
        private IProductService _productService;
        public EditProductModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public EditProductViewModel EditProduct { get; set; }
        public void OnGet(int id)
        {
            EditProduct = _productService.GetProductForEdit(id);

            #region INPUT VALUES


            List<SelectListItem> group = new List<SelectListItem>()
            {
                new SelectListItem(){Text="انتخاب کنید",Value="" }
            }; group.AddRange(_productService.GetProductGroup());
            ViewData["Groups"] = new SelectList(group, "Value", "Text", EditProduct.GroupId);

            List<SelectListItem> subGroup = new List<SelectListItem>()
            {
                new SelectListItem(){Text="انتخاب کنید",Value="" }
            }; subGroup.AddRange(_productService.GetProductSubGroup(EditProduct.GroupId));
            ViewData["SubGroups"] = new SelectList(subGroup, "Value", "Text", EditProduct.SubGroupId ?? 0);


            List<SelectListItem> seller = new List<SelectListItem>()
            {
                new SelectListItem(){Text="انتخاب کنید",Value="" }
            }; seller.AddRange(_productService.GetSeller());
            ViewData["Seller"] = new SelectList(seller, "Value", "Text", EditProduct.SellerId);

            #endregion

        }

        public IActionResult OnPost(IFormFile ImgeUp)
        {
            #region VALIDATION

            //if (!ModelState.IsValid)
            //{
            //    #region INPUT VALUES

            //    List<SelectListItem> group = new List<SelectListItem>()
            //    {
            //        new SelectListItem(){Text="انتخاب کنید",Value="" }
            //    }; group.AddRange(_productService.GetProductGroup());
            //        ViewData["Groups"] = new SelectList(group, "Value", "Text", EditProduct.GroupId);

            //        List<SelectListItem> subGroup = new List<SelectListItem>()
            //    {
            //        new SelectListItem(){Text="انتخاب کنید",Value="" }
            //    }; subGroup.AddRange(_productService.GetProductSubGroup(EditProduct.GroupId));
            //        ViewData["SubGroups"] = new SelectList(subGroup, "Value", "Text", EditProduct.SubGroupId ?? 0);


            //        List<SelectListItem> seller = new List<SelectListItem>()
            //    {
            //        new SelectListItem(){Text="انتخاب کنید",Value="" }
            //    }; seller.AddRange(_productService.GetSeller());
            //    ViewData["Seller"] = new SelectList(seller, "Value", "Text", EditProduct.SellerId);

            //    #endregion

            //    ViewData["IsSuccess"] = false;
            //    return Page();
            //}

            #endregion

            _productService.EditProductFromAdmin(EditProduct, ImgeUp);

            return Redirect("/Admin/Products");
        }
    }
}
