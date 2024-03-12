
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
    public class CreateDescriptionCommand:IRequest<Response<HousingDescriptions>>
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public int HousingId { get; set; }
    }
}
