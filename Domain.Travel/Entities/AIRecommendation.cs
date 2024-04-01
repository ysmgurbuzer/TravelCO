
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class AIRecommendation
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
    public class Place
    {
        [Key]
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<string> Types { get; set; }
        public double Score { get; set; }
    }

}
