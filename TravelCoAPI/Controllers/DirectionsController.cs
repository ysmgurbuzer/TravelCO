using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using TravelCoAPI.Models;

namespace TravelCoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionsController : ControllerBase
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public DirectionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

   

    [HttpPost]
    public async Task<IActionResult> GetDirections(GetDirectionModel model)
    {
        try
            {
           

                string apiKey = "API_KEY";

            HttpClient client = _httpClientFactory.CreateClient();

            string encodedSourceLat = model.sourceLat.ToString().Replace(',', '.');
            string encodedSourceLng = model.sourceLng.ToString().Replace(',', '.');
            string encodedDestLat = model.destLat.ToString().Replace(',', '.');
            string encodedDestLng = model.destLng.ToString().Replace(',', '.');

            JObject allRoutes = new JObject();

            string[] modes = { "driving", "walking", "bicycling", "transit" };
            string[] transitModes = { "bus", "subway", "train", "tram", "rail" };

            foreach (string mode in modes)
            {
                string apiUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={encodedSourceLat},{encodedSourceLng}&destination={encodedDestLat},{encodedDestLng}&alternatives=true&mode={mode}&key={apiKey}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                string responseBody = await response.Content.ReadAsStringAsync();

                allRoutes[mode] = JObject.Parse(responseBody);
            }

            return Content(allRoutes.ToString(), "application/json");
        }
        catch (Exception ex)
        {
           
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }




}
}
