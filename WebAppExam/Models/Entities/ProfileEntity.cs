using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppExam.Models.Entities;

public class ProfileEntity
{
    [Key, ForeignKey(nameof(User))]
    public string Id { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;


    [Column(TypeName = "nvarchar(50)")]
    public string? StreetName { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string? PostalCode{ get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string? City { get; set; }


    [Column(TypeName = "nvarchar(50)")]
    public string? CompanyName { get; set; }

    [Column(TypeName = "varchar(max)")]
    public string? ProfileImage { get; set; }

    public IdentityUser User { get; set; } = null!;
}
