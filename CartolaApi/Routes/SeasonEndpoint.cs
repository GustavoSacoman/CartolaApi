using CartolaApi.Models;
using Microsoft.EntityFrameworkCore;

public static class SeasonEndpoint
{
    public static void MapSeasonEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/season");

        
        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.Seasons.ToListAsync();
        });

        
        group.MapGet("/{id:int}", async (int id, AppDbContext db) =>
        {
            var season = await db.Seasons.FindAsync(id);
            if (season == null)
                return Results.NotFound();

            return Results.Ok(season);
        });

        
        group.MapPost("/", async (Season season, AppDbContext db) =>
        {
            db.Seasons.Add(season);
            await db.SaveChangesAsync();
            return Results.Created($"/season/{season.Id}", season);
        });

        
        group.MapPut("/{id:int}", async (int id, Season updatedSeason, AppDbContext db) =>
        {
            var season = await db.Seasons.FindAsync(id);
            if (season == null)
                return Results.NotFound();

            season.Name = updatedSeason.Name;
            season.StartDate = updatedSeason.StartDate;
            season.FinalDate = updatedSeason.FinalDate;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        
        group.MapDelete("/{id:int}", async (int id, AppDbContext db) =>
        {
            var season = await db.Seasons.FindAsync(id);
            if (season == null)
                return Results.NotFound();

            db.Seasons.Remove(season);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
