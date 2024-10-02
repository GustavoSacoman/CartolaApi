using Microsoft.AspNetCore.Builder;
using CartolaApi.Router.v1.endpoints;

namespace CartolaApi.Router
{
    public static class Routes
    {
        public static void MapV1Routes(this WebApplication app)
        {   
            app.MapGroup("/v1")
                .MapGroupPlayer()
                .MapTeamEndpoints()
                .MapGroupTournament()
                .MapGroupUser()
                .MapGroupMatch()
                .MapGroupSeason();
        }
    }
}