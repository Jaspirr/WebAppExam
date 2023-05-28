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
            ImageUrl = "images/placeholder/625x647.svg",
            Button = new LinkButtonModel
            {
                Content = "SHOP NOW",
                Url = "/products",
            }
        },
        new ShowcaseModel()
        {
            Ingress = "BMERKETO THE BEST A PERSON CAN GET",
            Title = "Always fresh",
            ImageUrl = "images/placeholder/625x647.svg",
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
