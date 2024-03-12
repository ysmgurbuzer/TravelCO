using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.HousingFeatureCommands;
using Application.Travel.Features.CQRS.Queries.HousingFeaturesQueries;
using Application.Travel.Features.CQRS.Queries.HousingQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelCoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousingFeatureController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HousingFeatureController> _logger;

        public HousingFeatureController(IMediator mediator, ILogger<HousingFeatureController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListFeatures()
        {
            try
            {
                var response = await _mediator.Send(new GetHousingFeatureQuery());
                if (response.Succeeded)
                {
                    _logger.LogInformation("Features listed successfully.");
                    return Ok(response.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to list features: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while listing features.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeature(int id)
        {
            try
            {
                var response = await _mediator.Send(new GetHousingFeatureByIdQuery(id));
                if (response.Succeeded)
                {
                    _logger.LogInformation("Feature retrieved successfully.");
                    return Ok(response.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve feature: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving feature.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateHousingFeatureCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                if (response.Succeeded)
                {
                    _logger.LogInformation("Feature created successfully.");
                    return Ok(response.Message);
                }
                else
                {
                    _logger.LogWarning("Failed to create feature: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating feature.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFeature(int id)
        {
            try
            {
                var response = await _mediator.Send(new RemoveHousingFeatureCommand(id));
                if (response.Succeeded)
                {
                    _logger.LogInformation("Feature removed successfully.");
                    return Ok(response.Message);
                }
                else
                {
                    _logger.LogWarning("Failed to remove feature: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing feature.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeature(UpdateHousingFeatureCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                if (response.Succeeded)
                {
                    _logger.LogInformation("Feature updated successfully.");
                    return Ok(response.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to update feature: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating feature.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
