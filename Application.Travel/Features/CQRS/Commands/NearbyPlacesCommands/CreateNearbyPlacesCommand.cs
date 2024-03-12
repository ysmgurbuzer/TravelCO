using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.GetNearbyPlacesCommands
{
    public class CreateNearbyPlacesCommand : IRequest<Unit>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
