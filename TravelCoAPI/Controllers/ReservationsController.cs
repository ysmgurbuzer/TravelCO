using Application.Travel.Features.CQRS.Commands.AIRecommendationCommands;
using Application.Travel.Features.CQRS.Commands.FavoritesCommands;
using Application.Travel.Features.CQRS.Commands.GetNearbyPlacesCommands;
using Application.Travel.Features.CQRS.Commands.ReservationCommands;
using Application.Travel.Features.CQRS.Commands.SurveyCommands;
using Application.Travel.Features.CQRS.Handlers.AIRecommendationHandlers;
using Application.Travel.Features.CQRS.Queries.FavoritesQueries;
using Application.Travel.Features.CQRS.Queries.ReservationQueries;
using Application.Travel.Interfaces;
using Domain.Travel.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelCoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Housing> _repo;
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(IMediator mediator, IRepository<Housing> repo, ILogger<ReservationsController> logger)
        {
            _mediator = mediator;
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListUsersReservations()
        {
            try
            {
                var result = await _mediator.Send(new GetReservationQuery());
                if (result.Succeeded)
                {
                    _logger.LogInformation("User reservations listed successfully.");
                    return Ok(result.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to list user reservations: {Message}", result.Message);
                    return BadRequest(result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while listing user reservations.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationCommand command)
        {
            try
            {
                var values = await _mediator.Send(command);
                if (values.Succeeded)
                {
                    var ıd = command.HousingId;
                    var house = await _repo.GetByIdAsync(ıd);
                    var latitude = house.Location.latitude;
                    var longitude = house.Location.longitude;

                    var nearbyPlacesUrl = "https://localhost:44356/api/GooglePlacesAPI";

                    using (HttpClient client = new HttpClient())
                    {
                        var parameters = new
                        {
                            latitude = latitude,
                            longitude = longitude
                        };
                        var jsonRequest = JsonConvert.SerializeObject(parameters);

                        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(nearbyPlacesUrl, content);
                        if (response.IsSuccessStatusCode)
                        {
                            var locationResult = await _mediator.Send(new GetReservationWithLocationQuery());
                            var recommendationCommand = new CreateRecommendationCommand
                            {
                                HomeLatitude = latitude,
                                HomeLongitude = longitude
                            };
                            var recommendationResult = await _mediator.Send(recommendationCommand);
                            if (recommendationResult.Succeeded)
                            {
                                var aıResult = await _mediator.Send(new ExportToExcelCommand());
                            }
                           
                          
                        }
                        return Ok(values.Message);
                    }
                }
                else
                {
                    _logger.LogWarning("Failed to create reservation: {Message}", values.Message);
                    return BadRequest(values.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating reservation.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            try
            {
                var value = await _mediator.Send(new RemoveReservationCommand(id));
                if (value.Succeeded)
                {
                    _logger.LogInformation("Reservation canceled successfully.");
                    return Ok(value.Message);
                }
                else
                {
                    _logger.LogWarning("Failed to cancel reservation: {Message}", value.Message);
                    return BadRequest(value.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while canceling reservation.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetReservationForOwners(string id)
        //{
        //    try
        //    {
        //        var values = await _mediator.Send(new GetReservationByIdQuery(id));
        //        if (values.Succeeded)
        //        {
        //            _logger.LogInformation("Reservation retrieved successfully.");
        //            return Ok(values);
        //        }
        //        else
        //        {
        //            _logger.LogWarning("Failed to retrieve reservation.");
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while retrieving reservation.");
        //        return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //    }
        //}

    }
}
