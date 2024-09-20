using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;

public static class TournamentEndpoint
{
   public static void MapGroupTournament(this WebApplication app)
   {
      var group = app.MapGroup("/tournament");
      group.MapGet("/", async (AppDbContext db) => { return await db.Tournaments.ToListAsync(); });

      group.MapPost("/", async (Tournament tournament, AppDbContext db) =>
      {

         Console.WriteLine($"/tournament/{tournament}");

         db.Tournaments.Add(tournament);
         await db.SaveChangesAsync();
         return Results.Created($"tournament/{tournament.Id}", tournament);
      });

      group.MapPut("/", async (int id, Tournament tournamentUpdated, AppDbContext db) =>
      {

         var tournament = await db.Tournaments.FindAsync(id);
         if (tournament is null)
            return Results.NotFound("");

         tournament.TournamentName = tournamentUpdated.TournamentName;

         await db.SaveChangesAsync();
         return Results.NoContent();

      });

      group.MapDelete("/", async (int id, AppDbContext db) =>
      {

         if (await db.Tournaments.FindAsync(id) is Tournament tournament)
         {
            db.Tournaments.Remove(tournament);
            return Results.NoContent();
         }

         return Results.NotFound();
      });
   }
}