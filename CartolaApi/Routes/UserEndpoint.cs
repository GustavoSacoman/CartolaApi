using CartolaApi.Data.Functions;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Routes.Models;
using DbUserModel = CartolaApi.Data.Models.User;

namespace CartolaApi.Routes;

public static class UserEndpoint
{
    public static void MapGroupUser(this WebApplication app)
    {
        var group = app.MapGroup("/user");
        
        group.MapGet("/", (UserDbFunctions userDbFunctions) =>
        {
            
            try
            {
                List<DbUserModel> users = userDbFunctions.GetUsers();
                var (successResponse, successStatusCode) = JsonResponse.JsonSuccessResponse(
                    status: "success",
                    data: users,
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
        
        group.MapPost("/", (User user, UserDbFunctions userDbFunctions) =>
        {
            try
            {
                DbUserModel dbUser = DbUserModel.CreateUser(
                    user.Email,
                    user.Password,
                    user.Name,
                    user.Phone
                );
                userDbFunctions.CreateUser(dbUser.Email, dbUser.Password, dbUser.Name, dbUser.Phone);
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

        group.MapPut("/", (User user, UserDbFunctions userDbFunctions) =>
        {
            try
            {
                userDbFunctions.UpdateUser(user.Email, user.Password, user.Name, user.Phone);
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

        group.MapDelete("/", (string email, UserDbFunctions userDbFunctions) =>
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