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
            var roleName = "user";

            if (!await _userManager.Users.AnyAsync())
                roleName = "admin";

            IdentityUser identityUser = viewModel;
            await _userManager.CreateAsync(identityUser, viewModel.Password);

            await _userManager.AddToRoleAsync(identityUser, roleName);

            UserProfileEntity userProfileEntity = viewModel;
            userProfileEntity.UserId = identityUser.Id;

            AddressEntity addressEntity = viewModel;

            var addressInDb = await _identityContext.Addresses.FirstOrDefaultAsync(x => x.StreetName == viewModel.StreetName);
            if (addressInDb != null)
            {
                userProfileEntity.AddressId = addressInDb.Id;
            }
            else
            {
                _identityContext.Addresses.Add(addressEntity);
                await _identityContext.SaveChangesAsync();
                userProfileEntity.AddressId = addressEntity.Id;
            }

            _identityContext.UserProfiles.Add(userProfileEntity);
            await _identityContext.SaveChangesAsync();

            return true;
        }
        catch { return false; }
    }

    public async Task<bool> RegisterAsync(UserRegisterViewModel viewModel)
    {
        try
        {
            IdentityUser identityUser = viewModel;
            await _userManager.CreateAsync(identityUser, viewModel.Password);

            if (viewModel.Role != null)
                await _userManager.AddToRoleAsync(identityUser, viewModel.Role);

            UserProfileEntity userProfileEntity = viewModel;
            userProfileEntity.UserId = identityUser.Id;

            AddressEntity addressEntity = viewModel;

            var addressInDb = await _identityContext.Addresses.FirstOrDefaultAsync(x => x.StreetName == viewModel.StreetName);
            if (addressInDb != null)
            {
                userProfileEntity.AddressId = addressInDb.Id;
            }
            else
            {
                _identityContext.Addresses.Add(addressEntity);
                await _identityContext.SaveChangesAsync();
                userProfileEntity.AddressId = addressEntity.Id;
            }

            _identityContext.UserProfiles.Add(userProfileEntity);
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