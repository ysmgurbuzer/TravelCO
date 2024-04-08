﻿
using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Results.HousingResults
{
    public class GetHousingByIdQueryResult
    {
        public int Id { get; set; }
        public string HouseTitle { get; set; }
        public int LocationId { get; set; }
        public int OwnerId { get; set; }
        public string CategoryName { get; set; }
        public string? ImagePathOne { get; set; }
        public string? ImagePathTwo { get; set; }
        public string? ImagePathThree { get; set; }
        public string FloorLocation { get; set; }
        public int RoomNumber { get; set; }
        public int BedNumber { get; set; }
        public int BathNumber { get; set; }
        public int MaxAccommodates { get; set; }
        public decimal Price { get; set; }
        public int? AirQuality { get; set; }
        public string? AirDescription { get; set; }

        [JsonIgnore]
        public Location Location { get; set; }
    }
}
