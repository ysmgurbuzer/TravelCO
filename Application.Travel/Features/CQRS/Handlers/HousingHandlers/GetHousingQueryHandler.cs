using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Results.HousingResults;
using Application.Travel.Features.CQRS.Results.UserResults;
using Application.Travel.Interfaces;
using AutoMapper;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.HousingHandlers
{
    public class GetHousingQueryHandler : IRequestHandler<GetHousingQuery, Response<List<GetHousingQueryResult>>>
    {
        private readonly IRepository<Housing> _repository;
        private readonly IMapper _mapper;

        public GetHousingQueryHandler(IRepository<Housing> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetHousingQueryResult>>> Handle(GetHousingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _repository.GetListAsync();
                
                var result = _mapper.Map<List<GetHousingQueryResult>>(values);

                return Response<List<GetHousingQueryResult>>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<List<GetHousingQueryResult>>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
