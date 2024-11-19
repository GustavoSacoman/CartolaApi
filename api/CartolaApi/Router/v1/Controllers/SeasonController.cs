using AutoMapper;
using CartolaApi.Data.Services;
using CartolaApi.Responses.JsonResponse;
using CartolaApi.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using dbSeasonModel = CartolaApi.Data.DTOs.Season;


namespace CartolaApi.Router.v1.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SeasonController : ControllerBase
    {
        private readonly SeasonServices _seasonServices;

        public SeasonController(SeasonServices seasonServices)
        {
            _seasonServices = seasonServices;
        }

        // GET: api/v1/season
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

        // POST: api/v1/season
[HttpPost]

public IActionResult CreateSeason([FromBody] Season season)
{
    try
    {
        Console.WriteLine("Dados recebidos pelo backend:");
        Console.WriteLine($"Name: {season.Name}, StartDate: {season.StartDate}, FinalDate: {season.FinalDate}");

        _seasonServices.CreateSeason(season);

        var (successResponse, successStatusCode) = JsonResponse.Success(
            status: "success",
            data: "Season created successfully",
            statusCode: 201
        );
        return new JsonResult(successResponse) { StatusCode = successStatusCode };
    }
    catch (Exception e)
    {
        Console.WriteLine("Erro no backend: " + e.Message);
        var (errorResponse, errorStatusCode) = JsonResponse.Error(
            status: "error",
            data: e.Message,
            statusCode: 400
        );
        return new JsonResult(errorResponse) { StatusCode = errorStatusCode };
    }
}



        // PUT: api/v1/season/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateSeason(int id, [FromBody] Season season)
        {
            try
            {
                season.Id = id;  // Ensures the correct ID is used
                _seasonServices.UpdateSeason(id, season);
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

        // DELETE: api/v1/season/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteSeason(int id)
        {
            try
            {
                _seasonServices.DeleteSeason(id);
                var (successResponse, successStatusCode) = JsonResponse.Success(
                    status: "success",
                    data: "Season deleted successfully",
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

