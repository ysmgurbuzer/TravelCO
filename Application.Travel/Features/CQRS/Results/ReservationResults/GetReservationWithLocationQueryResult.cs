using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Results.ReservationResults
{
    public class GetReservationWithLocationQueryResult
    {
       public string City {  get; set; }    
        public string Country {  get; set; }    
        public double longitude { get; set; }
        public double latitude { get; set; }
    }
}
