using AutoMapper;

using CartolaApi.Responses.JsonResponse;
using CartolaApi.Routes.Models;
using CartolaApi.Data.Functions;
using Microsoft.AspNetCore.Mvc;
using DbTournamentModel = CartolaApi.Data.DTOs.Tournament;
using DbTeamModel = CartolaApi.Data.DTOs.Team;
using DbPlayerModel = CartolaApi.Data.DTOs.Player;

namespace CartolaApi.Routes;

public static class TournamentEndpoint
{
   public static void MapGroupTournament(this WebApplication app)
   {
      var group = app.MapGroup("/tournament");
      var tournamentDbFunctions = new TournamentDbFunctions();

      group.MapGet("/", ([FromServices]IMapper mapper) =>
      {
         try
         {
            List<DbTournamentModel> tournament = tournamentDbFunctions.GetTournaments();
            var tournamentDtos = mapper.Map<List<Tournament>>(tournament);
            var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
               status: "success",
               data: tournamentDtos,
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
      
      group.MapPost("/create-tournament", (Tournament tournament, [FromServices]IMapper mapper) =>
      {
         try
         {
            var dbTournament = mapper.Map<DbTournamentModel>(tournament);
            tournamentDbFunctions.CreateTournament(dbTournament);
            var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
               status: "success",
               data: tournament,
               statusCode: 201
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
      
      group.MapPut("/update-tournament", (
         int tournamentId,
         string newTournameName,
         [FromBody]List<Team>? teams,
         
         [FromServices]IMapper mapper
         ) =>
      {
         try
         {
            List<DbTeamModel>? dbTeams = null;
            DbTeamModel? dbTeam = null;
            if (teams != null)
            {
               dbTeams = mapper.Map<List<DbTeamModel>>(teams);
            }

            tournamentDbFunctions.UpdateTournament(
               tournamentId,
               newTournameName,
               dbTeams,
               dbTeam
            );
            
            var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
               status: "success",
               data: "Tournament updated successfully",
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
      
      group.MapDelete("/delete-tournament", (int tournamentId) =>
      {
         try
         {
            tournamentDbFunctions.DeleteTournament(tournamentId);
            var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
               status: "success",
               data: $"Tournament {tournamentId} deleted",
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

