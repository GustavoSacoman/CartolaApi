using Microsoft.EntityFrameworkCore;

namespace CartolaApi
{
    
public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Setup DI
        var serviceProvider = new ServiceCollection()
            .AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
            .BuildServiceProvider();
    }

    public DbSet<Player> Players { get; set; }

}

}
