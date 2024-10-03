using CartolaApi.Data.Services;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Router.v1.Models;
using Microsoft.AspNetCore.Mvc;
using dbPlayerModel = CartolaApi.Data.DTOs.Player;

namespace CartolaApi.Router.v1.Controllers;
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerServices _playerDbFunctions;

        public PlayerController(PlayerServices playerDbFunctions)
        {
            _playerDbFunctions = playerDbFunctions;
        }

        [HttpGet]
        public IActionResult GetPlayers()
        {
            try
            {
                var players = _playerDbFunctions.GetPlayers();
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: players,
                    statusCode: 200
                );
                return new JsonResult(successResponse) { StatusCode = successStatusCode };
            }
            catch (Exception e)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.Error(
                    status: "error",
                    data: e.Message,
                    statusCode: 400
                );
                return new JsonResult(errorResponse) { StatusCode = errorStatusCode };
            }
        }

        [HttpPost("create-player")]
        public IActionResult CreatePlayer([FromBody] Player player)
        {
            try
            {
                dbPlayerModel dbPlayer = dbPlayerModel.CreatePlayer(
                    player.NamePlayer,
                    player.Position,
                    player.TeamId ?? null
                );
                _playerDbFunctions.CreatePlayer(dbPlayer);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "player created successfully",
                    statusCode: 201
                );
                return new JsonResult(successResponse) { StatusCode = successStatusCode };
            }
            catch (Exception e)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.Error(
                    status: "error",
                    data: e.Message,
                    statusCode: 400
                );
                return new JsonResult(errorResponse) { StatusCode = errorStatusCode };
            }
        }

        [HttpPut("update-player")]
        public IActionResult UpdatePlayer(int id, [FromBody] Player player)
        {
            try
            {
                _playerDbFunctions.UpdatePlayer(id, player.NamePlayer, player.Position);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "player updated successfully",
                    statusCode: 200
                );
                return new JsonResult(successResponse) { StatusCode = successStatusCode };
            }
            catch (Exception e)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.Error(
                    status: "error",
                    data: e.Message,
                    statusCode: 400
                );
                return new JsonResult(errorResponse) { StatusCode = errorStatusCode };
            }
        }

        [HttpDelete("delete-player")]
        public IActionResult DeletePlayer(int id)
        {
            try
            {
                _playerDbFunctions.DeletePlayer(id);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "player deleted successfully",
                    statusCode: 200
                );
                return new JsonResult(successResponse) { StatusCode = successStatusCode };
            }
            catch (Exception e)
            {
                var (errorResponse, errorStatusCode) = JsonResponse.Error(
                    status: "error",
                    data: e.Message,
                    statusCode: 400
                );
                return new JsonResult(errorResponse) { StatusCode = errorStatusCode };
            }
        }
    }
