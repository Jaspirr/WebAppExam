using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
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




    public async Task<bool> RegisterAsync(RegisterViewModel viewModel)
    {
        try
        {
            await _seedService.SeedRoles();
            var roleName = "user";
            //Om det inte finns några användare, lägg till roll till admin rollen annars user rollen.
            if (!await _userManager.Users.AnyAsync())
                roleName = "admin";

            //Skapa användare/registrerar  
            IdentityUser identityUser = new IdentityUser
            {
                UserName = viewModel.Email,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber
            };
            await _userManager.CreateAsync(identityUser, viewModel.Password);

            await _userManager.AddToRoleAsync(identityUser, roleName);

            //Skapa profil för användarprofil
            ProfileEntity profileEntity = new ProfileEntity
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
            };
            profileEntity.Id = identityUser.Id;

            //sparar ner till identitycontext
            _identityContext.UserProfiles.Add(profileEntity);
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
            userProfileEntity.Id = identityUser.Id;

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

            // Konvertera UserProfileEntity till ProfileEntity
            ProfileEntity profileEntity = new ProfileEntity
            {
                Id = userProfileEntity.Id,
                FirstName = userProfileEntity.FirstName,
                LastName = userProfileEntity.LastName,
                StreetName = userProfileEntity.Address.StreetName,
                PostalCode = userProfileEntity.Address.PostalCode,
                City = userProfileEntity.Address.City,
            };

            _identityContext.UserProfiles.Add(profileEntity);
            await _identityContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
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