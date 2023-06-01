using WebAppExam.Models.Entities;
using WebAppExam.Models;
using WebAppExam.ViewModels;
using System.Linq.Expressions;
using WebAppExam.Contexts;
using Microsoft.EntityFrameworkCore;

namespace WebAppExam.Services
{
    public class ProductService
    {
        private readonly ProductContext _productContext;
        private readonly CategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(ProductContext context, CategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productContext = context;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> RegisterAsync(ProductRegisterViewModel viewModel)
        {
            try
            {
                ProductEntity productEntity = viewModel;

                _productContext.Products.Add(productEntity);
                await _productContext.SaveChangesAsync();

                if (viewModel.CheckboxCategoryId.Any())
                {
                    foreach (var categoryId in viewModel.CheckboxCategoryId)
                    {
                        var currentCategory = await _productContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);

                        var productCategoryEntity = new ProductCategoryEntity
                        {
                            ProductId = productEntity.Id,
                            CategoryId = currentCategory!.Id
                        };

                        _productContext.ProductsCategories.Add(productCategoryEntity);
                    }
                }

                await _productContext.SaveChangesAsync();

                //Upload Image:
                if (await UploadImageAsync(productEntity, viewModel.ImageLg!, viewModel.ImageSm!) != true)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            var products = new List<ProductModel>();
            var categoriesEntities = new List<CategoryEntity>();
            var items = await _productContext.Products.ToListAsync();
            var productCategories = await _productContext.ProductsCategories.Include(x => x.Category).ToListAsync();

            foreach (var product in items)
            {
                ProductModel productModel = product;

                productModel.Categories = await _categoryService.GetProductCategoriesAsync(product);

                products.Add(productModel);
            }

            return products;
        }

        public async Task<bool> RemoveAsync(int productId)
        {
            try
            {
                var poductEntity = await _productContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

                if (poductEntity != null)
                {
                    _productContext.Products.Remove(poductEntity);
                    await _productContext.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductModel> GetAsync(Expression<Func<ProductEntity, bool>> predicate)
        {
            try
            {
                var productEntity = await _productContext.Products.FirstAsync(predicate);

                ProductModel product = productEntity;

                var categories = await _categoryService.GetProductCategoriesAsync(product);

                product.Categories = categories;

                return product!;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<List<ProductModel>> GetProductsByCategoryIdAsync(Expression<Func<ProductCategoryEntity, bool>> predicate)
        {
            var products = new List<ProductModel>();

            var productCategories = await _productContext.ProductsCategories.Include(x => x.Product).Where(predicate).ToListAsync();

            foreach (var category in productCategories)
            {
                products.Add(category.Product);
            }

            return products;
        }

        public async Task<bool> UploadImageAsync(ProductEntity product, IFormFile imageLg, IFormFile imageSm)
        {
            try
            {
                if (product.LgImgUrl != null)
                {
                    string imagePath = $"{_webHostEnvironment.WebRootPath}/images/products/{product.LgImgUrl}";
                    await imageLg.CopyToAsync(new FileStream(imagePath, FileMode.Create));
                }

                if (product.SmImgUrl != null)
                {
                    string imagePath = $"{_webHostEnvironment.WebRootPath}/images/products/{product.SmImgUrl}";
                    await imageSm.CopyToAsync(new FileStream(imagePath, FileMode.Create));
                }

                return true;
            }
            catch { return false; }
        }
    }
}