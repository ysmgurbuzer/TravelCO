using Application.Travel.Features.CQRS.Commands.SurveyCommands;
using Application.Travel.Features.CQRS.Commands.UserCommands;
using Application.Travel.Features.CQRS.Queries.UserQueries;
using Application.Travel.Services;
using Application.Travel.Tools;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TravelCoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<AuthController> _logger;
  
        public AuthController(IMediator mediator, JwtTokenGenerator jwtTokenGenerator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;  
            
          
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] GetCheckUserQuery query)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var values = await _mediator.Send(query);
                    if (values.Data.IsExist)
                    {
                        _logger.LogInformation("User logged in successfully.");
                        var tokenResponse = _jwtTokenGenerator.GenerateToken(values.Data);
                    
                        return Ok(new { token = tokenResponse.Token, expiration = tokenResponse.ExpireDate });
                    }
                    else
                    {
                        _logger.LogWarning("Login failed: {Message}", values.Message);
                        return BadRequest(values.Message);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("Register")]
      
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state during registration.");
                    return BadRequest();
                }

                var values = await _mediator.Send(command);
                if (values.Succeeded)
                {
                    _logger.LogInformation("User registered successfully.");
                    return Ok();
                }
                else
                {
                    _logger.LogWarning("User registration failed: {Message}", values.Message);
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during registration.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("CreateSurvey")]
        public async Task<IActionResult> CreateSurvey(CreateSurveyCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);

                if (response.Succeeded)
                {
                    _logger.LogInformation("Survey created successfully.");
                    return Ok(response.Message);
                }
                else
                {
                    _logger.LogWarning("Survey creation failed: {Message}", response.Message);
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during survey creation.");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
