using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrackerEF.Models;

namespace TrackerEF.Data;

public class TrackerContext : DbContext
{
    public TrackerContext() { }

    public TrackerContext(DbContextOptions<TrackerContext> options)
    : base(options)
    { }

    public DbSet<AssetTracker> Trackers { get; set; } = null!;
    public DbSet<Asset> Assets { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Computer> Computers { get; set; } = null!;
    public DbSet<Smartphone> Smartphones { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration config = new ConfigurationBuilder()
                                .SetBasePath(Environment.CurrentDirectory)
                                .AddJsonFile("appsettings.json")
                                .Build();

        optionsBuilder.UseSqlServer(config.GetConnectionString("AssetsDB"));
    }
}
