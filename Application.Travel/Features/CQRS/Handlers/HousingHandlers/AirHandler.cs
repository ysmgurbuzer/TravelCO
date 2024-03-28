using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Results.HousingResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.HousingHandlers
{
    public class AirHandler : IRequestHandler<GetCurrentAirQualityQuery, Response<AirQualityResponse>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public AirHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = "1fb16cb31ee3af790b5c6c6d1950b0d3c625e4c6";
        }

        public async Task<Response<AirQualityResponse>> Handle(GetCurrentAirQualityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var apiUrl = $"http://api.waqi.info/feed/{request.City}/?token={_apiKey}";

                var response = await client.GetAsync(apiUrl, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var airQualityData = JsonConvert.DeserializeObject<AirQualityResponse>(responseBody);
                string message = GetAirQualityMessage(airQualityData.Data.Aqi);
                var model = new AirQualityResponse
                {
                    Message = message,
                    Data = airQualityData.Data
                };
                return Response<AirQualityResponse>.Success(model);
            }
            catch (Exception ex)
            {
                return Response<AirQualityResponse>.Fail($"An error occurred while fetching air quality data for {request.City}: {ex.Message}");
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


    public class GetCurrentAirQualityQuery : IRequest<Response<AirQualityResponse>>
    {
        public string City { get; }

        public GetCurrentAirQualityQuery(string city)
        {
            City = city;
        }
    }

    public class AirQualityResponse
    {
        public string Status { get; set; }

        public string Message {  get; set; }    
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
