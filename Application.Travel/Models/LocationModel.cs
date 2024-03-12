using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Models
{
    public class LocationModel
    {
        public int ReservationId { get; set; }
        public List<HomePlace> HomePlace { get; set; }
    }
    public class HomePlace
    {
        public string City { get; set; }
        public string Country { get; set; }

        public double longitude { get; set; }
        public double latitude { get; set; }


    }

    public class LocationStorage
    {
        private static readonly List<List<object>> _placesList = new List<List<object>>();

        public static void AddLoc(int ReservationId,string City, string Country, double latitude, double longitude)
        {
            var placeInfo = new List<object> { ReservationId, City, Country, latitude, longitude };
            _placesList.Add(placeInfo);
        }

        public static List<List<object>> GetLocList() => _placesList;
    }
}
