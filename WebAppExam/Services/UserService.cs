using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAppExam.Contexts;
using WebAppExam.Models;
using WebAppExam.Models.Entities;
using WebAppExam.ViewModels;

namespace WebAppExam.Services;

public class UserService
{
    private readonly IdentityContext _identityContext;
    private readonly RoleService _roleService;

    public UserService(IdentityContext identityContext, RoleService roleService)
    {
        _identityContext = identityContext;
        _roleService = roleService;
    }

    public async Task<UserProfileEntity> GetUserProfileAsync(string userId)
    {
        var profileEntity = await _identityContext.UserProfiles.FirstOrDefaultAsync(u => u.Id == userId);

        var userProfileEntity = new UserProfileEntity
        {
            Id = profileEntity.Id,
            FirstName = profileEntity.FirstName,
            LastName = profileEntity.LastName,
            StreetName = profileEntity.StreetName,
            PostalCode = profileEntity.PostalCode,
            City = profileEntity.City,
            User = profileEntity.User
        };

        return userProfileEntity;
    }

    public async Task<IdentityUser> GetIdentityUserAsync(string email)
    {
        var identityUser = await _identityContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        return identityUser!;
    }


    public async Task<IEnumerable<UserModel>> GetAllUserModelAsync()
    {
        var userModels = new List<UserModel>();
        var userProfileEntities = await _identityContext.UserProfiles.Include(x => x.User).ToListAsync();

        var roles = await _roleService.GetUserRolesAsync();

        foreach (var user in userProfileEntities)
        {

            UserModel userModel = user;

            var foundRole = roles.FirstOrDefault(x => x.Id == userModel.Id);

            userModel.Role = foundRole!.RoleName;

            userModels.Add(userModel);
        }

        return userModels!;
    }
}