using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Models
{
    public class ReservationInfoPython
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public List<string> PreferredCategories { get; set; }
        public double HomeLatitude { get; set; }
        public double HomeLongitude { get; set; }

        public List<Place> Places { get; set; }


        public Survey? Survey { get; set; }
        public User? User { get; set; }

        public Location? Location { get; set; }



   










    }
}
