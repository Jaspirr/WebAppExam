using WebAppExam.Models.Entities;

namespace WebAppExam.ViewModels;

public class GridCollectionItemViewModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }

    public static implicit operator GridCollectionItemViewModel(ProductEntity entity)
    {
        return new GridCollectionItemViewModel
        {
            Id = entity.Id,
            ImageUrl = entity.LgImgUrl!,
            Title = entity.Name,
            Price = entity.Price
        };
    }
}
