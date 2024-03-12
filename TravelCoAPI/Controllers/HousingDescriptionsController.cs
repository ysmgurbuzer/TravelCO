using Application.Travel.Features.CQRS.Commands.HousingDescriptionCommands;
using Application.Travel.Features.CQRS.Commands.OwnerCommands;
using Application.Travel.Features.CQRS.Queries.HousingDescriptionQueries;
using Application.Travel.Features.CQRS.Queries.OwnerQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelCoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class HousingDescriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HousingDescriptionsController> _logger;

        public HousingDescriptionsController(IMediator mediator, ILogger<HousingDescriptionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListDescriptions()
        {
            try
            {
                var response = await _mediator.Send(new GetDescriptionQuery());
                if (response.Succeeded)
                {
                    _logger.LogInformation("Descriptions listed successfully.");
                    return Ok(response.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to list descriptions: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while listing descriptions.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDescription(int id)
        {
            try
            {
                var response = await _mediator.Send(new GetDescriptionByIdQuery(id));
                if (response.Succeeded)
                {
                    _logger.LogInformation("Description retrieved successfully.");
                    return Ok(response.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve description: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving description.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateDescription(UpdateDescriptionCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                if (response.Succeeded)
                {
                    _logger.LogInformation("Description updated successfully.");
                    return Ok(response.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to update description: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating description.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("{HousingId}")]
        public async Task<IActionResult> DeleteDescription(int HousingId)
        {
            try
            {
                var response = await _mediator.Send(new RemoveDescriptionCommand(HousingId));
                if (response.Succeeded)
                {
                    _logger.LogInformation("Description deleted successfully.");
                    return Ok(response.Message);
                }
                else
                {
                    _logger.LogWarning("Failed to delete description: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting description.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateDescription(CreateDescriptionCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                if (response.Succeeded)
                {
                    _logger.LogInformation("Description created successfully.");
                    return Ok(response.Message);
                }
                else
                {
                    _logger.LogWarning("Failed to create description: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating description.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
