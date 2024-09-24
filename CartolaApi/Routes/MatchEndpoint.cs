using AutoMapper;
using CartolaApi.Data.Functions;
using CartolaApi.Routes.Models;
using Microsoft.AspNetCore.Mvc;
using CartolaApi.Responses.JsonResponse;

using dbMatchModel = CartolaApi.Data.DTOs.Match;

namespace CartolaApi.Routes;
public static class MatchEndpoint
{
     public static void MapGroupMatch(this WebApplication app)
        {
         

            var group = app.MapGroup("/match");
            
            var matchDbFunctions = new MatchDbFunctions();
            
            group.MapGet("/", ([FromServices]IMapper mapper) =>
            {
                try
                {
                    List<dbMatchModel> match = matchDbFunctions.GetMatches();
                    var matchDtos = mapper.Map<List<Match>>(match);
                    var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                        status: "success",
                        data: matchDtos,
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
            
            group.MapPost("/create-match", (Match match, [FromServices]IMapper mapper) =>
            {
                try
                {
                    var dbMatch = mapper.Map<dbMatchModel>(match);
                    matchDbFunctions.CreateMatch(dbMatch);
                    var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                        status: "success",
                        data: match,
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
            
            group.MapPut("/update-match", (
                int matchId,
                Match match,
                [FromServices]IMapper mapper
            ) =>
            {
                try
                {
                    var dbMatch = mapper.Map<dbMatchModel>(match);
                    matchDbFunctions.UpdateMatch(dbMatch, matchId);
                    var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                        status: "success",
                        data: match,
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
            
            group.MapDelete("/delete-match", (int matchId) =>
            {
                try
                {
                    matchDbFunctions.DeleteMatch(matchId);
                    var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                        status: "success",
                        data: "match deleted successfully",
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