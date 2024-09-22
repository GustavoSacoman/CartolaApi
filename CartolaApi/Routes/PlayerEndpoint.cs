using Microsoft.EntityFrameworkCore;
using CartolaApi.Routes.Models;

namespace CartolaApi.Routes;
    public static class PlayerEndpoint
    {
        public static void MapGroupPlayer(this WebApplication app)
        {
            var group = app.MapGroup("/player");
            group.MapGet("/", async (AppDbContext db) =>
            {
                return await db.Players.ToListAsync();
            });

            group.MapPost("/", async (Player player, AppDbContext db) =>
            {
                Console.WriteLine($"Player: {player}");

                db.Players.Add(player);

                await db.SaveChangesAsync();
                return Results.Created($"/player/{player.Id}", player);
            });

            group.MapPut("/", async (int id,Player playerAlterado, AppDbContext db) =>
            {
                var player = await db.Players.FindAsync(id);
                if (player is null)
                    return Results.NotFound();
                
                player.NamePlayer = playerAlterado.NamePlayer;
                player.Position = playerAlterado.Position;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            group.MapDelete("/", async (int id, AppDbContext db) =>
            {
                if (await db.Players.FindAsync(id) is Player player)
                {
                    db.Players.Remove(player);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                return Results.NotFound();
            });
        }
    }







