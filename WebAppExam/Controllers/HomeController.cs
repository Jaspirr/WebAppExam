using Microsoft.AspNetCore.Mvc;
using WebAppExam.Services;
using WebAppExam.ViewModels;

namespace WebAppExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly GridCollectionCardService _gridCollectionCardService;

        public HomeController(GridCollectionCardService gridCollectionCardService)
        {
            _gridCollectionCardService = gridCollectionCardService;
        }
        public async Task <IActionResult> Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                BestCollection = new GridCollectionViewModel
                {
                    Title = "Best Collection",
                    Categories = new List<string> { "All", "Bags", "Dress", "Decoration", "Essentials", "Interior", "Laptop", "Mobile", "Beauty" },
                    GridItems = await _gridCollectionCardService.PopulateCardsByCategoryIdAsync(x => x.CategoryId == 3), //CategoryId = 3 => "featured"
                    LoadMore = true
                },
                NewCollection = new GridCollectionViewModel
                {
                    Title = "New arrivals",
                    GridItems = await _gridCollectionCardService.PopulateCardsByCategoryIdAsync(x => x.CategoryId == 1), //CategoryId = 1 => "new"
                    LoadMore = false
                },
                TopSellingCollection = new GridCollectionViewModel
                {
                    Title = "Top selling products in this week",
                    GridItems = await _gridCollectionCardService.PopulateCardsByCategoryIdAsync(x => x.CategoryId == 2), //CategoryId = 2 => "popular"
                    LoadMore = true
                },
                PlantSpotlight = new SpotlightViewModel
                {
                    //HARDCODED SPOTLIGHTS
                    SpotlightItem = new List<SpotlightItemViewModel>
                    {
                        new SpotlightItemViewModel{ Id="1", Title = "PLACEHOLDER", UserName = "Admin", CommentsTotal = 13 , Description = "Best dress for everyone ed totam velit risus viverra nobis donec recusandae perspiciatis incididuno", ImageUrl="./images/wall-lamp.jpg"},
                        new SpotlightItemViewModel{ Id="2", Title = "PLACEHOLDER", UserName = "HurrDurr", CommentsTotal = 14 , Description = "Best dress for everyone ed totam velit risus viverra nobis donec recusandae perspiciatis incididuno", ImageUrl="./images/wall-lamp.jpg"},
                        new SpotlightItemViewModel{ Id="3", Title = "PLACEHOLDER", UserName = "KurrFnurr", CommentsTotal = 15 , Description = "Best dress for everyone ed totam velit risus viverra nobis donec recusandae perspiciatis incididuno", ImageUrl="./images/wall-lamp.jpg"}
                    }
                }
            };

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}
