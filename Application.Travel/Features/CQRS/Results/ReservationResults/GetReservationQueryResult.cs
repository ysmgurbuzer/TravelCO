using Domain.Travel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Results.ReservationResults
{
    public class GetReservationQueryResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HousingId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public decimal TotalPrice { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
