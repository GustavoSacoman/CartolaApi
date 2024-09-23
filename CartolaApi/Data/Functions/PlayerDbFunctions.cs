using Microsoft.EntityFrameworkCore;

using CartolaApi.Data.Models;
namespace CartolaApi.Data.Functions;

public class PlayerDbFunctions
{
    private readonly AppDbContext _db;
    
    public PlayerDbFunctions()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("CartolaConnection");
        if (connectionString == null)
        {
            throw new Exception("Database connection string not found");
        }
        
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        _db = new AppDbContext(optionsBuilder.Options);

    }
    
    public bool VerifyPlayerExistence(int? playerId, string? playerName)
    {
        Player? player = null;

        if (playerId != null && playerName != null)
        {
            player = _db.Players.FirstOrDefault(p => p.Id == playerId);
        }
        else if (playerName != null)
        {
            player = _db.Players.FirstOrDefault(p => p.NamePlayer == playerName);
        }

        return player != null;
    }
    
    public List<Player> GetPlayers()
    {
        return _db.Players.ToList();
    }
    
    public void CreatePlayer(Player player)
    {
        if (VerifyPlayerExistence(null, player.NamePlayer))
        {
            throw new Exception("Player already exists");
        }

        _db.Players.Add(player);
        _db.SaveChanges();
    }
    
    public Player? GetPlayer(int? playerId, string? playerName)
    {
        if (!VerifyPlayerExistence(playerId ?? null, playerName ?? null))
        {
            throw new Exception("Player not found");
        }
        if (playerId == null)
        {
            return _db.Players.FirstOrDefault(player => player.NamePlayer == playerName);
        }
        return _db.Players.FirstOrDefault(player => player.Id == playerId);
    }
    
    public void DeletePlayer(int playerId)
    {
        if (!VerifyPlayerExistence(playerId, null))
        {
            throw new Exception("Player not found");
        }
        var player = _db.Players.FirstOrDefault(p => p.Id == playerId);
        _db.Players.Remove(player);
        _db.SaveChanges();
    }
    
    public void UpdatePlayer(int playerId ,string? playerName, string? position)
    {
        if (!VerifyPlayerExistence(playerId, null))
        {
            throw new Exception("Player not found");
        }
        var player = _db.Players.FirstOrDefault(p => p.Id == playerId);
        if (playerName != null)
        {
            player.NamePlayer = playerName;
        }
        if (position != null)
        {
            player.Position = position;
        }
        _db.SaveChanges();
    }
    
}