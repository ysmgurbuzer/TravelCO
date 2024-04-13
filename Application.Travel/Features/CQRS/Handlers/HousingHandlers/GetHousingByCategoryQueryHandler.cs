using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Results.HousingResults;
using Application.Travel.Interfaces;
using AutoMapper;
using Domain.Travel.Entities;
using Domain.Travel.Enums;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Travel.Enums.CategoryTypes;

namespace Application.Travel.Features.CQRS.Handlers.HousingHandlers
{
    public class GetHousingByCategoryQueryHandler : IRequestHandler<GetHousingByCategoryQuery, Response<List<GetHousingByCategoryQueryResult>>>
    {
        private readonly IRepository<Housing> _repository;
        private readonly IRepository<Location> _locRepository;
        private readonly IMapper _mapper;


        public GetHousingByCategoryQueryHandler(IRepository<Housing> repository, IMapper mapper, IRepository<Location> locRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _locRepository = locRepository;
        }

        public async Task<Response<List<GetHousingByCategoryQueryResult>>> Handle(GetHousingByCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CategoryId == null)
                    return Response<List<GetHousingByCategoryQueryResult>>.Fail($"Invalid ID format: {request.CategoryId}");

                var category = (Category)request.CategoryId;
                if (category==null)
                    return Response<List<GetHousingByCategoryQueryResult>>.Fail($"Invalid category: {request.CategoryId}");

                var values = _repository.GetList(x => x.CategoryName == category);
                if (values == null)
                    return Response<List<GetHousingByCategoryQueryResult>>.Fail($"Housing not found with ID: {request.CategoryId}");

                var result = _mapper.Map<List<GetHousingByCategoryQueryResult>>(values);
                return Response<List<GetHousingByCategoryQueryResult>>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<List<GetHousingByCategoryQueryResult>>.Fail($"Internal Server Error: {ex.Message}");
            }
        }

    }
}

