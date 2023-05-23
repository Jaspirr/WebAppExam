using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppExam.Contexts;
using WebAppExam.Models;
using WebAppExam.Models.Entities;

namespace WebAppExam.Services
{
	public class UserProfileService
	{
		private readonly IdentityContext _identityContext;

		public UserProfileService(IdentityContext identityContext)
		{
			_identityContext = identityContext;
		}

        public async Task<UserProfileEntity> GetUserProfileAsync(string userId)
        {
            var userProfileEntity = await _identityContext.UserProfiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);
            return userProfileEntity!;
        }

        public async Task<IdentityUser> GetIdentityUserAsync(string email)
		{
			var identityUser = await _identityContext.Users.FirstOrDefaultAsync(x => x.Email == email);

			return identityUser!;
		}

		public async Task<IEnumerable<UserProfileEntity>> GetAllUserProfileAsync()
		{
			var userProfiles = new List<UserProfileEntity>();
			var userProfileEntity = await _identityContext.UserProfiles.ToListAsync();

			foreach (var profile in userProfileEntity)
			{
				userProfiles.Add(profile);
			}

			return userProfiles!;
		}

        public async Task<IEnumerable<UserRoleModel>> GetRolesAsync()
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


        public async Task<IEnumerable<UserModel>> GetAllUserModelAsync()
		{
			var userModels = new List<UserModel>();
			var userProfileEntities = await _identityContext.UserProfiles.Include(x => x.User).ToListAsync();

			var roles = await GetRolesAsync();

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
}