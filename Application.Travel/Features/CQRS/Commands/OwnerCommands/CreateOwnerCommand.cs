using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.OwnerCommands
{
    public class CreateOwnerCommand : IRequest<Response<Owner>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ResponseTime { get; set; }
        public HomeownerTitle Title { get; set; }
    }
}
