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
    public class RemoveHousingReviewCommand : IRequest<Response<HousingReview>>
    {
        public int Id { get; set; }
        public RemoveHousingReviewCommand(int id)
        {
            Id = id;
        }
    }
}
