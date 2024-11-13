using Microsoft.EntityFrameworkCore;

using CartolaApi.Data.DTOs;
namespace CartolaApi.Data.Services;

public class SeasonServices
{
   private readonly AppDbContext _db;

    public SeasonServices()
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
        _db.SaveChanges();
    }
    public void UpdateSeason(int SeasonId, Season newSeason)
    {
        Season updatedSeason = _db.Seasons.FirstOrDefault(s => SeasonId == s.Id);
        updatedSeason.Name = newSeason.Name;
        updatedSeason.StartDate = newSeason.StartDate;
        updatedSeason.FinalDate = newSeason.FinalDate;
       // updatedSeason.TournamentsId = newSeason.TournamentsId;
        _db.Update(updatedSeason);
        _db.SaveChanges();
    }
    public void DeleteSeason(int SeasonId)
    {
        if (!verifySeasonExistence(SeasonId))
            throw new Exception("Season doesn't exist");
        Season? season = _db.Seasons.FirstOrDefault(season => SeasonId == season.Id);
        _db.Seasons.Remove(season);
        _db.SaveChanges();
    }

}