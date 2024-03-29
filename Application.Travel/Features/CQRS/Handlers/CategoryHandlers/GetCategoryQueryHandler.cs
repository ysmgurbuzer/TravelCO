using Application.Travel.Features.CQRS.Queries.CategoryQueries;
using Application.Travel.Features.CQRS.Queries.FavoritesQueries;
using Application.Travel.Features.CQRS.Results.CategoryResults;
using Application.Travel.Features.CQRS.Results.FavoritesResults;
using Application.Travel.Interfaces;
using AutoMapper;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Travel.Enums.CategoryTypes;

namespace Application.Travel.Features.CQRS.Handlers.CategoryHandlers
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Response<List<GetCategoryQueryResult>>>
    {
        private readonly IRepository<Categories> _repository;
        private readonly IMapper _mapper;
     
        public GetCategoryQueryHandler(IRepository<Categories> repository, IMapper mapper)
        {
                _mapper = mapper;
            _repository = repository;   
        }
        public async Task<Response<List<GetCategoryQueryResult>>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categoryValues = Enum.GetValues(typeof(Category)).Cast<Category>().ToList();
                var mappedCategory = categoryValues.Select(category => new GetCategoryQueryResult
                {
                    Id = (int)category, 
                    CategoryName = category.ToString() 
                }).ToList();
                return Response<List<GetCategoryQueryResult>>.Success(mappedCategory);
            }
            catch (Exception ex)
            {
                return Response<List<GetCategoryQueryResult>>.Fail($"Error: {ex.Message}");
            }
        }
    }
}
