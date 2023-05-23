using WebAppExam.Models;

namespace WebAppExam.ViewModels;

public class ProductDetailsViewModel
{
    public string? Title { get; set; } = "Product";
    public SubModel ShopSub { get; set; } = null!;
    public SameProductsViewModel Same { get; set; } = null!;

    public ProductModel Product { get; set; } = null!;

    public int Test { get; set; }
}
