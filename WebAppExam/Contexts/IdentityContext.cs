using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppExam.Models.Entities;

namespace WebAppExam.Contexts;

public class IdentityContext : IdentityDbContext<IdentityUser>
{
	public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
	{
	}

	public DbSet<UserProfileEntity> UserProfiles { get; set; }
	public DbSet<AddressEntity> Addresses { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "admin", NormalizedName = "ADMIN" });
		builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "user", NormalizedName = "USER" });
        builder.Entity<AddressEntity>().HasIndex(x => x.StreetName).IsUnique();
    }
}
