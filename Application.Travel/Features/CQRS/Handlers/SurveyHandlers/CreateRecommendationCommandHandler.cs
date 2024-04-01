using Application.Travel.Features.CQRS.Commands.SurveyCommands;
using Application.Travel.Features.CQRS.Events;
using Application.Travel.Features.CQRS.Handlers.ReservationHandlers;
using Application.Travel.Interfaces;
using Application.Travel.Models;
using Application.Travel.Services;
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
        private readonly AIRecommendationServiceBuilder _ai;
        private readonly IUow _uow;
        public CreateRecommendationCommandHandler(IRepository<AIRecommendation> repository,
            IMapper mapper, 
            IHttpContextAccessor contextAccessor,
            IRepository<Reservation> ReservationRepository,
            IRepository<Survey> SurveyRepository,
            IMediator mediator,
            IUow uow

            )
        {
            _repository = repository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _ReservationRepository = ReservationRepository;
            _SurveyRepository = SurveyRepository;
            _mediator = mediator;
            _uow = uow; 

        }



        //RESERVATİON APIDE CREATE EDİLECEK

        public async  Task<Response<AIRecommendation>> Handle(CreateRecommendationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = Int32.Parse(_contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
              
                var surveyResults =  _SurveyRepository.GetByFilter(s => s.UserId == userIdClaim);


                var homeLatitude = request.HomeLatitude;
                var homeLongitude = request.HomeLongitude;

             
                var matchingPlaces = GetPlacesMatchingHomeLocation(homeLatitude, homeLongitude);

                
                if (matchingPlaces.Count == 0)
                {
                    return Response<AIRecommendation>.Fail("Evin enlem ve boylam değerlerine eşit olan bir yer bulunamadı.");
                }



                var values = new AIRecommendation
                {   
                    UserId = userIdClaim,
                    PreferredCategories = surveyResults.PreferredCategories,
                    HomeLatitude = homeLatitude,
                    HomeLongitude = homeLongitude,
                    Places = new List<Domain.Travel.Entities.Place>()
                };

                for (int i = 0; i < matchingPlaces.Count; i++)
                {
                    var placeIndex = i + 1;
                    var place = new Domain.Travel.Entities.Place
                    {
                        Latitude = (double)matchingPlaces[i][2],
                        Longitude = (double)matchingPlaces[i][3],
                        Types = (List<string>)matchingPlaces[i][4],
                        Score = 0 
                    };
                    values.Places.Add(place);
                }

                await _repository.AddAsync(values);
                _uow.SaveChangeAsync();

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
