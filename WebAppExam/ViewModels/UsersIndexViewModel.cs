using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebAppExam.Models;

namespace WebAppExam.ViewModels
{
    public class UsersIndexViewModel
    {
        public string? Title { get; set; }

        [Display(Name = "Id:")]
        public string UserId { get; set; } = null!;

        [Display(Name = "Current role:")]
        public string Role { get; set; } = null!;

        public IEnumerable<UserModel>? UserModels { get; set; }
        public IEnumerable<IdentityRole>? AllRoles { get; set; }
    }
}
