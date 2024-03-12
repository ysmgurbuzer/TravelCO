using Domain.Travel.Entities;
using Domain.Travel.Enums;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.OwnerCommands
{
    public class UpdateOwnerCommand : IRequest<Response<Owner>>
    {

        public int Id { get; set; }
        public DescriptionCommand Command { get; set; }

    }
    public class DescriptionCommand
    {
        public int UserId { get; set; }
        public string Language { get; set; } = "English";
        public string ResponseTime { get; set; }
        public HomeownerTitle Title { get; set; }
    }
}
