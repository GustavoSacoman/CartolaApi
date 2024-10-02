using AutoMapper;

using CartolaApi.Responses.JsonResponse;
using CartolaApi.Router.v1.Models;
using Microsoft.AspNetCore.Mvc;
using DbTournamentModel = CartolaApi.Data.DTOs.TournamentDTO;
using DbTeamModel = CartolaApi.Data.DTOs.Team;
using DbPlayerModel = CartolaApi.Data.DTOs.Player;
using CartolaApi.Data.DTOs;
using CartolaApi.Data.Services;

namespace CartolaApi.Router.v1.endpoints;

public static class TournamentEndpoint
{
   public static void MapGroupTournament(this IEndpointRouteBuilder routes)
   {
      var group = routes.MapGroup("/tournament");
      var tournamentDbFunctions = new TournamentServices();

     group.MapGet("/", ([FromServices] IMapper mapper) =>
{
    try
    {
        // Obtenha a lista de torneios do banco de dados
        List<TournamentDTO> tournaments = tournamentDbFunctions.GetTournaments();
        
        // Mapeie a lista de torneios para a lista de DTOs
        List<DbTournamentModel> tournamentDtos = mapper.Map<List<DbTournamentModel>>(tournaments);

        var (successResponse, successStatusCode) = JsonResponse.Success(
            status: "success",
            data: tournamentDtos,
            statusCode: 200
        );
        return Results.Json(successResponse, statusCode: successStatusCode);
    }
    catch (Exception ex)
    {
        var (errorResponse, errorStatusCode) = JsonResponse.Error(
            status: "error",
            data: ex.Message,
            statusCode: 400
        );
        return Results.Json(errorResponse, statusCode: errorStatusCode);
    }
});


 group.MapPost("/create-tournament", (TournamentDTO tournament, [FromServices] IMapper mapper) =>
{
    try
    {
        // Mapeie o Tournament para TournamentDTO
        var tournamentDto = mapper.Map<DbTournamentModel>(tournament);

        // Crie o torneio no banco de dados
        tournamentDbFunctions.CreateTournament(tournamentDto);

        var (successResponse, successStatusCode) = JsonResponse.Success(
            status: "success",
            data: tournamentDto,
            statusCode: 201
        );
        return Results.Json(successResponse, statusCode: successStatusCode);
    }
    catch (Exception ex)
    {
        var (errorResponse, errorStatusCode) = JsonResponse.Error(
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
            
            var (successResponse, successStatusCode) = JsonResponse.Success(
               status: "success",
               data: "Tournament updated successfully",
               statusCode: 200
            );
            return Results.Json(successResponse, statusCode: successStatusCode);
         }
         catch (Exception ex)
         {
            var (errorResponse, errorStatusCode) = JsonResponse.Error(
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
            var (successResponse, successStatusCode) = JsonResponse.Success(
               status: "success",
               data: $"Tournament {tournamentId} deleted",
               statusCode: 200
            );
            return Results.Json(successResponse, statusCode: successStatusCode);
         }
         catch (Exception ex)
         {
            var (errorResponse, errorStatusCode) = JsonResponse.Error(
               status: "error",
               data: ex.Message,
               statusCode: 400
            );
            return Results.Json(errorResponse, statusCode: errorStatusCode);
         }
      });
   }

}

