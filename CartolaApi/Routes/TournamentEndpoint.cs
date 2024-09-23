using CartolaApi.Responses.JsonResponse;
using CartolaApi.Routes.Models;
using CartolaApi.Data.Functions;
using DbTournamentModel = CartolaApi.Data.Models.Tournament;
using DbTeamModel = CartolaApi.Data.Models.Team;
using DbPlayerModel = CartolaApi.Data.Models.Player;

namespace CartolaApi.Routes;

public static class TournamentEndpoint
{
   public static void MapGroupTournament(this WebApplication app)
   {
      var group = app.MapGroup("/tournament");
      var tournamentDbFunctions = new TournamentDbFunctions();

      group.MapGet("/", () =>
      {
         try
         {
            List<DbTournamentModel> tournament = tournamentDbFunctions.GetTournaments();
            var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
               status: "success",
               data: tournament,
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
      
      group.MapPost("/create-tournament", (Tournament tournament) =>
      {
         try
         {
            List<DbTeamModel> tempTeams = new List<DbTeamModel>();
            List<DbPlayerModel> tempPlayers = new List<DbPlayerModel>();
            
            foreach (Team team in tournament.Teams)
            {
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
               tempTeams.Add(tempTeam);
            }
            
            DbTournamentModel dbTournament = DbTournamentModel.CreateTournament(
               tournament.TournamentName,
               tempTeams
            );
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
      
      group.MapPut("/update-tournament", (Tournament tournament) =>
      {
         try
         {
            List<DbTeamModel> tempTeams = new List<DbTeamModel>();
            List<DbPlayerModel> tempPlayers = new List<DbPlayerModel>();
            
            foreach (Team team in tournament.Teams)
            {
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
               tempTeams.Add(tempTeam);
            }
            
            DbTournamentModel dbTournament = DbTournamentModel.CreateTournament(
               tournament.TournamentName,
               tempTeams
            );
            tournamentDbFunctions.UpdateTournament(
               dbTournament,
               tournament.TournamentName,
               tempTeams,
               null
            );
            var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
               status: "success",
               data: tournament,
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
      
      group.MapDelete("/delete-tournament", (string tournamentName) =>
      {
         try
         {
            tournamentDbFunctions.DeleteTournament(tournamentName);
            var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
               status: "success",
               data: $"Tournament {tournamentName} deleted",
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

