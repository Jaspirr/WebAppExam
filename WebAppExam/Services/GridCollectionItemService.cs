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
            var cards = new List<GridCollectionItemViewModel>();
            var products = await _productService.GetAllAsync();

            foreach (var product in products)
            {
                GridCollectionItemViewModel card = product;

                cards.Add(card);
            }

            return cards;
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
            var cards = new List<GridCollectionItemViewModel>();
            var products = await _productService.GetProductsByCategoryIdAsync(predicate);

            foreach (var product in products)
            {
                GridCollectionItemViewModel card = product;

                cards.Add(card);
            }

            return cards;
        }
        catch
        {
            return null!;
        }
    }
}
