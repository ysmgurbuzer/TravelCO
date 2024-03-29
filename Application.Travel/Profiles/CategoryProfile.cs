using Application.Travel.Features.CQRS.Results.CategoryResults;
using Application.Travel.Features.CQRS.Results.FavoritesResults;
using AutoMapper;
using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Travel.Enums.CategoryTypes;

namespace Application.Travel.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryQueryResult>().ReverseMap();

            CreateMap<Category, GetCategoryByIdQueryResult>().ReverseMap();
            
        }
    }
}
