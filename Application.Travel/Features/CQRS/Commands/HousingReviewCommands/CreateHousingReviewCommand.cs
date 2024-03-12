using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.HousingReviewCommands
{
    public class CreateHousingReviewCommand : IRequest<Response<HousingReview>>
    {
      
        public int UserId { get; set; }
        public int HousingId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
