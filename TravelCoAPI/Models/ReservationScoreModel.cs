namespace TravelCoAPI.Models
{
    public class PlaceData
    {
        public float? Score { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public class ReservationScoreModel
    {
        public int Rezno { get; set; }
        public int UserId { get; set; }
        public List<PlaceData> Top_15_Scores { get; set; }
        public float HomeLatitude { get; set; }
        public float HomeLongitude { get; set; }
    }

}
