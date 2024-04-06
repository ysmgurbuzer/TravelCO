using Domain.Travel.Entities;
using Domain.Travel.Enums;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Travel.Enums.HomeServiceTypesClass;

namespace Application.Travel.Features.CQRS.Commands.HousingFeatureCommands
{
    public class CreateHousingFeatureCommand : IRequest<Response<HousingFeatures>>
    {
        public int Id { get; set; }
        public int HousingId { get; set; }
        public HomeServiceTypes Name { get; set; }
        public bool Available { get; set; }

    }
}
