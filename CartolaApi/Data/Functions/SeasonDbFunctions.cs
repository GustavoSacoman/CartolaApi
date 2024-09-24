using Microsoft.EntityFrameworkCore;

using CartolaApi.Data.DTOs;
namespace CartolaApi.Data.Functions;

public class SeasonDbFunctions
{
   private readonly AppDbContext _db;

    public SeasonDbFunctions()
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
    public bool verifySeasonExistence(int SeasonId)
    {
        Season? season = _db.Seasons.FirstOrDefault(s => SeasonId == s.Id);
        return season != null;
    }
    
    public List<Season> GetSeasons()
    {
        return _db.Seasons.ToList();
    }

    public void CreateSeason(Season season)
    {
        if (verifySeasonExistence(season.Id))
            throw new Exception("Season already exist");
        
        _db.Seasons.Add(season);
    }
    public void EditSeason(int SeasonId, Season newSeason)
    {
        Season season1 = _db.Seasons.FirstOrDefault(s => SeasonId == s.Id);
        season1.Name = newSeason.Name;
        season1.StartDate = newSeason.StartDate;
        season1.FinalDate = newSeason.FinalDate;
        season1.Tournaments = newSeason.Tournaments;
        _db.SaveChanges();
    }
    public void DeleteSeason(int SeasonId)
    {
        if (!verifySeasonExistence(SeasonId))
            throw new Exception("Season doesn't exist");
        Season? season = _db.Seasons.FirstOrDefault(season => SeasonId == season.Id);
        _db.Seasons.Remove(season);
    }

}