using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Handlers.HousingHandlers;
using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Results.HousingResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static OfficeOpenXml.ExcelErrorValue;

namespace TravelCoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HousingController> _logger;

        public HousingController(IMediator mediator, ILogger<HousingController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> HousingList()
        {
            try
            {
                var response = await _mediator.Send(new GetHousingQuery());

                if (response.Succeeded)
                {
                    _logger.LogInformation("Housing list retrieved successfully.");
                    return Ok(response.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve housing list: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving housing list.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHousing(int id)
        {
            try
            {
                var response = await _mediator.Send(new GetHousingByIdQuery(id));

                if (response.Succeeded)
                {
                    _logger.LogInformation("Housing retrieved successfully.");
                    var city = response.Data.Location.City;
                    
                    var airQualityQuery = new GetCurrentAirQualityQuery(city); 

                 
                    var airQualityResponse = await _mediator.Send(airQualityQuery);
                    response.Data.AirQuality = airQualityResponse.Data.Data.Aqi;
                    response.Data.AirDescription = airQualityResponse.Data.Message;
                    return Ok(response.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve housing: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving housing.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
      
        public async Task<IActionResult> CreateHousing(CreateHousingCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);

                if (response.Succeeded)
                {
                    _logger.LogInformation("Housing created successfully.");
                    return Ok(response.Message);
                }
                else
                {
                    _logger.LogWarning("Failed to create housing: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating housing.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> RemoveHousing(int id)
        {
            try
            {
                var response = await _mediator.Send(new RemoveHousingCommand(id));

                if (response.Succeeded)
                {
                    _logger.LogInformation("Housing removed successfully.");
                    return Ok(response.Message);
                }
                else
                {
                    _logger.LogWarning("Failed to remove housing: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing housing.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateHousing(UpdateHousingCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);

                if (response.Succeeded)
                {
                    _logger.LogInformation("Housing updated successfully.");
                    return Ok(response.Message);
                }
                else
                {
                    _logger.LogWarning("Failed to update housing: {Message}", response.Message);
                    return Unauthorized(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating housing.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

  

        [HttpGet("GetListByOwner")]
        [Authorize(Roles = "2")]//kontrol et sayıyla çalışıyor 
        public async Task<IActionResult> GetListByOwner()
        {
            try
            {
                var response = await _mediator.Send(new GetHousingByOwnerQuery());

                if (response.Succeeded)
                {
                    _logger.LogInformation("Housing list retrieved successfully.");
                    return Ok(response.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve housing list: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving housing list.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

    }
}
