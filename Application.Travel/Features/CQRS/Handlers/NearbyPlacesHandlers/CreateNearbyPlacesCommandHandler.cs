using Application.Travel.Features.CQRS.Commands.GetNearbyPlacesCommands;
using Application.Travel.Models;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.NearbyPlacesHandlers
{
    public class CreateNearbyPlacesCommandHandler : IRequestHandler<CreateNearbyPlacesCommand, Unit>
    {

        private readonly string _apiKey = "AIzaSyAP8xFXLmlUSx7OgN0t8_XSCBHUZE4t4AY";

        public CreateNearbyPlacesCommandHandler(string apiKey)
        {
            _apiKey = apiKey;
        }



        public async Task<Unit> Handle(CreateNearbyPlacesCommand request, CancellationToken cancellationToken)
        {
            var apiUrl = "https://places.googleapis.com/v1/places:searchNearby";
            var data = new
            {
                maxResultCount = 3,
                locationRestriction = new
                {
                    circle = new
                    {
                        center = new { request.Latitude, request.Longitude },
                        radius = 500.0
                    }
                }
            };

            var jsonRequest = JsonConvert.SerializeObject(data);
            var apiKeyParam = $"key={_apiKey}";

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

                        foreach (var place in places.Places)
                        {
                            PlaceStorage.AddPlace(request.Latitude,request.Longitude,place.Location.Latitude, place.Location.Longitude, place.Types, place.Rating);
                        }

                        return Unit.Value;
                    }
                    return Unit.Value;
                }

                throw new Exception("Google Places API çağrısı başarısız oldu.");
            }
        }
    }
}
