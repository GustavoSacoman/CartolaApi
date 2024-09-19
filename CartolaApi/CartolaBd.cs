using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {

        string con = "server=localhost;port=3306;database=cartola;user=root;password=";

        builder.UseMySQL(con).LogTo(Console.WriteLine, LogLevel.Information);

    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }

}