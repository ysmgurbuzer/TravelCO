using API.Travel.Models;
using Application.Travel.Features.CQRS.Commands.GetNearbyPlacesCommands;
using Application.Travel.Features.CQRS.Commands.SurveyCommands;
using Application.Travel.Features.CQRS.Queries.ReservationQueries;
using Application.Travel.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace TravelCoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GooglePlacesAPIController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly string _api = "API KEY";
        private readonly ILogger<GooglePlacesAPIController> _logger;

        public GooglePlacesAPIController(string apinew, IMediator mediator, ILogger<GooglePlacesAPIController> logger)
        {
            _api = apinew;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GetNearbyPlaces([FromBody] LocationofHomeModel home)
        {

            try
            {
                var apiUrl = "https://places.googleapis.com/v1/places:searchNearby";
                var includedTypes = new[]
  {

    "convention_center",
    "cultural_center",
    "hiking_area",
    "historical_landmark",
    "marina",
    "movie_rental",
    "movie_theater",
    "national_park",
    "night_club",
    "park",
    "tourist_attraction",
    "visitor_center",
    "zoo",
    "american_restaurant",
    "bakery",
    "bar",
    "barbecue_restaurant",
    "breakfast_restaurant",
    "brunch_restaurant",
    "cafe",
    "chinese_restaurant",
    "fast_food_restaurant",
    "hamburger_restaurant",
    "indian_restaurant",
    "italian_restaurant",
    "japanese_restaurant",
    "korean_restaurant",
    "mexican_restaurant",
    "middle_eastern_restaurant",
    "pizza_restaurant",
    "restaurant",
    "seafood_restaurant",
    "steak_house",
    "sushi_restaurant",
    "turkish_restaurant",
    "vegetarian_restaurant",
    "convenience_store",
    "gift_shop",
    "shopping_mall",
    "sporting_goods_store",
    "store",
    "supermarket",
    "athletic_field",
    "fitness_center",
    "gym",
    "ski_resort",
    "sports_club",
    "sports_complex",
    "stadium",
    "swimming_pool"
};

                var data = new
                {
                    maxResultCount = 20,
                    locationRestriction = new
                    {
                        circle = new
                        {
                            center = new { home.latitude, home.longitude },
                            radius = 30000
                        }
                    },
                    includedTypes
                };

                var jsonRequest = JsonConvert.SerializeObject(data);
                var apiKeyParam = $"key=AIzaSyC4QvdTDRc-m_mSHcqxcvPh0BncxdLeXOU";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-Goog-FieldMask", "*");
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{apiUrl}?{apiKeyParam}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var contentType = response.Content.Headers.ContentType?.MediaType;

                        if (!string.IsNullOrEmpty(contentType))
                        {
                            var places = JsonConvert.DeserializeObject<GooglePlacesResponseModel>(jsonResponse);
                            var shuffledPlaces = ShufflePlaces(places.Places);

                            
                            var selectedPlaces = shuffledPlaces.Take(20).ToList();

                            foreach (var place in selectedPlaces)
                            {
                                PlaceStorage.AddPlace(home.latitude, home.longitude, place.Location.Latitude, place.Location.Longitude, place.Types, place.Rating);
                            }
                            var placesList = PlaceStorage.GetPlacesList();

                            var a = placesList.Count;


                            int idCount = places.Places.Count;

                            var jsonResponseObj = new
                            {
                                places = places,
                                idCount = idCount
                            };

                            var jsonResponsee = JsonConvert.SerializeObject(jsonResponseObj);

                            return Content(jsonResponsee, "application/json");

                        }

                        return Ok(jsonResponse);
                    }
                    _logger.LogWarning("Google Places API request failed with status code: {StatusCode}", response.StatusCode);
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching nearby places from Google Places API.");
                return StatusCode(500, ex.Message);
            }
        }


        private List<Application.Travel.Models.Place> ShufflePlaces(List<Application.Travel.Models.Place> places)
        {
            Random rnd = new Random();
            for (int i = places.Count - 1; i > 0; i--)
            {
                int randomIndex = rnd.Next(0, i + 1);
                Application.Travel.Models.Place temp = places[i];
                places[i] = places[randomIndex];
                places[randomIndex] = temp;
            }
            return places;
        }
    }
}


