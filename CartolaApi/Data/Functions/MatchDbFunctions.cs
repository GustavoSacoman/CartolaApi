using Microsoft.EntityFrameworkCore;
using CartolaApi.Data.DTOs;

namespace CartolaApi.Data.Functions;

public class MatchDbFunctions
{
    private readonly AppDbContext _db;

    public MatchDbFunctions()
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
    
    public List<Match> GetMatches()
    {
        return _db.Matches.ToList();
    }
    
    public void CreateMatch(Match match)
    {
        _db.Matches.Add(match);
        _db.SaveChanges();
    }
    
    public Match? GetMatch(int matchId)
    {
        return _db.Matches.FirstOrDefault(m => m.IdMatch == matchId);
    }
    public void UpdateMatch(Match match, int matchId)
    {
        var matchToUpdate = _db.Matches.FirstOrDefault(m => m.IdMatch == matchId);
        if (matchToUpdate == null)
        {
            throw new Exception("Match not found");
        }
        matchToUpdate.Date = match.Date;
        matchToUpdate.Result = match.Result;
        matchToUpdate.IdTeam1 = match.IdTeam1;
        matchToUpdate.IdTeam2 = match.IdTeam2;
        matchToUpdate.IdTournament = match.IdTournament;
        _db.Matches.Update(matchToUpdate);
        _db.SaveChanges();
    }
    
    public void DeleteMatch(int matchId)
    {
        var matchToDelete = _db.Matches.FirstOrDefault(m => m.IdMatch == matchId);
        if (matchToDelete == null)
        {
            throw new Exception("Match not found");
        }
        _db.Matches.Remove(matchToDelete);
        _db.SaveChanges();
    }
    
    
}   