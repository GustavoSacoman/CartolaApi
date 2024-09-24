using AutoMapper;

using CartolaApi.Responses.JsonResponse;
using CartolaApi.Routes.Models;
using CartolaApi.Data.Functions;
using Microsoft.AspNetCore.Mvc;
using DbTournamentModel = CartolaApi.Data.DTOs.TournamentDTO;
using DbTeamModel = CartolaApi.Data.DTOs.Team;
using DbPlayerModel = CartolaApi.Data.DTOs.Player;
using CartolaApi.Data.DTOs;

namespace CartolaApi.Routes;

public static class TournamentEndpoint
{
   public static void MapGroupTournament(this WebApplication app)
   {
      var group = app.MapGroup("/tournament");
      var tournamentDbFunctions = new TournamentDbFunctions();

     group.MapGet("/", ([FromServices] IMapper mapper) =>
{
    try
    {
        // Obtenha a lista de torneios do banco de dados
        List<TournamentDTO> tournaments = tournamentDbFunctions.GetTournaments();
        
        // Mapeie a lista de torneios para a lista de DTOs
        List<DbTournamentModel> tournamentDtos = mapper.Map<List<DbTournamentModel>>(tournaments);

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


 group.MapPost("/create-tournament", (Tournament tournament, [FromServices] IMapper mapper) =>
{
    try
    {
        // Mapeie o Tournament para TournamentDTO
        var tournamentDTO = mapper.Map<DbTournamentModel>(tournament);

        // Crie o torneio no banco de dados
        tournamentDbFunctions.CreateTournament(tournamentDTO);

        var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
            status: "success",
            data: tournamentDTO,
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



      group.MapPut("/update-tournament", ( int tournamentId,string newTournameName, [FromServices]IMapper mapper ) =>
      {
         try
         {
            tournamentDbFunctions.UpdateTournament(
               tournamentId,
               newTournameName
              
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

