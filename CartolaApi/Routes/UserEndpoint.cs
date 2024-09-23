using AutoMapper;
using CartolaApi.Data.Functions;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Routes.Models;
using Microsoft.AspNetCore.Mvc;
using DbUserModel = CartolaApi.Data.DTOs.User;

namespace CartolaApi.Routes;

public static class UserEndpoint
{
    public static void MapGroupUser(this WebApplication app)
    {
        var group = app.MapGroup("/user");
        var userDbFunctions = new UserDbFunctions();

        group.MapGet("/get-users", ([FromServices]IMapper mapper) =>
        {
            try
            {
                List<DbUserModel> users = userDbFunctions.GetUsers();
                var userDtos = mapper.Map<List<User>>(users);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: userDtos,
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

        group.MapPost("/add-user", (User user, [FromServices]IMapper mapper) =>
        {
            try
            {
                var dbUser = mapper.Map<DbUserModel>(user);
                userDbFunctions.CreateUser(dbUser);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: user,
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

        group.MapPut("/update-user", (User user, [FromServices]IMapper mapper) =>
        {
            try
            {
                var dbUser = mapper.Map<DbUserModel>(user);
                userDbFunctions.UpdateUser(dbUser.Email, dbUser.Password, dbUser.Name, dbUser.Phone);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: "user updated successfully",
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

        group.MapDelete("/delete-user", (string email) =>
        {
            try
            {
                userDbFunctions.DeleteUser(email);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: "user deleted successfully",
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