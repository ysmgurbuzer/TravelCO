using API.Travel.Models;
using Application.Travel.Models;
using Azure.Core;
using Domain.Travel.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;


namespace TravelCoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirQualityAPIController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public AirQualityAPIController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            // API anahtarını burada ayarlayın
            _apiKey = "1fb16cb31ee3af790b5c6c6d1950b0d3c625e4c6";
        }


        [HttpGet("{city}")]
        public async Task<IActionResult> GetCurrentAirQuality(string city)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var apiUrl = $"http://api.waqi.info/feed/{city}/?token={_apiKey}";

                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var airQualityData = JsonConvert.DeserializeObject<AirQualityResponse>(responseBody);

                var aqiValue = airQualityData?.Data?.Aqi;

                string message = GetAirQualityMessage(aqiValue);

                return Ok(new { AQI = aqiValue, Message = message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string GetAirQualityMessage(int? aqiValue)
        {
            if (!aqiValue.HasValue)
                return "AQI değeri bulunamadı.";

            if (aqiValue >= 0 && aqiValue <= 50)
                return "Hava kalitesi iyi. Hava kirliliği neredeyse hiçbir risk oluşturmaz.";

            if (aqiValue >= 51 && aqiValue <= 100)
                return "Hava kalitesi orta. Bazı kirleticiler için kabul edilebilir; ancak, hava kirliliğine son derece duyarlı olan çok az insan için orta derecede bir sağlık endişesi olabilir.";

            if (aqiValue >= 101 && aqiValue <= 150)
                return "Hava hassas gruplar için sağlıksız. Hassas grup üyeleri sağlık sorunları yaşayabilir.";

            if (aqiValue >= 151 && aqiValue <= 200)
                return "Hava genel olarak sağlıksız. Herkes sağlık etkilerini yaşamaya başlayabilir.";

            if (aqiValue >= 201 && aqiValue <= 300)
                return "Hava çok sağlıksız. Acil durum koşulları için sağlık uyarıları.";

            if (aqiValue > 300)
                return "Hava tehlikeli. Tüm dış mekanda etkinliği sınırlayın.";

            return "Geçersiz AQI değeri.";
        }
    }

    public class AirQualityResponse
    {
        public string Status { get; set; }
        public AirQualityData Data { get; set; }
    }

    public class AirQualityData
    {
        public int Aqi { get; set; }
        public Time Time { get; set; }
        public City City { get; set; }
        public Iaqi Iaqi { get; set; }
    }

    public class Time
    {
        public string S { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string[] Geo { get; set; }
    }

    public class Iaqi
    {
        [JsonProperty("pm25")]
        public PM25 Pm25 { get; set; }
    }

    public class PM25
    {
        public string Value { get; set; }
    }
}

