using Application.Travel.Features.CQRS.Commands.RouteCommands;
using Application.Travel.Features.CQRS.Queries.FavoritesQueries;
using Application.Travel.Features.CQRS.Queries.RouteQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelCoAPI.Models;

namespace TravelCoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoutesController(IMediator mediator)
        {
                _mediator   = mediator;
        }

        [HttpPost("routes")]
        public async Task<IActionResult> GetRoutes(List<FinalRouteModel> routes)
        {
            CreateRouteCommand createRouteCommand = new CreateRouteCommand();
            createRouteCommand.RoutesList = routes.Select(route => new RouteList
            {
                Source_Lat = route.Source_Lat,
                Source_Long = route.Source_Long,
                Target_Lat = route.Target_Lat,
                Target_Long = route.Target_Long,
                User_ID = route.User_ID,
                Distance = route.Distance,
                Time = route.Time,
                TravelMode = route.TravelMode,
                VehicleName = route.VehicleName,
                Puan = route.Puan,
                RezNo = route.RezNo,
                Route_ID = route.Route_ID
            }).ToList();

            var values = await _mediator.Send(createRouteCommand);
            if (values.Succeeded)
            {
                return Ok("Routes saved successfully.");
            }
            else
            {
                return BadRequest();
            }
           
        }

        [HttpGet("{housingId}")]
        public async Task<IActionResult> GetRoutesById(int housingId)
        {
            var values = await _mediator.Send(new GetRouteByHousingIdQuery(housingId));
            if (values.Succeeded)
            {
                return Ok(values.Data);
            }
            else { return BadRequest(); }   
        }
    }
}
