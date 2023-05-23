using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebAppExam.Models.Entities;

namespace WebAppExam.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Display(Name = "First Name*")]
        [Required(ErrorMessage = "Please enter your first name.")]
        [RegularExpression(@"^[a-öA-Ö]+(?:[ é'-][a-öA-Ö]+)*$", ErrorMessage = "You need to enter a valid first name.")]
        public string FirstName { get; set; } = null!;


        [Display(Name = "Last Name*")]
        [Required(ErrorMessage = "Please enter your last name.")]
        [RegularExpression(@"^[a-öA-Ö]+(?:[ é'-][a-öA-Ö]+)*$", ErrorMessage = "You need to enter a valid last name.")]
        public string LastName { get; set; } = null!;


        [Display(Name = "Street Name*")]
        [Required(ErrorMessage = "Please enter your street name.")]
        public string StreetName { get; set; } = null!;


        [Display(Name = "Postal Code*")]
        [Required(ErrorMessage = "Please enter your postal code.")]
        public string PostalCode { get; set; } = null!;


        [Display(Name = "City*")]
        [Required(ErrorMessage = "Please enter your city.")]
        public string City { get; set; } = null!;

        [Display(Name = "Mobile (optional)")]
        [RegularExpression(@"/^(?=.{7})\+?\d[\d\s-]*\d$/", ErrorMessage = "Please enter a valid phone number or no phone number at all.")]
        public string? PhoneNumber { get; set; }


        [Display(Name = "Company (optional)")]
        public string? CompanyName { get; set; }


        [Display(Name = "E-mail*")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter your e-mail.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "You need to enter a valid e-mail.")]
        public string Email { get; set; } = null!;


        [Display(Name = "Password*")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a password.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "You need to enter a valid password.")]
        public string Password { get; set; } = null!;


        [Display(Name = "Confirm Password*")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please confirm the password.")]
        [Compare(nameof(Password), ErrorMessage = "The passwords don't match.")]
        public string ConfirmPassword { get; set; } = null!;


        [Display(Name = "Profile Image Url (optional)")]
        public string? ProfileImage { get; set; }

        [Required(ErrorMessage = "Please Accept the Terms.")]
        public bool AcceptsTerms { get; set; } = false;


        #region implicit operators

        public static implicit operator IdentityUser(AccountRegisterViewModel viewModel)
        {
            return new IdentityUser
            {
                UserName = viewModel.Email,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
            };
        }

        public static implicit operator UserProfileEntity(AccountRegisterViewModel viewModel)
        {
            return new UserProfileEntity
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                CompanyName = viewModel.CompanyName,
                ProfileImage = viewModel.ProfileImage,
            };
        }

        public static implicit operator AddressEntity(AccountRegisterViewModel viewModel)
        {
            return new AddressEntity
            {
                StreetName = viewModel.StreetName,
                PostalCode = viewModel.PostalCode,
                City = viewModel.City
            };
        }

        #endregion
    }
}
