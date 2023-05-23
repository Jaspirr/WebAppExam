using System.ComponentModel.DataAnnotations;
using WebAppExam.Models;
using WebAppExam.Models.Entities;

namespace WebAppExam.ViewModels
{
    public class ProductRegisterViewModel
    {
        public string? Title { get; set; }

        [Display(Name = "Product Name*")]
        [Required(ErrorMessage = "Please enter the Product Name.")]
        public string Name { get; set; } = null!;


        [Display(Name = "Product Description*")]
        [Required(ErrorMessage = "Please enter the Product Description.")]
        public string Description { get; set; } = null!;


        [Display(Name = "Product Price*")]
        [Required(ErrorMessage = "Please enter the Product Price.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; } = 0;


        [Display(Name = "Product Image (501 x 430px)")]
        public string? LgImgUrl { get; set; }


        [Display(Name = "Product Image (120 x 113px)")]
        public string? SmImgUrl { get; set; }


        public List<CheckboxOptionModel> Checkboxes { get; set; } = new();

        public List<int> CheckboxCategoryId { get; set; } = new();

        public static implicit operator ProductEntity(ProductRegisterViewModel productRegisterViewModel)
        {
            return new ProductEntity
            {
                Name = productRegisterViewModel.Name,
                Description = productRegisterViewModel.Description,
                Price = productRegisterViewModel.Price,
                LgImgUrl = productRegisterViewModel.LgImgUrl,
                SmImgUrl = productRegisterViewModel.SmImgUrl
            };
        }
    }
}
