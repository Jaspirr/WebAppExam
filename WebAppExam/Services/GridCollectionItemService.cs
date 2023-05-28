using System.Linq.Expressions;
using WebAppExam.Models.Entities;
using WebAppExam.ViewModels;

namespace WebAppExam.Services;

public class GridCollectionItemService
{
    private readonly ProductService _productService;

    public GridCollectionItemService(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<IEnumerable<GridCollectionItemViewModel>> PopulateItemsWithAllProductsAsync()
    {
        try
        {
            var items = new List<GridCollectionItemViewModel>();
            var products = await _productService.GetAllAsync();

            foreach (var product in products)
            {
                GridCollectionItemViewModel item = product;

                items.Add(item);
            }

            return items;
        }
        catch
        {
            return null!;
        }
    }

    public async Task<IEnumerable<GridCollectionItemViewModel>> PopulateItemsByCategoryIdAsync(Expression<Func<ProductCategoryEntity, bool>> predicate)
    {
        try
        {
            var items = new List<GridCollectionItemViewModel>();
            var products = await _productService.GetProductsByCategoryIdAsync(predicate);

            foreach (var product in products)
            {
                GridCollectionItemViewModel item = product;

                items.Add(item);
            }

            return items;
        }
        catch
        {
            return null!;
        }
    }
}
