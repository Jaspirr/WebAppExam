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
        {
            var claimIdentity = await base.GenerateClaimsAsync(user);
            var profileEntity = await _userService.GetUserProfileAsync(user.Id);

            claimIdentity.AddClaim(new Claim("DisplayName", $"{profileEntity.FirstName} {profileEntity.LastName}"));

            var roles = await UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claimIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return claimIdentity;
        }
    }
}