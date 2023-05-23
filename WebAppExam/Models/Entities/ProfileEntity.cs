using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppExam.Models.Entities;

public class ProfileEntity
{
    [Key, ForeignKey("User")]
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string? StreetName { get; set; } 
    public string? PostalCode { get; set; } 
    public string? City { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CompanyName { get; set; }
    public string? ProfileImage { get; set; }

    public UserEntity User { get; set; } = null!;
}
