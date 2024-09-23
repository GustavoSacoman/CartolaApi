using CartolaApi.Data.Functions;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Routes.Models;
using dbPlayerModel = CartolaApi.Data.DTOs.Player;

namespace CartolaApi.Routes;
    public static class PlayerEndpoint
    {
        public static void MapGroupPlayer(this WebApplication app)
        {
            var group = app.MapGroup("/player");
            var playerDbFunctions = new PlayerDbFunctions();

            group.MapGet("/", () =>
            {
                try
                {
                    var players = playerDbFunctions.GetPlayers();
                    var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                        status: "success",
                        data: players,
                        statusCode: 200
                    );
                    return Results.Json(successResponse, statusCode: successStatusCode);
                }
                catch (Exception e)
                {
                    var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
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
                    var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                        status: "success",
                        data: "player created successfully",
                        statusCode: 201
                    );
                    return Results.Json(successResponse, statusCode: successStatusCode);
                }
                
                catch (Exception e)
                {
                    var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
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
                    var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                        status: "success",
                        data: "player updated successfully",
                        statusCode: 200
                    );
                    return Results.Json(successResponse, statusCode: successStatusCode);
                }
                catch (Exception e)
                {
                    var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
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
                    var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                        status: "success",
                        data: "player deleted successfully",
                        statusCode: 200
                    );
                    return Results.Json(successResponse, statusCode: successStatusCode);
                }
                catch (Exception e)
                {
                    var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
                        status: "error",
                        data: e.Message,
                        statusCode: 400
                    );
                    return Results.Json(errorResponse, statusCode: errorStatusCode);
                }
            });
        }
    }







