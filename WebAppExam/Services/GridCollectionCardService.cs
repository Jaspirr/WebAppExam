using System.Linq.Expressions;
using WebAppExam.Models.Entities;
using WebAppExam.ViewModels;

namespace WebAppExam.Services;

public class GridCollectionCardService
{
    private readonly ProductService _productService;

    public GridCollectionCardService(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<IEnumerable<GridCollectionCardViewModel>> PopulateCardsWithAllProductsAsync()
    {
        try
        {
            var cards = new List<GridCollectionCardViewModel>();
            var products = await _productService.GetAllAsync();

            foreach (var product in products)
            {
                GridCollectionCardViewModel card = product;

                cards.Add(card);
            }

            return cards;
        }
        catch
        {
            return null!;
        }
    }

    public async Task<IEnumerable<GridCollectionCardViewModel>> PopulateCardsByCategoryIdAsync(Expression<Func<ProductCategoryEntity, bool>> predicate)
    {
        try
        {
            var cards = new List<GridCollectionCardViewModel>();
            var products = await _productService.GetProductsByCategoryIdAsync(predicate);

            foreach (var product in products)
            {
                GridCollectionCardViewModel card = product;

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
