using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.HousingDescriptionCommands
{
    public class RemoveDescriptionCommand : IRequest<Response<HousingDescriptions>>
    {
        public int HousingId { get; set; }
        public RemoveDescriptionCommand(int housingId)
        {
            HousingId = housingId;
        }
    }
}
