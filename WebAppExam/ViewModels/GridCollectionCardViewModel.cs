using WebAppExam.Models;
using WebAppExam.Models.Entities;

namespace WebAppExam.ViewModels;

public class GridCollectionCardViewModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }


    public static implicit operator GridCollectionCardViewModel(ProductEntity entity)
    {
        return new GridCollectionCardViewModel
        {
            Id = entity.Id,
            ImageUrl = entity.LgImgUrl!,
            Title = entity.Name,
            Price = entity.Price
        };
    }
}
