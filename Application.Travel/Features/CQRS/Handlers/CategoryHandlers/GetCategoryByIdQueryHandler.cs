using Application.Travel.Features.CQRS.Queries.CategoryQueries;
using Application.Travel.Features.CQRS.Queries.FavoritesQueries;
using Application.Travel.Features.CQRS.Results.CategoryResults;
using Application.Travel.Features.CQRS.Results.FavoritesResults;
using Application.Travel.Interfaces;
using AutoMapper;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Travel.Enums.CategoryTypes;

namespace Application.Travel.Features.CQRS.Handlers.CategoryHandlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response<GetCategoryByIdQueryResult>>
    {
        private readonly IRepository<Categories> _repository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IRepository<Categories> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Response<GetCategoryByIdQueryResult>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id != null)
                {
                    var category = (Category)request.Id;
                    var mappedCategory = new GetCategoryByIdQueryResult
                    {
                        Id = (int)category,
                        CategoryName = category.ToString()
                    };
                
                    return Response<GetCategoryByIdQueryResult>.Success(mappedCategory);

                }
                else
                {
                    return Response<GetCategoryByIdQueryResult>.Fail("Favorites not found");
                }

            }
            catch (Exception ex)
            {
                return Response<GetCategoryByIdQueryResult>.Fail($"Error: {ex.Message}");
            }

        }
    }
}
