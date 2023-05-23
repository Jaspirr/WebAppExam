using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAppExam.Models;
using WebAppExam.Services;
using WebAppExam.ViewModels;

namespace WebAppExam.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        private readonly CheckboxOptionService _checkBoxOptionService;
        private readonly GridCollectionCardService _gridCollectionCardService;

        public ProductsController(ProductService productService, CheckboxOptionService checkBoxOptionService, GridCollectionCardService gridCollectionCardService)
        {
            _productService = productService;
            _checkBoxOptionService = checkBoxOptionService;
            _gridCollectionCardService = gridCollectionCardService;
        }

        public async Task<IActionResult> Index()
        {

            var viewModel = new ProductsIndexViewModel
            {
                Title = "Products",
                All = new GridCollectionViewModel
                {
                    Title = "All Products",
                    GridItems = await _gridCollectionCardService.PopulateCardsWithAllProductsAsync(),

                    LoadMore = false
                }
            };

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) { return RedirectToAction("index", "home"); }

            var viewModel = new ProductDetailsViewModel
            {
                Title = "Product Details",
                ShopSub = new SubModel
                {
                    Heading = "SHOP",
                    Subheading = "HOME. PRODUCT DETAILS",
                    BackgroundImg = "/images/header.jpg"
                },
                Same = new SameProductsViewModel
                {
                    //HARDCODED PRODUCTS
                    GridCards = new List<GridCollectionCardViewModel>
                    {
                        new GridCollectionCardViewModel{ Id = 1, Title = "Apple watch series", Price = 10, ImageUrl = "https://images.pexels.com/photos/7897470/pexels-photo-7897470.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                        new GridCollectionCardViewModel{ Id = 2, Title = "Apple watch series", Price = 20, ImageUrl = "https://images.pexels.com/photos/1667071/pexels-photo-1667071.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                        new GridCollectionCardViewModel{ Id = 3, Title = "Apple watch series", Price = 30, ImageUrl = "https://images.pexels.com/photos/37539/colored-pencils-colour-pencils-mirroring-color-37539.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                        new GridCollectionCardViewModel{ Id = 4, Title = "Apple watch series", Price = 40, ImageUrl = "https://images.pexels.com/photos/90946/pexels-photo-90946.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    }
                },
                Test = id
            };

            if (await _productService.GetAsync(x => x.Id == id) == null) { return RedirectToAction("index", "home"); }

            viewModel.Product = await _productService.GetAsync(x => x.Id == id);

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

        //----PRODUCT LIST----
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> List()
        {
            var viewModel = new ProductListViewModel
            {
                Title = "Product List",
                Products = await _productService.GetAllAsync()
            };

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(ProductListViewModel viewModel)
        {
            viewModel.Products = await _productService.GetAllAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    if (await _productService.RemoveAsync(viewModel.ProductId))
                        return RedirectToAction("list", "products");
                    else
                        ModelState.AddModelError("", "An error occurred while removing the product, and it could not be removed.");
                }
                catch
                {
                    ModelState.AddModelError("", "An error occurred while removing the product, and it could not be removed");
                }

            }

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }


        //----REGISTER PRODUCT----
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Register()
        {

            var viewModel = new ProductRegisterViewModel
            {
                Title = "Register Product",
                Checkboxes = await _checkBoxOptionService.PopulateCategoryCheckBoxesAsync()
            };

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Register(ProductRegisterViewModel viewModel)
        {

            viewModel.Title = "Register Product";

            if (ModelState.IsValid)
            {
                try
                {
                    if (await _productService.RegisterAsync(viewModel))
                        return RedirectToAction("register", "products");
                    else
                        ModelState.AddModelError("", "Something went wrong while creating the product.");
                }
                catch
                {
                    ModelState.AddModelError("", "Something went wrong while creating the product.");
                }

            }

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}
