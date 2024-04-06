using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.SurveyCommands
{
    public class CreateRecommendationCommand : IRequest<Response<AIRecommendation>>
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public List<string> PreferredCategories { get; set; }
        public double HomeLatitude { get; set; }
        public double HomeLongitude { get; set; }

        public List<Place> Places { get; set; }


    }
    public class Place
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<string> Types { get; set; }
        public double Score { get; set; }
    }

}

