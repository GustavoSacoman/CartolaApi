using AutoMapper;
using CartolaApi.Data.Services;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Router.v1.Models;
using Microsoft.AspNetCore.Mvc;
using TournamentDto = CartolaApi.Data.DTOs.TournamentDTO;

namespace CartolaApi.Router.v1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TournamentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TournamentServices _tournamentServices;

        public TournamentController(IMapper mapper, TournamentServices tournamentServices)
        {
            _mapper = mapper;
            _tournamentServices = tournamentServices;
        }

        [HttpGet]
        public IActionResult GetTournaments()
        {
            try
            {
                List<TournamentDto> tournaments = _tournamentServices.GetTournaments();
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: tournaments,
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

        [HttpPost("create-tournament")]
        public IActionResult CreateTournament([FromBody] Tournament tournament)
        {
            try
            {
                var tournamentDto = _mapper.Map<TournamentDto>(tournament);
                _tournamentServices.CreateTournament(tournamentDto);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: tournamentDto,
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

        [HttpPut("update-tournament")]
        public IActionResult UpdateTournament(int tournamentId, string newTournamentName)
        {
            try
            {
                _tournamentServices.UpdateTournament(tournamentId, newTournamentName);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "Tournament updated successfully",
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

        [HttpDelete("delete-tournament")]
        public IActionResult DeleteTournament(int tournamentId)
        {
            try
            {
                _tournamentServices.DeleteTournament(tournamentId);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: $"Tournament {tournamentId} deleted",
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
    }
}