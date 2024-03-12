using Application.Travel.Features.CQRS.Commands.ReservationCommands;
using Application.Travel.Features.CQRS.Commands.SurveyCommands;
using Application.Travel.Interfaces;
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
    public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, Response<Survey>>
    {

        private readonly IRepository<Survey> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public CreateSurveyCommandHandler(IRepository<Survey> repository, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            
        }



        public async Task<Response<Survey>> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
              
            
                var values = new Survey
                {
                    UserId = Int32.Parse(userIdClaim),    
                   LikesArtPlaces=request.LikesArtPlaces,
                   LikesCultureAndHistory=request.LikesCultureAndHistory,
                   LikesFarEastCuisine=request.LikesFarEastCuisine,
                   LikesFastFood=request.LikesFastFood,
                   LikesNaturePlaces=request.LikesNaturePlaces,
                   LikesNightlife=request.LikesNightlife,   
                   LikesShopping=request.LikesShopping,
                   LikesSports=request.LikesSports, 
                   LikesTraditionalCuisine=request.LikesTraditionalCuisine,    
                   
                 
                };
                values.PreferredCategories = GetPreferredCategories(values);
              

                await _repository.AddAsync(values);

                return Response<Survey>.Success(values);
            }
            catch (ValidateException ex)
            {
                return Response<Survey>.Fail($"Validation Error: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {

                return Response<Survey>.Fail($"Record not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<Survey>.Fail($"Internal Server Error: {ex.Message}");
            }

        }

        private List<string> GetPreferredCategories(Survey survey)
        {
            var preferredCategories = new List<string>();

            if (survey.LikesArtPlaces)
            {
                preferredCategories.AddRange(new[]
                {
            "convention_center", "cultural_center", "movie_rental", "movie_theater", "museum", "historical_landmark", "tourist_attraction", "visitor_center"
        });
            }

            if (survey.LikesSports)
            {
                preferredCategories.AddRange(new[]
                {
            "athletic_field", "fitness_center", "gym", "ski_resort", "sports_club", "sports_complex", "stadium", "swimming_pool"
        });
            }

            if (survey.LikesCultureAndHistory)
            {
                preferredCategories.AddRange(new[]
                {
            "historical_landmark", "museum", "national_park", "park", "tourist_attraction", "visitor_center", "zoo"
        });
            }

            if (survey.LikesNaturePlaces)
            {
                preferredCategories.AddRange(new[]
                {
            "hiking_area", "marina", "national_park", "park", "zoo"
        });
            }

            if (survey.LikesFarEastCuisine)
            {
                preferredCategories.AddRange(new[]
                {
            "chinese_restaurant", "indian_restaurant", "japanese_restaurant", "korean_restaurant", "sushi_restaurant","middle_eastern_restaurant"
        });
            }

            if (survey.LikesFastFood)
            {
                preferredCategories.AddRange(new[]
                {
            "fast_food_restaurant", "pizza_restaurant"
        });
            }

            if (survey.LikesTraditionalCuisine)
            {
                preferredCategories.AddRange(new[]
                {
            "american_restaurant", "barbecue_restaurant", "breakfast_restaurant", "brunch_restaurant", "italian_restaurant", "mexican_restaurant",  "restaurant", "seafood_restaurant", "steak_house", "turkish_restaurant", "vegetarian_restaurant"
        });
            }

            if (survey.LikesNightlife)
            {
                preferredCategories.AddRange(new[]
                {
            "bar", "nightclub"
        });
            }

            if (survey.LikesShopping)
            {
                preferredCategories.AddRange(new[]
                {
            "convenience_store", "gift_shop", "shopping_mall", "sporting_goods_store", "store", "supermarket"
        });
            }

            return preferredCategories;
        }

    }
}
