using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebAppExam.Controllers
{  
    [Authorize(Roles = "admin")] // säkrar upp vem som kan komma in på sidan och inte.
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Admin - Dashboard";

            return View();
        }
    }
}
