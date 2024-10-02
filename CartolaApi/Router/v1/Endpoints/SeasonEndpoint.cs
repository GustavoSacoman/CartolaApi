using AutoMapper;
using CartolaApi.Data.Services;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Router.v1.Models;
using Microsoft.AspNetCore.Mvc;
using dbSeasonModel = CartolaApi.Data.DTOs.Season;

namespace CartolaApi.Router.v1.endpoints;
    public static class SeasonEndPoint
    {
        public static void MapGroupSeason(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/season");
            var seasonDbFunctions = new SeasonServices();
            
            group.MapGet("/", () =>
            {
                try
                {
                    var season = seasonDbFunctions.GetSeasons();
                    var (successResponse, successStatusCode) = JsonResponse.Success(
                        status: "success",
                        data: season,
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

            group.MapPost("/create-season", (Season season) =>
            {
                try
                {
                    dbSeasonModel dbSeason = dbSeasonModel.CreateSeason(
                         season.Name,
                        season.StartDate,
                        season.FinalDate,
                        season.tournamentId ?? null 
                        );
                 
                    seasonDbFunctions.CreateSeason(dbSeason);
                    var (successResponse, successStatusCode) = JsonResponse.Success(
                        status: "success",
                        data: "season created successfully",
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

            group.MapPut("/update-season", (int id, Season season, [FromServices] IMapper mapper) =>
            {
                try
                {
                    var seasonDto = mapper.Map<dbSeasonModel>(season);
                    seasonDbFunctions.UpdateSeason(id, seasonDto);
                    var (successResponse, successStatusCode) = JsonResponse.Success(
                        status: "success",
                        data: "Season updated successfully",
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

            group.MapDelete("/delete-season", (int id) =>
            {
                try 
                {
                    seasonDbFunctions.DeleteSeason(id);
                    var (successResponse, successStatusCode) = JsonResponse.Success(
                        status: "success",
                        data: "season deleted successfully",
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







