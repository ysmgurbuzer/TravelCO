namespace Application.Travel.Models
{
    public class GooglePlacesResponseModel
    {
        public List<Place> Places { get; set; }
    }
    public class Place
    {
        public string Id { get; set; }
        public List<string> Types { get; set; }
        public Locations Location { get; set; }
        public double Rating { get; set; }

        public class Locations
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }

    public class PlaceStorage
    {
        private static readonly List<List<object>> _placesList = new List<List<object>>();

        public static void AddPlace(double homeLatitude,double homelong,double latitude, double longitude, List<string> types, double rating)
        {
            var placeInfo = new List<object> {homeLatitude,homelong ,latitude, longitude, types, rating };
            _placesList.Add(placeInfo);
        }

        public static List<List<object>> GetPlacesList() => _placesList;
    }
}
