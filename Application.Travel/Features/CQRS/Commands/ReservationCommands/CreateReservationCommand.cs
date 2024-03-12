using Domain.Travel.Entities;
using Domain.Travel.Enums;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.ReservationCommands
{
    public class CreateReservationCommand : IRequest<Response<Reservation>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HousingId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
     
    }
}
