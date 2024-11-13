using AutoMapper;
using CartolaApi.Data.Services;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Router.v1.Models;
using Microsoft.AspNetCore.Mvc;
using dbSeasonModel = CartolaApi.Data.DTOs.Season;

namespace CartolaApi.Router.v1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SeasonController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SeasonServices _seasonServices;

        public SeasonController(IMapper mapper, SeasonServices seasonServices)
        {
            _mapper = mapper;
            _seasonServices = seasonServices;
        }

        [HttpGet]
        public IActionResult GetSeasons()
        {
            try
            {
                var seasons = _seasonServices.GetSeasons();
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: seasons,
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

        [HttpPost]
        public IActionResult CreateSeason([FromBody] Season season)
        {
            try
            {
                dbSeasonModel dbSeason = dbSeasonModel.CreateSeason(
                    season.Name,
                    season.StartDate,
                    season.FinalDate,
                    season.tournamentId ?? null
                );

                _seasonServices.CreateSeason(dbSeason);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "season created successfully",
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

        [HttpPut("{id}")]
        public IActionResult UpdateSeason(int id, [FromBody] Season season)
        {
            try
            {
                var seasonDto = _mapper.Map<dbSeasonModel>(season);
                _seasonServices.UpdateSeason(id, seasonDto);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "Season updated successfully",
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

        [HttpDelete("{id}")]
        public IActionResult DeleteSeason(int id)
        {
            try
            {
                _seasonServices.DeleteSeason(id);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "season deleted successfully",
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
}