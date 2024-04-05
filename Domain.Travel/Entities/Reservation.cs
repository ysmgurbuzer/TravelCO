using Domain.Travel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class Reservation
    {
    
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [ForeignKey("Housings")]
        public int HousingId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public User User { get; set; }
        public Housing Housing { get; set; }
    }

    
}
