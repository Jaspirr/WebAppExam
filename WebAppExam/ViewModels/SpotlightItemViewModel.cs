namespace WebAppExam.ViewModels;

public class SpotlightItemViewModel
{
    public string Id { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public int CommentsTotal { get; set; } = 0;
}
