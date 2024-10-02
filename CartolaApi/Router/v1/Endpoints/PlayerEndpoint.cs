using CartolaApi.Data.Services;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Router.v1.Models;
using dbPlayerModel = CartolaApi.Data.DTOs.Player;

namespace CartolaApi.Router.v1.endpoints;
    public static class PlayerEndpoint
    {
        public static void MapGroupPlayer(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/player");
            var playerDbFunctions = new PlayerServices();

            group.MapGet("/", () =>
            {
                try
                {
                    var players = playerDbFunctions.GetPlayers();
                    var (successResponse, successStatusCode) = JsonResponse.Success(
                        status: "success",
                        data: players,
                        statusCode: 200
                    );
                    return Results.Json(successResponse, statusCode: successStatusCode);
                }
                catch (Exception e)
                {
                    var (errorResponse, errorStatusCode) = JsonResponse.Error(
                        status: "error",
                        data: e.Message,
                        statusCode: 400
                    );
                    return Results.Json(errorResponse, statusCode: errorStatusCode);
                }
            });

            group.MapPost("/create-player", (Player player) =>
            {
                try
                {
                    dbPlayerModel dbPlayer = dbPlayerModel.CreatePlayer(
                        player.NamePlayer,
                        player.Position,
                        player.TeamId ?? null
                    );
                    playerDbFunctions.CreatePlayer(dbPlayer);
                    var (successResponse, successStatusCode) = JsonResponse.Success(
                        status: "success",
                        data: "player created successfully",
                        statusCode: 201
                    );
                    return Results.Json(successResponse, statusCode: successStatusCode);
                }
                
                catch (Exception e)
                {
                    var (errorResponse, errorStatusCode) = JsonResponse.Error(
                        status: "error",
                        data: e.Message,
                        statusCode: 400
                    );
                    return Results.Json(errorResponse, statusCode: errorStatusCode);
                }
            });

            group.MapPut("/update-player", (int id, string? newName,string? newPosition) =>
            {
                try
                {
                    playerDbFunctions.UpdatePlayer(id, newName, newPosition);
                    var (successResponse, successStatusCode) = JsonResponse.Success(
                        status: "success",
                        data: "player updated successfully",
                        statusCode: 200
                    );
                    return Results.Json(successResponse, statusCode: successStatusCode);
                }
                catch (Exception e)
                {
                    var (errorResponse, errorStatusCode) = JsonResponse.Error(
                        status: "error",
                        data: e.Message,
                        statusCode: 400
                    );
                    return Results.Json(errorResponse, statusCode: errorStatusCode);
                }
            });

            group.MapDelete("/delete-player", (int id) =>
            {
                try 
                {
                    playerDbFunctions.DeletePlayer(id);
                    var (successResponse, successStatusCode) = JsonResponse.Success(
                        status: "success",
                        data: "player deleted successfully",
                        statusCode: 200
                    );
                    return Results.Json(successResponse, statusCode: successStatusCode);
                }
                catch (Exception e)
                {
                    var (errorResponse, errorStatusCode) = JsonResponse.Error(
                        status: "error",
                        data: e.Message,
                        statusCode: 400
                    );
                    return Results.Json(errorResponse, statusCode: errorStatusCode);
                }
            });
        }
    }







