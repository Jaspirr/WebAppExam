using Microsoft.AspNetCore.Identity;
using WebAppExam.Models.Entities;

namespace WebAppExam.Models
{
    public class UserModel
    {
        public string Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? ProfileImage { get; set; }
        public string Role { get; set; } = null!;
    
        public static implicit operator UserModel(UserProfileEntity entity)
        {
            return new UserModel
            {
                Id = entity.UserId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.User.Email!,
                ProfileImage = entity.ProfileImage,
                PhoneNumber = entity.User.PhoneNumber,
                
            };
        }

        public static implicit operator UserModel(IdentityUser entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                Email = entity.Email!,
                PhoneNumber = entity.PhoneNumber
            };
        }
    }
}