using System.ComponentModel.DataAnnotations;

namespace WebAppExam.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Please enter your Email")]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Please enter your Password")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Keep me logged in")]
    public bool KeepLoggedIn { get; set; } = false;

}
