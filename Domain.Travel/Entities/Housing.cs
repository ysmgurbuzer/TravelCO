
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Travel.Enums.CategoryTypes;

namespace Domain.Travel.Entities
{
    public class Housing
    {

        public int Id { get; set; }
        public string HouseTitle { get; set; }
        public int LocationId {  get; set; }   
        public int OwnerId { get; set; }
        public Category CategoryName { get; set; }
        public string? ImagePathOne { get; set; }
        public string? ImagePathTwo { get; set; }
        public string? ImagePathThree { get; set; }
        public string FloorLocation {  get; set; }  
        public int RoomNumber {  get; set; }    
        public int BedNumber {  get; set; } 
        public int BathNumber {  get; set; }    
        public int MaxAccommodates {  get; set; }   
        public decimal Price { get; set; }
        public int? AirQuality { get; set; }
        public string? AirDescription { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<HousingReview> HousingReviews { get; set; } = new List<HousingReview>();
        public List<HousingFeatures> HousingFeatures { get; set; }
        public HousingDescriptions HousingDescriptions { get; set; }

        public List<Favorites> Favorites { get; set; }
        public Location Location { get; set; } 
        public Owner Owner { get; set; }
    }
}
