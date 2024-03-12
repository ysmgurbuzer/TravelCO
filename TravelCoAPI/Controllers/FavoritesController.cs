using Application.Travel.Features.CQRS.Commands.FavoritesCommands;
using Application.Travel.Features.CQRS.Queries.FavoritesQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelCoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FavoritesController> _logger;

        public FavoritesController(IMediator mediator, ILogger<FavoritesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListFavorites()
        {
            try
            {
                var values = await _mediator.Send(new GetFavoritesQuery());
                if (values.Succeeded)
                {
                    _logger.LogInformation("Favorites listed successfully.");
                    return Ok(values);
                }
                else
                {
                    _logger.LogWarning("Failed to list favorites.");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while listing favorites.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavorites(CreateFavoritesCommand command)
        {
            try
            {
                var values = await _mediator.Send(command);
                if (values.Succeeded)
                {
                    _logger.LogInformation("Favorites created successfully.");
                    return Ok(values);
                }
                else
                {
                    _logger.LogWarning("Failed to create favorites.");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating favorites.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorites(int id)
        {
            try
            {
                var value = await _mediator.Send(new RemoveFavoritesCommand(id));
                if (value.Succeeded)
                {
                    _logger.LogInformation("Favorites deleted successfully.");
                    return Ok();
                }
                else
                {
                    _logger.LogWarning("Failed to delete favorites.");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting favorites.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFavorites(int id)
        {
            try
            {
                var values = await _mediator.Send(new GetFavoritesByIdQuery(id));
                if (values.Succeeded)
                {
                    _logger.LogInformation("Favorites retrieved successfully.");
                    return Ok(values);
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve favorites.");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving favorites.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}

