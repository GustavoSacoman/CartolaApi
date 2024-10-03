using CartolaApi.Data.DTOs;
using CartolaApi.Router.v1.Models;
using Microsoft.EntityFrameworkCore;
using Team = CartolaApi.Router.v1.Models.Team;

namespace CartolaApi.Data.Services;

public class TournamentServices
{
    private readonly AppDbContext _db;

    public TournamentServices()
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
    
    public bool VerifyTournamentExistence(int? tournamentId, string? tournamentName)
{
    if (tournamentId.HasValue)
    {
        return _db.Tournaments.Any(t => t.Id == tournamentId.Value);
    }
    else if (!string.IsNullOrEmpty(tournamentName))
    {
        return _db.Tournaments.Any(t => t.TournamentName.Equals(tournamentName, StringComparison.OrdinalIgnoreCase));
    }

    return false; // Se nenhum dos critérios for fornecido
}

    
  public List<TournamentDTO> GetTournaments()
{
 
        return _db.Tournaments.ToList();
}

    
  public void CreateTournament(TournamentDTO tournamentDTO)
{
    try
    {
        // Verifica se o torneio já existe
        if (VerifyTournamentExistence(tournamentDTO.Id, tournamentDTO.TournamentName))
        {
            throw new Exception("Tournament already exists");
        }

        // Cria o torneio
        var tournament = new TournamentDTO
        {
            Id = tournamentDTO.Id ?? 0,
            TournamentName = tournamentDTO.TournamentName
        };

        _db.Tournaments.Add(tournamentDTO);
        _db.SaveChanges();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        throw; // Re-lance a exceção após logar
    }
}

    
    public void DeleteTournament(int tournamentId)
    {
        if (!VerifyTournamentExistence(tournamentId, null))
        {
            throw new Exception("Tournament not found");
        }

        var tournament = _db.Tournaments.FirstOrDefault(tournament => tournament.Id == tournamentId);
        _db.Tournaments.Remove(tournament);
        _db.SaveChanges();
    }
    
    public void UpdateTournament(int tournamentId, string newTournamentName)
    {
        if (!VerifyTournamentExistence(tournamentId, null))
        {
            throw new Exception("Tournament not found");
        }
        var tournament = _db.Tournaments.FirstOrDefault(t => t.Id == tournamentId);
        
        tournament.TournamentName = newTournamentName;
        _db.SaveChanges();
        
    }
  

    
}

