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
        public int UserId { get; set; }

        public List<object> PreferredCategories { get; set; }
        public double HomeLatitude { get; set; }
        public double HomeLongitude { get; set; }

        public double Place1Latitude { get; set; }
        public double Place1Longitude { get; set; }
        public object Place1Type { get; set; }

        public double Place2Latitude { get; set; }
        public double Place2Longitude { get; set; }
        public object Place2Type { get; set; }

        public double Place3Latitude { get; set; }
        public double Place3Longitude { get; set; }
        public object Place3Type { get; set; }

        public double Place1Score { get; set; }
        public double Place2Score { get; set; }
        public double Place3Score { get; set; }

    }
}
