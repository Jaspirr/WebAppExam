using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppExam.Models.Entities
{
    public class UserProfileEntity
    {
        [Key, ForeignKey("User")]
        public string UserId { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;


        //Optional
        public string? CompanyName { get; set; }
        public string? ProfileImage { get; set; }

        public int AddressId { get; set; }
        public AddressEntity Address { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;

    }
}
