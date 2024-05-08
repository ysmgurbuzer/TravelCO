using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class AIRoute
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }

        public float Source_Place_Latitude { get; set; }
        public float Source_Place_Longutude { get; set; }
        public float Target_Place_Latitude { get; set; }
        public float Target_Place_Longutude { get; set; }

        public string? RouteNo { get; set; }
        public decimal? CO2 { get; set; }
        public string  Duration { get; set; }

        public string TravelMode { get; set; }
        public string Distance { get; set;}
        public string? VehicleName { get; set; } //sadece transitler için
    }
}
