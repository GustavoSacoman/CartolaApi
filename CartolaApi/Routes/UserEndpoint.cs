using CartolaApi.Data.Functions;
using CartolaApi.Models;
using CartolaApi.Responses.JsonResponse;

namespace CartolaApi.Routes;

public static class UserEndpoint
{
    public static void MapGroupUser(this WebApplication app)
    {
        var group = app.MapGroup("/user");
        
        group.MapGet("/", (UserDbFunctions userDbFunctions) =>
        {
            List<User> users = userDbFunctions.GetUsers();
            try
            {
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: users,
                    status_code: 200
                );
                return Results.Json(successResponse, statusCode: successStatusCode);
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
                    status: "error",
                    data: ex.Message,
                    status_code: 400
                );
                return Results.Json(errorResponse, statusCode: errorStatusCode);
            }
        });
        
        group.MapPost("/", (User user, UserDbFunctions userDbFunctions) =>
        {
            try
            {
                userDbFunctions.CreateUser(user.Email, user.Password, user.Name, user.Phone);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: user,
                    status_code: 201
                );
                return Results.Json(successResponse, statusCode: successStatusCode);

            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
                    status: "error",
                    data: ex.Message,
                    status_code: 400
                );
                return Results.Json(errorResponse, statusCode: errorStatusCode); // 400 Bad Request
            }
            
        });

        group.MapPut("/", (User user, UserDbFunctions userDbFunctions) =>
        {
            try
            {
                userDbFunctions.UpdateUser(user.Email, user.Password, user.Name, user.Phone);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: "user updated successfully",
                    status_code: 200
                );
                return Results.Json(successResponse, statusCode: successStatusCode);
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
                    status: "error",
                    data: ex.Message,
                    status_code: 400
                );
                return Results.Json(errorResponse, statusCode: errorStatusCode);
            }
        });

        group.MapDelete("/", (string email, UserDbFunctions userDbFunctions) =>
        {
            try
            {
                userDbFunctions.DeleteUser(email);
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: "user deleted successfully",
                    status_code: 200
                );
                return Results.Json(successResponse, statusCode: successStatusCode);
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.JsonErrorResponse(
                    status: "error",
                    data: ex.Message,
                    status_code: 400
                );
                return Results.Json(errorResponse, statusCode: errorStatusCode);
            }
        });
    }
}