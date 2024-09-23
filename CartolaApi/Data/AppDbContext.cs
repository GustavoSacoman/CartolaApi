using CartolaApi.Data.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }))
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasKey(user => user.Id);
        builder.Entity<User>()
            .Property(user => user.Id)
            .ValueGeneratedOnAdd();
        builder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        builder.Entity<Team>()
            .HasKey(team => team.Id);
        builder.Entity<Team>()
            .Property(team => team.Id)
            .ValueGeneratedOnAdd();
        builder.Entity<Team>()
            .HasMany(team => team.Players)
            .WithOne(player => player.PlayerTeam)
            .HasForeignKey(player => player.TeamId);

        builder.Entity<Tournament>()
            .HasKey(tournament => tournament.Id);
        builder.Entity<Tournament>()
            .Property(tournament => tournament.Id)
            .ValueGeneratedOnAdd();
        builder.Entity<Tournament>()
            .HasMany(tournament => tournament.Teams)
            .WithOne(team => team.Tournament)
            .HasForeignKey(team => team.TournamentId);

        builder.Entity<Player>()
            .HasKey(player => player.Id);
        builder.Entity<Player>()
            .Property(player => player.Id)
            .ValueGeneratedOnAdd();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
}