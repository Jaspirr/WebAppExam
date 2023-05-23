using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppExam.Services;
using WebAppExam.ViewModels;

namespace WebAppExam.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AuthService _auth;
        private readonly UserService _userService;
        private readonly RoleService _roleService;


        public AccountController(UserService userService, AuthService auth, SignInManager<IdentityUser> signInManager, RoleService roleService)
        {
            _userService = userService;
            _auth = auth;
            _signInManager = signInManager;
            _roleService = roleService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var _identityUser = await _userService.GetIdentityUserAsync(User!.Identity!.Name!);

            var viewModel = new IndexViewModel
            {
                Title = "My Account",
                UserProfile = await _userService.GetUserProfileAsync(_identityUser.Id),
                User = await _userService.GetIdentityUserAsync(User!.Identity!.Name!)
            };

            var roleModel = await _roleService.GetSpecificUserRolesAsync(_identityUser.Id);

            //This also makes the first letter in roleModel.RoleName to be capitalized:
            viewModel.User.Role = char.ToUpper(roleModel.RoleName[0]) + roleModel.RoleName.Substring(1);

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
        // REGISTER
        public IActionResult Register()
        {
            ViewData["Title"] = "Register Account";

            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("index", "account");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            ViewData["Title"] = "Register Account";

            if (ModelState.IsValid)
            {
                if (await _auth.RegisterAsync(viewModel))
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "A user with the same email already exists");
                }
            }
            return View(viewModel);

        }

        // LOGIN
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            ViewData["Title"] = "Login";

            if (ModelState.IsValid)
            {
                if (await _auth.LoginAsync(viewModel))
                    return RedirectToAction("Index", "Account");

                ModelState.AddModelError("", "Incorrect Email or Password");
            }
            
            return View(viewModel);
        }

        // är vi inte inloggade spå kommer vi ej åt Sign out.
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (await _auth.LogoutAsync(User))
            {
                return RedirectToAction("Index", "home");
            }

            return RedirectToAction("login", "Account");
        }
    }
}
