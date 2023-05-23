using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAppExam.Contexts;
using WebAppExam.Models.Entities;
using WebAppExam.ViewModels;

namespace WebAppExam.Services;

public class AuthService
{

    private readonly UserManager <IdentityUser> _userManager;
    private readonly IdentityContext _identityContext;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SeedService _seedService;


    public AuthService(UserManager<IdentityUser> userManager, IdentityContext identityContext, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, SeedService seedService)
    {
        _userManager = userManager;
        _identityContext = identityContext;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _seedService = seedService;
    }




    public async Task<bool> RegisterAsync(AccountRegisterViewModel viewModel)
    {
        try
        {
            await _seedService.SeedRoles();
            var roleName = "user";
            //Om det inte finns några användare, lägg till roll till admin rollen annars user rollen.
            if (!await _userManager.Users.AnyAsync())
                roleName = "admin";

            //Skapa användare/registrerar  
            IdentityUser identityUser = viewModel;
            await _userManager.CreateAsync(identityUser, viewModel.Password);

            await _userManager.AddToRoleAsync(identityUser, roleName);

            //Skapa profil för användarprofil
            ProfileEntity userprofileEntity = viewModel;
            userprofileEntity.Id = identityUser.Id;

            //sparar ner till identitycontext
            _identityContext.UserProfiles.Add(userprofileEntity);
            await _identityContext.SaveChangesAsync();

            return true;

        }
        catch { return false; }

    }

    public async Task<bool> LoginAsync(LoginViewModel viewModel)
    {
        try
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.KeepLoggedIn, false);

            return result.Succeeded;
        }
        catch { return false; }
    }

    public async Task<bool> LogoutAsync(ClaimsPrincipal user)
    {
        await _signInManager.SignOutAsync();

        return _signInManager.IsSignedIn(user);
    }
}
