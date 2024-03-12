
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.HousingCommands
{
    public class UpdateHousingCommand:IRequest<Response<Housing>>   
    {
        public int Id { get; set; }
        public HousingCommand Command { get; set; }
        
    }
    public class HousingCommand
    {
        public int LocationId { get; set; }
        public int CategoryId { get; set; }
        public int OwnerId { get; set; }
        public string? ImagePathOne { get; set; }
        public string? ImagePathTwo { get; set; }
        public string? ImagePathThree { get; set; }
        public string FloorLocation { get; set; }
        public int RoomNumber { get; set; }
        public int BedNumber { get; set; }
        public int BathNumber { get; set; }
        public int MaxAccommodates { get; set; }
        public decimal Price { get; set; }
    }
}
