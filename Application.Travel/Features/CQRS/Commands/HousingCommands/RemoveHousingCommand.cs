using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.HousingCommands
{
    public class RemoveHousingCommand:IRequest<Response<Housing>>
    {
        public int Id { get; set; }
        public RemoveHousingCommand(int id)
        {
                  Id= id;   
        }
    }
}
