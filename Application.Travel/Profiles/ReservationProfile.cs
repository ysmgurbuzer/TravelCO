using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.ReservationCommands;
using Application.Travel.Features.CQRS.Results.ReservationResults;
using AutoMapper;
using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Profiles
{
    public class ReservationProfile:Profile
    {
        public ReservationProfile()
        {
            CreateMap<CreateReservationCommand, Reservation>().ReverseMap();
            CreateMap<GetReservationWithLocationQueryResult, List<Reservation>>().ReverseMap();
            CreateMap<GetReservationWithLocationQueryResult, Reservation>().ReverseMap();
            CreateMap<Location, GetReservationWithLocationQueryResult>()
     .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
     .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
     .ForMember(dest => dest.longitude, opt => opt.MapFrom(src => src.longitude))
     .ForMember(dest => dest.latitude, opt => opt.MapFrom(src => src.latitude));

            CreateMap<GetReservationWithLocationQueryResult, Location>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.longitude, opt => opt.MapFrom(src => src.longitude))
                .ForMember(dest => dest.latitude, opt => opt.MapFrom(src => src.latitude));

            CreateMap<GetReservationQueryResult, Reservation>().ReverseMap();
        }
    }
}
