using Core.Convertors;
using Core.DTOs;
using Core.Generators;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        private YektayiSilverContext _context;

        public ProductService(YektayiSilverContext context)
        {
            _context = context;
        }

        public string SaveOrUpdateImg(IFormFile newImg, string oldImg = "Defult.png")
        {
            if (newImg != null)
            {
                if (newImg.IsImage())
                {
                    string imgPath = "";
                    string thumbPath = "";
                    if (oldImg != "Defult.png")
                    {
                        imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/image", oldImg);
                        if (File.Exists(imgPath))
                            File.Delete(imgPath);
                        thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/thumb", oldImg);
                        if (File.Exists(thumbPath))
                            File.Delete(thumbPath);

                    }

                    string newImgName = NameGenerator.GeneratorUniqCode() + Path.GetExtension(newImg.FileName);
                    imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/image", newImgName);
                    using (var stream = new FileStream(imgPath, FileMode.Create))
                    {
                        newImg.CopyTo(stream);
                    }

                    #region RESIZE IMAGE

                    ImageConvertor imgResize = new ImageConvertor();
                    thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/thumb", newImgName);

                    imgResize.Image_resize(imgPath, thumbPath, 170);

                    #endregion

                    return newImgName;
                }
                else
                {
                    return "Defult.png";
                }
            }
            else if (oldImg != "Defult.png")
            {
                return oldImg;
            }
            else
            {
                return "Defult.png";
            }
        }



        // // // // // // // // // PRODUCT FOR ADMIN

        public List<ShowProductForAdminPanel> GetAllProductForShowAdminPanel()
        {
            return _context.Products
                .Include(p => p.ProductStatus)
                .Select(p => new ShowProductForAdminPanel()
                {
                    ProductId=p.ProductId,
                    MainProductImage = p.MainProductImage,
                    ProductPrice = p.ProductPrice,
                    Status = p.ProductStatus.StatusTitel,
                    ProductTitle = p.ProductTitle
                }).ToList();
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.SingleOrDefault(p => p.ProductId == productId);
        }

        public int AddProduct(CreateProductViewModel product, IFormFile imageUp)
        {
            Product pro = new Product()
            {
                SellerId = product.SellerId,
                StatusId = product.StatusId,
                ProductGenderId=product.ProductGenderId,
                ProductTitle = product.ProductTitle,
                ProductStock = product.ProductStock,
                ProductShortDescription = product.ProductShortDescription,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                Weight=product.Weight,
                Tags = product.Tags,
                MainProductImage = SaveOrUpdateImg(imageUp),
                IsDelete = false,
                CreateDate = DateTime.Now,
                GroupId = product.GroupId,
                SubGroupId = product.SubGroupId
            };

            _context.Products.Add(pro);
            _context.SaveChanges();


            return pro.ProductId;
        }
        
        public int UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();

            return product.ProductId;
        }

        public EditProductViewModel GetProductForEdit(int productId)
        {
            return _context.Products
                .Where(p => p.ProductId == productId)
                .Select(p => new EditProductViewModel()
                {
                    ProductId = p.ProductId,
                    ProductTitle = p.ProductTitle,
                    GroupId = p.GroupId,
                    SubGroupId = p.SubGroupId,
                    SellerId = p.SellerId,
                    ProductGenderId = p.ProductGenderId,
                    ProductShortDescription = p.ProductShortDescription,
                    ProductDescription = p.ProductDescription,
                    OldMainProductImage = p.MainProductImage,
                    ProductPrice = p.ProductPrice,
                    Weight = p.Weight,
                    ProductStock = p.ProductStock,
                    Tags = p.Tags,
                    StatusId = p.StatusId
                }).Single();
        }

        public int EditProductFromAdmin(EditProductViewModel product, IFormFile imageUp)
        {
            Product pro = new Product()
            {
                ProductId=product.ProductId,
                SellerId = product.SellerId,
                StatusId = product.StatusId,
                ProductGenderId = product.ProductGenderId,
                ProductTitle = product.ProductTitle,
                ProductStock = product.ProductStock,
                ProductShortDescription = product.ProductShortDescription,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                Weight = product.Weight,
                Tags = product.Tags,
                MainProductImage = SaveOrUpdateImg(imageUp, product.OldMainProductImage),
                IsDelete = false,
                UpdateDate = DateTime.Now,
                GroupId = product.GroupId,
                SubGroupId = product.SubGroupId
            };


            return UpdateProduct(pro);
        }

        public DeleteProductViewModel GetProductForShowInDelete(int productId)
        {
            return _context.Products
                .Where(p => p.ProductId == productId)
                .Include(p => p.ProductStatus)
                .Select(p => new DeleteProductViewModel()
                {
                    ProductId = p.ProductId,
                    Status = p.ProductStatus.StatusTitel,
                    Seller = _context.Users.SingleOrDefault(u=>u.UserId==p.SellerId).UserName,
                    MainProductImage = p.MainProductImage,
                    ProductTitle = p.ProductTitle,
                    ProductShortDescription = p.ProductShortDescription,
                    ProductDescription = p.ProductDescription,
                    Tags = p.Tags,
                    Weight = p.Weight,
                    ProductPrice = p.ProductPrice,
                    ProductStock = p.ProductStock,
                    CreateDate=p.CreateDate
                }).Single();
        }

        public int DeleteProductFromAdmin(int productId)
        {
            Product product = GetProductById(productId);
            product.IsDelete = true;

            return UpdateProduct(product);
        }

        public List<SelectListItem> GetProductGroup()
        {
            return _context.ProductGroups
                .Where(pg => pg.ParentId == null)
                .Select(pg => new SelectListItem()
                {
                    Text = pg.GroupTitle,
                    Value = pg.GroupId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetProductSubGroup(int parentId)
        {
            return _context.ProductGroups
                .Where(pg => pg.ParentId == parentId)
                .Select(pg => new SelectListItem()
                {
                    Text = pg.GroupTitle,
                    Value = pg.GroupId.ToString()
                }).ToList();
        }

        public List<SelectListItem> GetSeller()
        {
            return _context.UserRoles
                .Where(ur => ur.RoleId == 3)
                .Include(ur => ur.User)
                .Select(ur => new SelectListItem()
                {
                    Text = ur.User.UserName,
                    Value = ur.User.UserId.ToString()
                }).ToList();
        }


        // // // // // // // // // PRODUCT GROUPS


        public List<ProductGroup> GetAllProductGroups()
        {
            return _context.ProductGroups.ToList();
        }

        public ProductGroup GetProductGroupById(int groupId)
        {
            return _context.ProductGroups.Find(groupId);
        }

        public int AddProductGroup(ProductGroup productGroup)
        {
            _context.ProductGroups.Add(productGroup);
            _context.SaveChanges();

            return productGroup.GroupId;
        }

        public int UpdateProductGroup(ProductGroup productGroup)
        {
            _context.ProductGroups.Update(productGroup);
            _context.SaveChanges();

            return productGroup.GroupId;
        }

        public void DeleteProductGroup(int groupId)
        {
            ProductGroup group = GetProductGroupById(groupId);
            group.IsDelete = true;

            UpdateProductGroup(group);
        }
    }
}
