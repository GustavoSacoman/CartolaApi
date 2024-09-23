using AutoMapper;

using DbTeamModel = CartolaApi.Data.DTOs.Team;
using DbPlayerModel = CartolaApi.Data.DTOs.Player;
using CartolaApi.Data.Functions;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Routes.Models;
using Microsoft.AspNetCore.Mvc;

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

        group.MapPost("/include-team", (Team team, [FromServices]IMapper mapper) =>
        {
            try
            {
                var dbTeam = mapper.Map<DbTeamModel>(team);
                teamDbFunctions.CreateTeam(dbTeam);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: dbTeam,
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

        group.MapPut("/update-team", (
            int teamToUpateId,
            Team updatedTeam,
            [FromServices]IMapper mapper
            ) =>
        {
            try
            {
                var dbTeamUpdated = mapper.Map<DbTeamModel>(updatedTeam);
                teamDbFunctions.UpdateTeam(teamToUpateId, dbTeamUpdated);
                var team = teamDbFunctions.GetTeam(teamToUpateId, null);
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
