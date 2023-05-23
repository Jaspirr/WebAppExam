namespace WebAppExam.Models;

public class CheckboxOptionModel
{
    public bool IsChecked { get; set; } = false;
    public string Description { get; set; } = null!;
    public string Value { get; set; } = null!;
}
