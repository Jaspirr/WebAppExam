using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebAppExam.Models.Entities;

namespace WebAppExam.ViewModels
{
    public class RegisterViewModel
    {
        
        [Display(Name = "First Name*")]
        [Required(ErrorMessage = "Please enter your First Name.")]
        [RegularExpression(@"^[a-öA-Ö]+(?:[ é'-][a-öA-Ö]+)*$", ErrorMessage = "You need to enter a valid First Name.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name*")]
        [Required(ErrorMessage = "Please enter your Last Name.")]
        [RegularExpression(@"^[a-öA-Ö]+(?:[ é'-][a-öA-Ö]+)*$", ErrorMessage = "You need to enter a valid Last Name.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Email*")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter your Email.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "You need to enter a valid E-mail.")]
        public string Email { get; set; } = null!;

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a Password.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "You need to enter a valid Password.")]
        public string Password { get; set; } = null!;

        [Display(Name = "Confirm Password*")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please confirm the Password.")]
        [Compare(nameof(Password), ErrorMessage = "The Passwords don't match.")]
        public string ConfirmPassword { get; set; } = null!;


        [Display(Name = "Street Name")]
        public string? StreetName { get; set; }

        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "Mobile (optional)")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Company (optional)")]
        public string? CompanyName { get; set; }

        [Display(Name = "Upload Profile Image (optional)")]
        public string? ProfileImage { get; set; }

        //Standard Identity
        public static implicit operator IdentityUser(RegisterViewModel viewModel)
        {
            return new IdentityUser
            {
                UserName = viewModel.Email,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
            };
        }

        public static implicit operator UserProfileEntity(RegisterViewModel viewModel)
        {
            return new UserProfileEntity
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                CompanyName = viewModel.CompanyName,
                ProfileImage = viewModel.ProfileImage,
            };
        }

        public static implicit operator AddressEntity(RegisterViewModel viewModel)
        {
            return new AddressEntity
            {
                StreetName = viewModel.StreetName,
                PostalCode = viewModel.PostalCode,
                City = viewModel.City
            };
        }
    }
}


