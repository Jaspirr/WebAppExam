using WebAppExam.Models;

namespace WebAppExam.Services;

public class ShowcaseService
{
    private readonly List<ShowcaseModel> _showcases = new()
    {
        new ShowcaseModel()
        {
            Ingress = "WELCOME TO BMERKETO SHOP",
            Title = "Exclusive Plants Collection.",
            ImageUrl = "images/products/showcasevegie.jpg",
            Button = new LinkButtonModel
            {
                Content = "SHOP NOW",
                Url = "/products",
            }
        },
        new ShowcaseModel()
        {
            Ingress = "BMERKETO THE BEST A PERSON CAN GET",
            Title = "Best on the market",
            ImageUrl = "images/products/showcasevegie.jpg",
            Button = new LinkButtonModel
            {
                Content = "SHOP NOW",
                Url = "/products",
            }
        }
    };


    public ShowcaseModel GetLatest()
    {
        return _showcases.LastOrDefault()!;
    }

}
