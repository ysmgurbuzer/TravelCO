using Application.Travel.Features.CQRS.Queries.CategoryQueries;
using Application.Travel.Features.CQRS.Queries.FavoritesQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelCoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IMediator mediator, ILogger<CategoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListofCategory()
        {
            try
            {
                var values = await _mediator.Send(new GetCategoryQuery());
                if (values.Succeeded)
                {
                    _logger.LogInformation("Category listed successfully.");
                    return Ok(values.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to list categories.");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while listing categories.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            try
            {
                var values = await _mediator.Send(new GetCategoryByIdQuery(id));
                if (values.Succeeded)
                {
                    _logger.LogInformation("Categories retrieved successfully.");
                    return Ok(values.Data);
                }
                else
                {
                    _logger.LogWarning("Failed to retrieve categories.");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving categories.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
