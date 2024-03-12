
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public  double longitude { get; set; }
        public double latitude { get; set; }

        public Housing Housing { get; set; }
    }
    
}