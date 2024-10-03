using CartolaApi.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CartolaApi.Data.Services;

public class TeamServices
{
    private readonly AppDbContext _db;

    public TeamServices()
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
    public bool VerifyTeamExistence(int? teamId, string? teamName)
    {
        Team? team = null;
        
        if (teamId != null)
        {
        
            team = _db.Teams.FirstOrDefault(t => t.Id == teamId);
        }
        else if (teamName != null)
        {
        
            team = _db.Teams.FirstOrDefault(t => t.Name == teamName);
        }
        
        return team != null;
    }
    
    
    public Team GetTeam(int? id, string? name)
    {
        if (!VerifyTeamExistence(id ?? null, name ?? null))
        {
            throw new Exception("Team not found");
        }
        if (id == null)
        {
            return _db.Teams.FirstOrDefault(team => team.Name == name);
        }
        return _db.Teams.FirstOrDefault(team => team.Id == id);
    }
    public List<Team> GetTeams()
    {
        return _db.Teams.ToList();
    }
    
    public void CreateTeam(Team team)
    {
        if (VerifyTeamExistence(null, team.Name))
        {
            throw new Exception("Team already exists");
        }

        _db.Teams.Add(team);
        _db.SaveChanges();
    }
    
    public void DeleteTeam(string teamName)
    {
        if (!VerifyTeamExistence(null, teamName))
        {
            throw new Exception("Team not found");
        }

        var team = _db.Teams.FirstOrDefault(team => team.Name == teamName);
        _db.Teams.Remove(team);
        _db.SaveChanges();
    }

    public void RemovePlayerFromTeam(string teamName, int playerId)
    {
        if (!VerifyTeamExistence(null, teamName))
        {
            throw new Exception("Team not found");
        }

        var team = _db.Teams.FirstOrDefault(team => team.Name == teamName);
        if (team.PlayersId.FirstOrDefault(playerId) == null)
        {
            throw new Exception("Player not found");
        }

        team.PlayersId.Remove(playerId);
        _db.SaveChanges();
    }
    
    public void UpdateTeam(int teamId, Team updatedTeam)
    {
        if (!VerifyTeamExistence(teamId, null))
        {
           

            throw new Exception("Team not found");
            
        }
        Team team = _db.Teams.FirstOrDefault(team => team.Id == teamId);
        team.Name = updatedTeam.Name;
        team.PlayersId = updatedTeam.PlayersId;
        _db.SaveChanges();
    }
}
