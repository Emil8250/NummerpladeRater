using Microsoft.EntityFrameworkCore;
using NummerpladeRater.Models;

namespace NummerpladeRater.Data;

public class LicensePlateDbContext : DbContext
{
    public LicensePlateDbContext(DbContextOptions<LicensePlateDbContext> options)
        : base(options)
    {
    }

    public DbSet<LicensePlate> LicensePlate { get; set; }
    public DbSet<Upvote> Upvotes { get; set; }
    public DbSet<Downvote> Downvotes { get; set; }
}