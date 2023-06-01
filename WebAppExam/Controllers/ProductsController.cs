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
        private readonly GridCollectionItemService _gridCollectionItemService;

        public ProductsController(ProductService productService, CheckboxOptionService checkBoxOptionService, GridCollectionItemService gridCollectionItemService)
        {
            _productService = productService;
            _checkBoxOptionService = checkBoxOptionService;
            _gridCollectionItemService = gridCollectionItemService;
        }

        public async Task<IActionResult> Index()
        {

            var viewModel = new ProductsIndexViewModel
            {
                Title = "Products",
                All = new GridCollectionViewModel
                {
                    Title = "All Products",
                    GridItems = await _gridCollectionItemService.PopulateItemsWithAllProductsAsync(),

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
                    GridItems = new List<GridCollectionItemViewModel>
                    {
                        new GridCollectionItemViewModel{ Id = 1, Title = "PLACEHOLDER", Price = 10, ImageUrl = "product.jpg" },
                        new GridCollectionItemViewModel{ Id = 2, Title = "PLACEHOLDER", Price = 20, ImageUrl = "product.jpg" },
                        new GridCollectionItemViewModel{ Id = 3, Title = "PLACEHOLDER", Price = 30, ImageUrl = "product.jpg" },
                        new GridCollectionItemViewModel{ Id = 4, Title = "PLACEHOLDER", Price = 40, ImageUrl = "product.jpg" },
                    }
                },
            };

            if (await _productService.GetAsync(x => x.Id == id) == null) { return RedirectToAction("index", "home"); }

            viewModel.Product = await _productService.GetAsync(x => x.Id == id);

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }


        //PRODUCT LIST
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
