using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.HousingDescriptionCommands;
using Application.Travel.Features.CQRS.Commands.HousingFeatureCommands;
using Application.Travel.Features.CQRS.Results.HousingDescriptionResults;
using Application.Travel.Features.CQRS.Results.HousingFeatureResults;
using Application.Travel.Features.CQRS.Results.HousingResults;
using Application.Travel.Features.CQRS.Results.RouteResults;
using Application.Travel.Features.CQRS.Results.UserResults;
using AutoMapper;
using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Profiles
{
    public class HousingProfile:Profile
    {
        public HousingProfile()
        {
            CreateMap<Housing, CreateHousingCommand>().ReverseMap();
            
            CreateMap<Housing, GetHousingByIdQueryResult>().ReverseMap();
            
            CreateMap<Housing, GetHousingQueryResult>().ReverseMap();
            CreateMap<UpdateHousingCommand, Housing>().ReverseMap();
            CreateMap<HousingCommand, Housing>().ReverseMap();
            CreateMap<User, GetCheckUserResult>().ReverseMap();
            CreateMap<CreateDescriptionCommand, HousingDescriptions>().ReverseMap();
            CreateMap<DescriptionCommand, HousingDescriptions>().ReverseMap();
            CreateMap<HousingDescriptions, GetDescriptionByIdQueryResult>().ReverseMap();
            CreateMap<GetDescriptionQueryResult, HousingDescriptions>().ReverseMap();
          
            CreateMap<GetHousingByOwnerResult, Housing>().ReverseMap();
            CreateMap<CreateHousingFeatureCommand, HousingFeatures>().ReverseMap(); 
                CreateMap<GetHousingFeaturesQueryResult, HousingFeatures>().ReverseMap();

            CreateMap<FeatureCommand, HousingFeatures>().ReverseMap();
               
            CreateMap<HousingFeatures, GetHousingFeatureByIdQueryResult>().ReverseMap();

            CreateMap<HousingFeatures, List<GetHousingFeatureByIdQueryResult>>().ReverseMap();
            
            CreateMap<Housing, GetHousingByCategoryQueryResult>().ReverseMap();
            CreateMap<Routes, GetRouteByHousingIdQueryResult>().ReverseMap();
        }
    }
}
