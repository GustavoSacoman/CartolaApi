using DbTeamModel = CartolaApi.Data.DTOs.Team;
using DbPlayerModel = CartolaApi.Data.DTOs.Player;
using CartolaApi.Data.Functions;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Routes.Models;

namespace CartolaApi.Routes;
public static class TeamEndpoint
{
    public static void MapTeamEndpoints(this WebApplication app)
    
    {
        var group = app.MapGroup("/teams");
        var teamDbFunctions = new TeamDbFunctions();

        group.MapGet("/", () =>
        {
            try 
            {
                var teams = teamDbFunctions.GetTeams();
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: teams,
                    statusCode: 200
                );
                return Results.Json(successResponse, statusCode: successStatusCode);
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
                    status: "error",
                    data: ex.Message,
                    statusCode: 400
                );
                return Results.Json(errorResponse, statusCode: errorStatusCode);
            }
        });

        
        group.MapGet("/search-team", (int? id,string? name) =>
        {
            try
            {
                var team = teamDbFunctions.GetTeam(id, name);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: team,
                    statusCode: 200
                );
                return Results.Json(successResponse, statusCode: successStatusCode);
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
                    status: "error",
                    data: ex.Message,
                    statusCode: 400
                );
                return Results.Json(errorResponse, statusCode: errorStatusCode);
            }
        });

        group.MapPost("/include-team", (Team team) =>
        {
            try
            {
                List<DbPlayerModel> tempPlayers = new List<DbPlayerModel>();
                foreach (Player player in team.Players)
                {
                    if (player.NamePlayer != null && player.Position != null)
                    {
                        DbPlayerModel tempPlayer = DbPlayerModel.CreatePlayer(
                            player.NamePlayer,
                            player.Position,
                            player.TeamId
                        );
                        tempPlayers.Add(tempPlayer);
                    }
                }
                DbTeamModel tempTeam = DbTeamModel.CreateTeam(
                    team.Name,
                    tempPlayers
                );
                teamDbFunctions.CreateTeam(tempTeam);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: tempTeam,
                    statusCode: 200
                );
                return Results.Json(successResponse, statusCode: successStatusCode);
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
                    status: "error",
                    data: ex.Message,
                    statusCode: 400
                );
                return Results.Json(errorResponse, statusCode: errorStatusCode);
            }            
        });

        group.MapPut("/update-team", (int teamId, Team updatedTeam) =>
        {
            try
            {
                List<DbPlayerModel> tempPlayers = new List<DbPlayerModel>();
                foreach (Player player in updatedTeam.Players)
                {
                    if (player.NamePlayer != null && player.Position != null)
                    {
                        DbPlayerModel tempPlayer = DbPlayerModel.CreatePlayer(
                            player.NamePlayer,
                            player.Position,
                            player.TeamId
                        );
                        tempPlayers.Add(tempPlayer);
                    }
                }
                DbTeamModel tempTeam = DbTeamModel.CreateTeam(
                    updatedTeam.Name,
                    tempPlayers
                );
                teamDbFunctions.UpdateTeam(teamId, updatedTeam.Name, tempPlayers, null);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: tempTeam,
                    statusCode: 200
                );
                return Results.Json(successResponse, statusCode: successStatusCode);
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
                    status: "error",
                    data: ex.Message,
                    statusCode: 400
                );
                return Results.Json(errorResponse, statusCode: errorStatusCode);
            }
        });

        
        group.MapDelete("/delete-team", (string name) =>
        {
            try
            {
                teamDbFunctions.DeleteTeam(name);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: "Team deleted",
                    statusCode: 200
                );
                return Results.Json(successResponse, statusCode: successStatusCode);
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
                    status: "error",
                    data: ex.Message,
                    statusCode: 400
                );
                return Results.Json(errorResponse, statusCode: errorStatusCode);
            }
        });
    }
}
