using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppExam.Contexts;
using WebAppExam.Models;

namespace WebAppExam.Services;

public class RoleService
{
    private readonly IdentityContext _identityContext;
    private readonly UserManager<IdentityUser> _userManager;

    public RoleService(IdentityContext identityContext, UserManager<IdentityUser> userManager)
    {
        _identityContext = identityContext;
        _userManager = userManager;
    }

    public async Task<IEnumerable<IdentityRole>> GetRolesAsync()
    {
        var roles = await _identityContext.Roles.ToListAsync();

        return roles!;
    }

    public async Task<IEnumerable<UserRoleModel>> GetUserRolesAsync()
    {
        var userRoleModels = new List<UserRoleModel>();
        var roles = await _identityContext.Roles.ToListAsync();
        var usersRoles = await _identityContext.UserRoles.ToListAsync();


        foreach (var user in usersRoles)
        {
            var userAdd = new UserRoleModel
            {
                Id = user.UserId,
                RoleName = user.RoleId
            };

            var foundRole = roles.FirstOrDefault(x => x.Id == userAdd.RoleName);

            userAdd.RoleName = foundRole!.Name!;

            userRoleModels.Add(userAdd);
        }

        return userRoleModels!;
    }
    public async Task<UserRoleModel> GetSpecificUserRolesAsync(string userId)
    {
        var userRoleModels = await GetUserRolesAsync();

        var specificUserRole = userRoleModels.FirstOrDefault(x => x.Id == userId);

        return specificUserRole!;
    }

    public async Task<bool> ChangeRoleAsync(string userId, string roleChange)
    {
        try
        {
            var userEntity = await _identityContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var currentRoles = await _userManager.GetRolesAsync(userEntity!);

            await _userManager.RemoveFromRolesAsync(userEntity!, currentRoles);

            await _userManager.AddToRoleAsync(userEntity!, roleChange);
            await _identityContext.SaveChangesAsync();

            return true;

        }
        catch
        {
            return false;
        }
    }
}
