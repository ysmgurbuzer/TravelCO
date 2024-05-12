using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.RouteCommands
{
    public class CreateRouteCommand : IRequest<Response<Routes>>
    {
        public List<RouteList> RoutesList { get; set; }
    }
    public class RouteList 
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
