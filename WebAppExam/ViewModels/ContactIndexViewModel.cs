using WebAppExam.Models;

namespace WebAppExam.ViewModels;

public class ContactIndexViewModel
{
    public string? Title { get; set; }
    public SubModel ContactSub { get; set; } = null!;
    public ContactFormViewModel ContactForm { get; set; } = null!;
}
