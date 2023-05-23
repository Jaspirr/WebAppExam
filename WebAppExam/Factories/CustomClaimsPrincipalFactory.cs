using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebAppExam.Services;

namespace WebAppExam.Factories;

public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser>
{
	private readonly UserProfileService _userProfileService;

	public CustomClaimsPrincipalFactory(UserManager<IdentityUser> userManager, IOptions<IdentityOptions> optionsAccessor, UserProfileService userProfileService) : base(userManager, optionsAccessor)
	{
		_userProfileService = userProfileService;
	}

	protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
	{
		var claimsIdentity = await base.GenerateClaimsAsync(user);

		//Add custom claims
		var userProfileEntity = await _userProfileService.GetUserProfileAsync(user.Id);
		claimsIdentity.AddClaim(new Claim("DisplayName", $"{userProfileEntity.FirstName} {userProfileEntity.LastName}"));

		// Add user roles to claims
		var roles = await UserManager.GetRolesAsync(user);
		foreach (var role in roles)
		{
			claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
		}

		return claimsIdentity;
	}
}