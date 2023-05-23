using Microsoft.EntityFrameworkCore;
using WebAppExam.Models.Entities;

namespace WebAppExam.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ProfileEntity> Profiles { get; set; }

    public DbSet<ContactEntity> Contacts { get; set; }

   
}
