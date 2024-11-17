using AutoMapper;
using CartolaApi.Data.Services;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Router.v1.Models;
using Microsoft.AspNetCore.Mvc;
using DbUserModel = CartolaApi.Data.DTOs.User;

namespace CartolaApi.Router.v1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserServices _userServices;

        public UserController(IMapper mapper, UserServices userServices)
        {
            _mapper = mapper;
            _userServices = userServices;
        }

        [HttpGet("get-users")]
        public IActionResult GetUsers()
        {
            try
            {
                List<DbUserModel> users = _userServices.GetUsers();
                var userDtos = _mapper.Map<List<User>>(users);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: userDtos,
                    statusCode: 200
                );
                return new JsonResult(successResponse) { StatusCode = successStatusCode };
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.Error(
                    status: "error",
                    data: ex.Message,
                    statusCode: 400
                );
                return new JsonResult(errorResponse) { StatusCode = errorStatusCode };
            }
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                var dbUser = _mapper.Map<DbUserModel>(user);
                _userServices.CreateUser(dbUser);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: user,
                    statusCode: 201
                );
                return new JsonResult(successResponse) { StatusCode = successStatusCode };
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.Error(
                    status: "error",
                    data: ex.Message,
                    statusCode: 400
                );
                return new JsonResult(errorResponse) { StatusCode = errorStatusCode };
            }
        }

        [HttpPut("update-user")]
        public IActionResult UpdateUser([FromBody] UserUpdate user)
        {
            try
            {
                var dbUser = _mapper.Map<DbUserModel>(user);
                _userServices.UpdateUser(dbUser.Email, dbUser.Password, dbUser.Name, dbUser.Phone);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "user updated successfully",
                    statusCode: 200
                );
                return new JsonResult(successResponse) { StatusCode = successStatusCode };
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.Error(
                    status: "error",
                    data: ex.Message,
                    statusCode: 400
                );
                return new JsonResult(errorResponse) { StatusCode = errorStatusCode };
            }
        }

        [HttpDelete("delete-user")]
        public IActionResult DeleteUser(string email)
        {
            try
            {
                _userServices.DeleteUser(email);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "user deleted successfully",
                    statusCode: 200
                );
                return new JsonResult(successResponse) { StatusCode = successStatusCode };
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.Error(
                    status: "error",
                    data: ex.Message,
                    statusCode: 400
                );
                return new JsonResult(errorResponse) { StatusCode = errorStatusCode };
            }
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin user)
        {
            try
            {
                var dbUser = _mapper.Map<DbUserModel>(user);
                Dictionary<string,string> userdata = _userServices.Login(dbUser.Email, dbUser.Password);
                {
                    var (successResponse, successStatusCode) = JsonResponse.Success(
                        status: "success",
                        data: userdata,
                        statusCode: 200
                    );
                    return new JsonResult(successResponse) { StatusCode = successStatusCode };
                }
            }
            catch (Exception ex)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.Error(
                    status: "error",
                    data: ex.Message,
                    statusCode: 400
                );
                return new JsonResult(errorResponse) { StatusCode = errorStatusCode };
            }
        }
    }
}