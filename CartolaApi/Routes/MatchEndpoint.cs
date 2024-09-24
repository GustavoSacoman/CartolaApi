using CartolaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CartolaApi.Routes;
public static class MatchEndpoint
{
     public static void MapGroupMatch(this WebApplication app)
        {
         

            var group = app.MapGroup("/match");

            group.MapGet("/", async (AppDbContext db) =>
            {
               
                return await db.Matches.Include(m => m.IdTournament).ToListAsync();
            });

            group.MapPost("/", async (Match match, AppDbContext db) =>
            {
                Console.WriteLine($"Match: {match}");

                db.Matches.Add(match);
                await db.SaveChangesAsync();

                return Results.Created($"/match/{match.IdMatch}", match);
            });

            group.MapPut("/{id:int}", async (int id, Match matchAlterado, AppDbContext db) =>
            {
            
                var match = await db.Matches.FindAsync(id);
                if (match is null)
                {
                    return Results.NotFound();
                }

                match.Date = matchAlterado.Date;
                match.Result = matchAlterado.Result;
                match.IdTeam1 = matchAlterado.IdTeam1;
                match.IdTeam2 = matchAlterado.IdTeam2;
                match.IdTournament = matchAlterado.IdTournament;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", async (int id, AppDbContext db) =>
            {
                if (await db.Matches.FindAsync(id) is Match match)
                {
                    db.Matches.Remove(match);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                return Results.NotFound();
            });
        }
    }