using CartolaApi.Data.Models;
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
            .HasMany(team => team.Players)
            .WithOne(player => player.PlayerTeam)
            .HasForeignKey(player => player.TeamId);
        
        builder.Entity<Tournament>()
            .HasMany(tournament => tournament.Teams)
            .WithOne(team => team.Tournament)
            .HasForeignKey(team => team.TournamentId);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }

}