using Microsoft.AspNetCore.Mvc;
using WebAppExam.Services;
using WebAppExam.ViewModels;

namespace WebAppExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly GridCollectionItemService _gridCollectionItemService;

        public HomeController(GridCollectionItemService gridCollectionItemService)
        {
            _gridCollectionItemService = gridCollectionItemService;
        }
        public async Task <IActionResult> Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                BestCollection = new GridCollectionViewModel
                {
                    Title = "Best Collection",
                    Categories = new List<string> { "All", "Vegetables", "Trees", "Berries", "Peppers", "Greens", "Other" },
                    GridItems = await _gridCollectionItemService.PopulateItemsByCategoryIdAsync(x => x.CategoryId == 3), 
                    LoadMore = true
                },
                NewCollection = new GridCollectionViewModel
                {
                    Title = "New arrivals",
                    GridItems = await _gridCollectionItemService.PopulateItemsByCategoryIdAsync(x => x.CategoryId == 1),
                    LoadMore = false
                },
                TopSellingCollection = new GridCollectionViewModel
                {
                    Title = "Top selling products in this week",
                    GridItems = await _gridCollectionItemService.PopulateItemsByCategoryIdAsync(x => x.CategoryId == 2),
                    LoadMore = true
                },
                PlantSpotlight = new SpotlightViewModel
                {
                    //HARDCODED SPOTLIGHTS
                    SpotlightItem = new List<SpotlightItemViewModel>
                    {
                        new SpotlightItemViewModel{ Id="1", Title = "Feedback", UserName = "Sara", CommentsTotal = 2 , Description = "My favorite potatoes, everyone ed totam velit risus viverra nobis donec recusandae perspiciatis incididuno", ImageUrl="./images/products/färskpotatis.jpg"},
                        new SpotlightItemViewModel{ Id="2", Title = "Feedback:", UserName = "Sofie", CommentsTotal = 5 , Description = "Best on the market ed totam velit risus viverra nobis donec recusandae perspiciatis incididuno", ImageUrl="./images/products/spenat.jpg"},
                        new SpotlightItemViewModel{ Id="3", Title = "Feedback:", UserName = "Peter", CommentsTotal = 11 , Description = "So good ed totam velit risus viverra nobis donec recusandae perspiciatis incididuno", ImageUrl="./images/products/smalcucumber.jpg"}
                    }
                }
            };

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}
