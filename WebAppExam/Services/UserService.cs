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
        var userProfileEntity = await _identityContext.UserProfiles.Include(x => x.User).Include(x => x.Address).FirstOrDefaultAsync(x => x.UserId == userId);
        if (userProfileEntity != null)
        {
            UserProfileEntity convertedProfileEntity = new UserProfileEntity
            {
                UserId = userProfileEntity.UserId,
                FirstName = userProfileEntity.FirstName,
                LastName = userProfileEntity.LastName,
                // Andra egenskaper i UserProfileEntity
                Address = userProfileEntity.Address
            };

            return convertedProfileEntity;
        }

        return null;
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
            UserModel userModel = new UserModel
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                // ...

                // Du kanske behöver tilldela andra egenskaper här baserat på ditt User- och ProfileEntity-schema
            };

            var foundRole = roles.FirstOrDefault(x => x.Id == userModel.Id);
            userModel.Role = foundRole?.RoleName;

            userModels.Add(userModel);
        }

        return userModels; // Lägg till returinstruktionen här
    }
}