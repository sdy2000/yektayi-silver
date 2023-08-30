using Core.DTOs;
using DataLayer.Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Services.Interfaces
{
    public interface IProductService
    {
        string SaveOrUpdateImg(IFormFile newImg, string oldImg = "Defult.png");



        #region PRODUCT FOR ADMIN

        List<ShowProductForAdminPanel> GetAllProductForShowAdminPanel();
        Product GetProductById(int productId);
        int AddProduct(CreateProductViewModel product, IFormFile imageUp);
        int UpdateProduct(Product product);
        EditProductViewModel GetProductForEdit(int productId);
        int EditProductFromAdmin(EditProductViewModel product, IFormFile imageUp);
        DeleteProductViewModel GetProductForShowInDelete(int productId);
        int DeleteProductFromAdmin(int productId);
        List<SelectListItem> GetProductGroup();
        List<SelectListItem> GetProductSubGroup(int parentId);
        List<SelectListItem> GetSeller();

        #endregion

        #region PRODUCT GROUPS

        List<ProductGroup> GetAllProductGroups();
        ProductGroup GetProductGroupById(int groupId);
        int AddProductGroup(ProductGroup productGroup);
        int UpdateProductGroup(ProductGroup productGroup);
        void DeleteProductGroup(int groupId);

        #endregion
    }
}
