using CartolaApi.Models;
using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();   
        
        builder.Entity<Team>()
            .HasIndex(team => team.Id)
            .IsUnique();

        
        builder.Entity<Tournament>()
            .HasIndex(tornament => tornament.Id)
            .IsUnique();
        
        builder.Entity<Match>()
            .HasOne<Tournament>()
            .WithMany()
            .HasForeignKey(m => m.IdTournament);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Match> Matches { get; set; }
    
}