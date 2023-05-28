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


        [Display(Name = "Large Product Image (optional)")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageLg { get; set; }

        [Display(Name = "Small Product Image (optional)")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageSm { get; set; }

        public List<CheckboxOptionModel> Checkboxes { get; set; } = new();

        public List<int> CheckboxCategoryId { get; set; } = new();

        public static implicit operator ProductEntity(ProductRegisterViewModel viewModel)
        {
            var productEntity = new ProductEntity
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price
            };

            if (viewModel.ImageLg != null)
            {
                productEntity.LgImgUrl = $"{Guid.NewGuid()}_{viewModel.ImageLg.FileName}";
            }

            if (viewModel.ImageSm != null)
            {
                productEntity.SmImgUrl = $"{Guid.NewGuid()}_{viewModel.ImageSm.FileName}";
            }

            return productEntity;
        }

    }
}
