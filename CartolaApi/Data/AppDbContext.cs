using CartolaApi.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using Match = CartolaApi.Data.DTOs.Match;

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
            .HasIndex(team => team.Id)
            .IsUnique();

        builder.Entity<TournamentDTO>()
            .HasKey(tournament => tournament.Id);
        builder.Entity<TournamentDTO>()
            .Property(tournament => tournament.Id)
            .ValueGeneratedOnAdd();

        builder.Entity<Player>()
            .HasKey(player => player.Id);
        builder.Entity<Player>()
            .Property(player => player.Id)
            .ValueGeneratedOnAdd();
        
        builder.Entity<Match>()
            .HasKey(match => match.IdMatch);
        builder.Entity<Match>()
            .Property(match => match.IdMatch)
            .ValueGeneratedOnAdd();

        builder.Entity<Season>()
            .HasKey(season => season.Id);
        builder.Entity<Season>()
            .Property(season => season.Id)
            .ValueGeneratedOnAdd();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TournamentDTO> Tournaments { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Season> Seasons { get; set; }
}