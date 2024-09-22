using Microsoft.EntityFrameworkCore;
using CartolaApi.Models;

namespace CartolaApi.Endpoints
{
    
public static class TeamApi
{
    public static void MapTeamEndpoints(this WebApplication app)
    {
            var group = app.MapGroup("/teams");

            group.MapGet("/", async (AppDbContext db) =>
            {
                return await db.Teams.Include(t => t.Players).ToListAsync();
            });

            
            group.MapGet("/{id:int}", async (int id, AppDbContext db) =>
            {
                var team = await db.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == id);
                if (team == null)
                    return Results.NotFound();

                return Results.Ok(team);
            });

            group.MapPost("/", async (Team team, AppDbContext db) =>
            {
                db.Teams.Add(team);
                await db.SaveChangesAsync();
                return Results.Created($"/teams/{team.Id}", team);
            });

            group.MapPut("/{id:}", async (int id, Team updatedTeam, AppDbContext db) =>
            {
                var team = await db.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == id);
                if (team == null)
                    return Results.NotFound();

                team.TeamName = updatedTeam.TeamName;
                team.Players = updatedTeam.Players;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            
            group.MapDelete("/{id:int}", async (int id, AppDbContext db) =>
            {
                var team = await db.Teams.FindAsync(id);
                if (team == null)
                    return Results.NotFound();

                db.Teams.Remove(team);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
    }
}
}
