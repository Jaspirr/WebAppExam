namespace WebAppExam.Models.Entities;

public class ContactEntity
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? CompanyName { get; set; }
    public string Comment { get; set; } = null!;
}
