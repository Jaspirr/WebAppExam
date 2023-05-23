using Microsoft.AspNetCore.Mvc;

namespace WebAppExam.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Cart";
            return View();
        }
    }
}
