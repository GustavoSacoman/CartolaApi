using AutoMapper;
using CartolaApi.Data.Services;
using CartolaApi.Router.v1.Models;
using Microsoft.AspNetCore.Mvc;
using CartolaApi.Responses.JsonResponse;
using dbMatchModel = CartolaApi.Data.DTOs.Match;

namespace CartolaApi.Router.v1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MatchServices _matchServices;

        public MatchController(IMapper mapper, MatchServices matchServices)
        {
            _mapper = mapper;
            _matchServices = matchServices;
        }

        [HttpGet]
        public IActionResult GetMatches()
        {
            try
            {
                List<dbMatchModel> matches = _matchServices.GetMatches();
                var matchDtos = _mapper.Map<List<Match>>(matches);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: matchDtos,
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

        [HttpPost]
        public IActionResult CreateMatch([FromBody] Match match)
        {
            try
            {
                var dbMatch = _mapper.Map<dbMatchModel>(match);
                _matchServices.CreateMatch(dbMatch);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: match,
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

        [HttpPut("{matchId}")]
        public IActionResult UpdateMatch(int matchId, [FromBody] UpdateMatch match)
        {
            try
            {
                var dbMatch = _mapper.Map<dbMatchModel>(match);
                _matchServices.UpdateMatch(dbMatch, matchId);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: match,
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

        [HttpDelete("{matchId}")]
        public IActionResult DeleteMatch(int matchId)
        {
            try
            {
                _matchServices.DeleteMatch(matchId);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "match deleted successfully",
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
    