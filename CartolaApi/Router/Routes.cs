using Microsoft.AspNetCore.Builder;

namespace CartolaApi.Router
{
    public static class Routes
    {
        public static void MapV1Routes(this WebApplication app)
        {
            var apiGroup = app.MapGroup("/v1");

            apiGroup.MapControllerRoute(
                name: "player",
                pattern: "api/v1/player/{action=GetPlayers}/{id?}",
                defaults: new { controller = "Player" }
            );

            apiGroup.MapControllerRoute(
                name: "team",
                pattern: "api/v1/team/{action=GetTeams}/{id?}",
                defaults: new { controller = "Team" }
            );

            apiGroup.MapControllerRoute(
                name: "tournament",
                pattern: "api/v1/tournament/{action=GetTournaments}/{id?}",
                defaults: new { controller = "Tournament" }
            );

            apiGroup.MapControllerRoute(
                name: "user",
                pattern: "api/v1/user/{action=GetUsers}/{id?}",
                defaults: new { controller = "User" }
            );

            apiGroup.MapControllerRoute(
                name: "match",
                pattern: "api/v1/match/{action=GetMatches}/{id?}",
                defaults: new { controller = "Match" }
            );

            apiGroup.MapControllerRoute(
                name: "season",
                pattern: "api/v1/season/{action=GetSeasons}/{id?}",
                defaults: new { controller = "Season" }
            );
        }
    }
}