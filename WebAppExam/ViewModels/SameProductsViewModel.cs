namespace WebAppExam.ViewModels;

public class SameProductsViewModel
{
    public string Heading { get; set; } = "Related Products";
    public IEnumerable<GridCollectionCardViewModel> GridCards { get; set; } = null!;
}
