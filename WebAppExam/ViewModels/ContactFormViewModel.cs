using System.ComponentModel.DataAnnotations;
using WebAppExam.Models.Entities;

namespace WebAppExam.ViewModels;

public class ContactFormViewModel
{
    [Display(Name = "Your Name*")]
    [Required(ErrorMessage = "Please enter your name.")]
    [RegularExpression(@"^[a-öA-Ö]+(?:[ é'-][a-öA-Ö]+)*$", ErrorMessage = "You need to enter a valid First Name.")]
    public string FullName { get; set; } = null!;

    [Display(Name = "E-mail*")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Please enter your e-mail.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "You need to enter a valid E-mail.")]
    public string Email { get; set; } = null!;

    [Display(Name = "Phone Number*")]
    [Required(ErrorMessage = "Please enter your phone number.")]
    public string PhoneNumber { get; set; } = null!;

    [Display(Name = "Company (optional)")]
    public string? CompanyName { get; set; }

    [Display(Name = "Write something*")]
    [Required(ErrorMessage = "Please enter a comment.")]
    public string Comment { get; set; } = null!;

    //public bool SaveInBrowser { get; set; } = false;


    public static implicit operator ContactEntity(ContactFormViewModel viewModel)
    {
        return new ContactEntity
        {
            FullName = viewModel.FullName,
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber,
            CompanyName = viewModel.CompanyName,
            Comment = viewModel.Comment
        };
    }

    public static implicit operator ContactFormViewModel(ContactEntity entity)
    {
        return new ContactFormViewModel
        {
            FullName = entity.FullName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            CompanyName = entity.CompanyName,
            Comment = entity.Comment
        };
    }
}
