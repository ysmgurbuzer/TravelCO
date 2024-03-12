using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Results.FavoritesResults;
using AutoMapper;
using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Profiles
{
    public class FavoritesProfile:Profile
    {
        public FavoritesProfile()
        {
            CreateMap<Favorites, GetFavoritesByIdQueryResult>().ReverseMap();
            CreateMap<Favorites, GetFavoritesQueryResult>().ReverseMap();
            
        }

    }
}
