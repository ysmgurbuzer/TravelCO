using Application.Travel.Features.CQRS.Commands.AIRecommendationCommands;
using Application.Travel.Features.CQRS.Commands.FavoritesCommands;
using Application.Travel.Features.CQRS.Commands.GetNearbyPlacesCommands;
using Application.Travel.Features.CQRS.Commands.ReservationCommands;
using Application.Travel.Features.CQRS.Commands.SurveyCommands;
using Application.Travel.Features.CQRS.Handlers.AIRecommendationHandlers;
using Application.Travel.Features.CQRS.Queries.FavoritesQueries;
using Application.Travel.Features.CQRS.Queries.ReservationQueries;
using Application.Travel.Interfaces;
using Application.Travel.Models;
using Application.Travel.Services;
using Domain.Travel.Entities;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using Persistence.Travel.Repositories;
using System;
using System.Diagnostics;
using System.Text;
using TravelCoAPI.Models;
using static OfficeOpenXml.ExcelErrorValue;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelCoAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Housing> _repo;
        private readonly IRepository<Location> _locationRepo;
        private readonly ILogger<ReservationsController> _logger;
        private readonly IRepository<AIRecommendation> _airepo;
        private readonly IRepository<Domain.Travel.Entities.Place> _placerepo;
        private readonly IRepository<PlaceEntity> _plcepo;
        private readonly IUow _uow;
        public ReservationsController(IMediator mediator, IRepository<Housing> repo, ILogger<ReservationsController> logger, IRepository<Location> locationRepo, IRepository<AIRecommendation> airepo, IRepository<Domain.Travel.Entities.Place> placerepo,IUow uow, IRepository<PlaceEntity> plcepo)
        {
            _mediator = mediator;
            _repo = repo;
            _logger = logger;
            _locationRepo = locationRepo;
            _airepo = airepo;
            _placerepo = placerepo;
            _uow = uow;
            _plcepo = plcepo;   
        }

        //getbyıd kullanarak ownerın evine yapılan rezervasyonları görüntüle

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


        [HttpGet("{housingId}")]
        public async Task<IActionResult> ListOwnersReservations(int housingId)
        {
            try
            {
                var result = await _mediator.Send(new GetReservationByHousingIdForOwnerQuery(housingId));
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
                    var location = await _locationRepo.GetByIdAsync(house.LocationId);
                    var latitude = location.latitude;
                    var longitude = location.longitude;

                    var nearbyPlacesUrl = "http://localhost:19175/api/GooglePlacesAPI";

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

                            await _uow.SaveChangeAsync();
                          
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



        [HttpPost("ReceiveScores")]
        public async Task<IActionResult> Post(ReservationScoreModel model)
        {
            if (model == null || model.Top_15_Scores == null || model.Top_15_Scores.Count == 0)
            {
                return BadRequest("Geçersiz istek verisi.");
            }

            int rezno = model.Rezno;
            int userId = model.UserId;

            var filePath = @"C:\Users\ysmgu\OneDrive\Masaüstü\Predictions.xlsx";
            FileInfo fileInfo = new FileInfo(filePath);
            ExcelPackage package;

            if (!fileInfo.Exists)
            {
                package = new ExcelPackage(fileInfo);
                var worksheet = package.Workbook.Worksheets.Add("Tabelle1");

                worksheet.Cells[1, 1].Value = "RezNo";
                worksheet.Cells[1, 2].Value = "UserId";
                worksheet.Cells[1, 3].Value = "Source_PlaceLat";
                worksheet.Cells[1, 4].Value = "Source_PlaceLong";
                worksheet.Cells[1, 5].Value = "Target_PlaceLat";
                worksheet.Cells[1, 6].Value = "Target_PlaceLong";
                worksheet.Cells[1, 7].Value = "Route_Id";
                worksheet.Cells[1, 8].Value = "Distance";
                worksheet.Cells[1, 9].Value = "Time";
                worksheet.Cells[1, 10].Value = "VehicleName";
                worksheet.Cells[1, 11].Value = "TravelMode";
               

            }
            else
            {
                package = new ExcelPackage(fileInfo);
            }

            var existingWorksheet = package.Workbook.Worksheets.First(); 

            int rowCount = existingWorksheet.Dimension?.Rows ?? 0;
            rowCount++;

            int iteration = 1;
            List<string> uniqueCoordinates = new List<string>();
            foreach (var score in model.Top_15_Scores)
            {
                var homeLatitude = model.HomeLatitude;
                var homeLongitude = model.HomeLongitude;

                float destinationLatitude = score.Latitude;
                float destinationLongitude = score.Longitude;
               

                string coordinates = $"{destinationLatitude},{destinationLongitude}";
                if (!uniqueCoordinates.Contains(coordinates))
                {
                    uniqueCoordinates.Add(coordinates);

                }
                var routeapi = "http://localhost:19175/api/Directions";

                var parameters = new
                {
                    sourceLat = (double)homeLatitude,
                    sourceLng = (double)homeLongitude,
                    destLat = (double)destinationLatitude,
                    destLng = (double)destinationLongitude,
                };

                var jsonRequest = JsonConvert.SerializeObject(parameters);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(routeapi, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var routes = JsonConvert.DeserializeObject<JObject>(responseBody);

                        foreach (var mode in routes)
                        {
                            var modeName = mode.Key;
                            var modeRoutes = mode.Value["routes"].Take(3);
                            foreach (var route in modeRoutes)
                            {
                                var legs = route["legs"];
                                foreach (var leg in legs)
                                {
                                    var transitDetails = leg["transit_details"];
                                    var steps = leg["steps"];

                                    var stepDictionary = new Dictionary<string, Tuple<string, string>>();

                                    foreach (var step in steps)
                                    {
                                        var stepDuration = step["duration"]["text"].ToString();
                                        var stepDistance = step["distance"]["text"].ToString();
                                        var stepMode = step["travel_mode"].ToString();

                                        if (modeName == "transit")
                                        {
                                            var vehicleName = step["transit_details"]?["line"]?["vehicle"]?["name"]?.ToString() ?? "";

                                            if (stepMode == "WALKING")
                                            {
                                                vehicleName = "Walking";
                                            }
                                            if (stepMode == "BICYCLING")
                                            {
                                                vehicleName = "Bicycling";
                                            }
                                            if (stepMode == "DRIVING")
                                            {
                                                vehicleName = "Driving";
                                            }
                                            if (stepDictionary.ContainsKey(vehicleName))
                                            {
                                                int count = 1;
                                                while (stepDictionary.ContainsKey($"{vehicleName} {count}"))
                                                {
                                                    count++;
                                                }
                                                vehicleName = $"{vehicleName} {count}";
                                            }

                                            stepDictionary.Add(vehicleName, new Tuple<string, string>(stepDuration, stepDistance));
                                        }
                                    }

                                    var airoutemodel = new AIRoute
                                    {
                                        UserId = userId,
                                        ReservationId = rezno,
                                        Source_Place_Latitude = homeLatitude,
                                        Source_Place_Longutude = homeLongitude,
                                        Target_Place_Latitude = destinationLatitude,
                                        Target_Place_Longutude = destinationLongitude,
                                        CO2 = 0,
                                        Distance = route["legs"][0]["distance"]["text"].ToString(),
                                        Duration = route["legs"][0]["duration"]["text"].ToString(),
                                        VehicleName = modeName == "transit" ? JsonConvert.SerializeObject(stepDictionary) : "Unknown",
                                        TravelMode = modeName,
                                        RouteNo = route["overview_polyline"]["points"].ToString()
                                    };

                                    existingWorksheet.Cells[rowCount, 1].Value = airoutemodel.ReservationId;
                                    existingWorksheet.Cells[rowCount, 2].Value = airoutemodel.UserId;
                                    existingWorksheet.Cells[rowCount, 3].Value = airoutemodel.Source_Place_Latitude;
                                    existingWorksheet.Cells[rowCount, 4].Value = airoutemodel.Source_Place_Longutude;
                                    existingWorksheet.Cells[rowCount, 5].Value = airoutemodel.Target_Place_Latitude;
                                    existingWorksheet.Cells[rowCount, 6].Value = airoutemodel.Target_Place_Longutude;
                                    existingWorksheet.Cells[rowCount, 7].Value = airoutemodel.RouteNo;
                                    existingWorksheet.Cells[rowCount, 8].Value = airoutemodel.Distance;
                                  
                                    existingWorksheet.Cells[rowCount, 9].Value = airoutemodel.Duration;
                                    existingWorksheet.Cells[rowCount, 10].Value = airoutemodel.VehicleName;
                                    existingWorksheet.Cells[rowCount, 11].Value = airoutemodel.TravelMode;
                                    

                                    rowCount++;
                                }
                            }
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
               
                }
              


            }
           

            package.Save();
          

            return Ok("Veriler Excel dosyasına kaydedildi.");
        }

        //[HttpGet("runpythonscript1")]
        //public void Runpythonscript1()
        //{
        //    try
        //    {
        //        Process.Start("python", @"C:\Users\ysmgu\PycharmProjects\pythonProject7");
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine("hata oluştu: " + ex.Message);
        //    }
        //}

        //[HttpGet("runpythonscript2")]
        //public void RunPythonScript2()
        //{
        //    try
        //    {
        //        Process.Start("python", @"C:\Users\ysmgu\PycharmProjects\pythonProject8");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("hata oluştu: " + ex.Message);
        //    }
        //}
    }
}

