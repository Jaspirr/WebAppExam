using WebAppExam.Models.Entities;
using WebAppExam.Models;

namespace WebAppExam.ViewModels
{
    public class IndexViewModel
    {
        public string? Title { get; set; }
        public UserProfileEntity UserProfile { get; set; } = null!;
        public UserModel User { get; set; } = null!;
    }
}
