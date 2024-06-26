﻿namespace TravelCoAPI.Models
{
    public class FinalRouteModel
    {
        public double Source_Lat { get; set; }
        public double Source_Long { get; set; }
        public double Target_Lat { get; set; }
        public double Target_Long { get; set; }
        public int User_ID { get; set; }
        public double Distance { get; set; }
        public double Time { get; set; }
        public string TravelMode { get; set; }
        public string VehicleName { get; set; }
        public double Puan { get; set; }
        public int RezNo { get; set; }
        public string Route_ID { get; set; }
        public int? HousingId { get; set; }

    }
}
