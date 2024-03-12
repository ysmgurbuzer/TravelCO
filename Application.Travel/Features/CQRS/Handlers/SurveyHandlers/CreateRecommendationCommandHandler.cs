using Application.Travel.Features.CQRS.Commands.SurveyCommands;
using Application.Travel.Features.CQRS.Events;
using Application.Travel.Features.CQRS.Handlers.ReservationHandlers;
using Application.Travel.Interfaces;
using Application.Travel.Models;
using AutoMapper;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.SurveyHandlers
{
    public class CreateRecommendationCommandHandler : IRequestHandler<CreateRecommendationCommand, Response<AIRecommendation>>
    {
        private readonly IRepository<AIRecommendation> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepository<Reservation> _ReservationRepository;
        private readonly IRepository<Survey> _SurveyRepository;
        private readonly IMediator _mediator;
        public CreateRecommendationCommandHandler(IRepository<AIRecommendation> repository,
            IMapper mapper, 
            IHttpContextAccessor contextAccessor,
            IRepository<Reservation> ReservationRepository,
            IRepository<Survey> SurveyRepository,
            IMediator mediator

            )
        {
            _repository = repository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _ReservationRepository = ReservationRepository;
            _SurveyRepository = SurveyRepository;
            _mediator = mediator;

        }



        //RESERVATİON APIDE CREATE EDİLECEK

        public async  Task<Response<AIRecommendation>> Handle(CreateRecommendationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = Int32.Parse(_contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
              
                var surveyResults =  _SurveyRepository.GetList(s => s.UserId == userIdClaim);


                var homeLatitude = request.HomeLatitude;
                var homeLongitude = request.HomeLongitude;

                // Evin konumuyla eşleşen yerleri bul
                var matchingPlaces = GetPlacesMatchingHomeLocation(homeLatitude, homeLongitude);

                // Evin konumuyla eşleşen yer bulunamazsa hata döndür
                if (matchingPlaces.Count == 0)
                {
                    return Response<AIRecommendation>.Fail("Evin enlem ve boylam değerlerine eşit olan bir yer bulunamadı.");
                }

                // Rastgele 3 yer seç
                var random = new Random();
                var placeIndex1 = random.Next(0, matchingPlaces.Count);
                var placeIndex2 = random.Next(0, matchingPlaces.Count);
                var placeIndex3 = random.Next(0, matchingPlaces.Count);

                // Yerlerin özelliklerini AIRecommendation nesnesine aktar
                var values = new AIRecommendation
                {
                    UserId = userIdClaim,
                    PreferredCategories = surveyResults.PreferredCategories,
                    HomeLatitude = homeLatitude,
                    HomeLongitude = homeLongitude,
                    Place1Latitude = (double)matchingPlaces[placeIndex1][2],
                    Place1Longitude = (double)matchingPlaces[placeIndex1][3],
                    Place1Type = (List<string>)matchingPlaces[placeIndex1][4],
                    Place2Latitude = (double)matchingPlaces[placeIndex2][2],
                    Place2Longitude = (double)matchingPlaces[placeIndex2][3],
                    Place2Type = (List<string>)matchingPlaces[placeIndex2][4],
                    Place3Latitude = (double)matchingPlaces[placeIndex3][2],
                    Place3Longitude = (double)matchingPlaces[placeIndex3][3],
                    Place3Type = (List<string>)matchingPlaces[placeIndex3][4]
                };

                Console.WriteLine((double)PlaceStorage.GetPlacesList()[placeIndex1][0]);
                Console.WriteLine((double)PlaceStorage.GetPlacesList()[placeIndex1][1]);
                await _repository.AddAsync(values);
             
                return Response<AIRecommendation>.Success(values);
            }
            catch (ValidateException ex)
            {
                return Response<AIRecommendation>.Fail($"Validation Error: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {

                return Response<AIRecommendation>.Fail($"Record not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<AIRecommendation>.Fail($"Internal Server Error: {ex.Message}");
            }

        }
        private List<List<object>> GetPlacesMatchingHomeLocation(double homeLatitude, double homeLongitude)
        {
            var placesList = PlaceStorage.GetPlacesList();
            var matchingPlaces = new List<List<object>>();
            foreach (var place in placesList)
            {
                if (place[0] is double && place[1] is double)
                {
                    var latitude = (double)place[0];
                    var longitude = (double)place[1];
                    if (latitude == homeLatitude && longitude == homeLongitude)
                    {
                        matchingPlaces.Add(place);
                    }
                }
            }
            return matchingPlaces;
        }





    }
}
