using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.HousingFeatureCommands
{
    public class RemoveHousingFeatureCommand : IRequest<Response<HousingFeatures>>
    {
        public int Id { get; set; }
        public RemoveHousingFeatureCommand(int id)
        {
            Id = id;
        }
    }
}
