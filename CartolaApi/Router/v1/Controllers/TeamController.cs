using AutoMapper;
using CartolaApi.Data.Services;
using DbTeamModel = CartolaApi.Data.DTOs.Team;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Router.v1.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartolaApi.Router.v1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TeamServices _teamServices;

        public TeamController(IMapper mapper, TeamServices teamServices)
        {
            _mapper = mapper;
            _teamServices = teamServices;
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            try
            {
                var teams = _teamServices.GetTeams();
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: teams,
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

        [HttpGet("search-team")]
        public IActionResult GetTeam(int? id, string? name)
        {
            try
            {
                var team = _teamServices.GetTeam(id, name);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: team,
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

        [HttpPost("include-team")]
        public IActionResult CreateTeam([FromBody] Team team)
        {
            try
            {
                var dbTeam = _mapper.Map<DbTeamModel>(team);
                _teamServices.CreateTeam(dbTeam);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: dbTeam,
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

        [HttpPut("update-team")]
        public IActionResult UpdateTeam(int teamToUpdateId, [FromBody] Team updatedTeam)
        {
            try
            {
                var dbTeamUpdated = _mapper.Map<DbTeamModel>(updatedTeam);
                _teamServices.UpdateTeam(teamToUpdateId, dbTeamUpdated);
                var team = _teamServices.GetTeam(teamToUpdateId, null);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: team,
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

        [HttpDelete("delete-team")]
        public IActionResult DeleteTeam(string name)
        {
            try
            {
                _teamServices.DeleteTeam(name);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "Team deleted",
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