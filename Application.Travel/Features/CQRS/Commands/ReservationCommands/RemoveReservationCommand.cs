using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.ReservationCommands
{
    public class RemoveReservationCommand : IRequest<Response<Reservation>>
    {
        public int Id { get; set; }
        public RemoveReservationCommand(int id)
        {
            Id = id;
        }

    }
}
