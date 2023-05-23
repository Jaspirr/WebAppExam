using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebAppExam.Services;

namespace WebAppExam.Factories;

public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser>
{
    private readonly UserService _userService;

    public CustomClaimsPrincipalFactory(UserManager<IdentityUser> userManager, IOptions<IdentityOptions> optionsAccessor, UserService userService) : base(userManager, optionsAccessor)
    {
        _userService = userService;
    }

    //Add custom claims:
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
    {
        var claimsIdentity = await base.GenerateClaimsAsync(user);

        try
        {
            var userProfileEntity = await _userService.GetUserProfileAsync(user.Id);

            if (userProfileEntity != null)
            {
                // Add a combined string of "FirstName LastName"
                claimsIdentity.AddClaim(new Claim("DisplayName", $"{userProfileEntity.FirstName} {userProfileEntity.LastName}"));

                // Add user roles
                var roles = await UserManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                return claimsIdentity;
            }
            return null!;
        }
        catch { return null!; }
    }
}