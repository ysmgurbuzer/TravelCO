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
        private readonly IRepository<Location> _repositoryLoc;
        public GetHousingQueryHandler(IRepository<Housing> repository,IMapper mapper, IRepository<Location> repositoryLoc)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryLoc = repositoryLoc; 
        }

        public async Task<Response<List<GetHousingQueryResult>>> Handle(GetHousingQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var values = await _repository.GetListAsync();
                var results = new List<GetHousingQueryResult>();
                foreach (var value in values)
                {
                    var Location = await _repositoryLoc.GetByIdAsync(value.LocationId);
                    var result1 = _mapper.Map<GetHousingQueryResult>(value);
                    if (Location != null)
                        result1.LocationCity = Location.City;
                    result1.LocationCountry = Location.Country;

                    results.Add(result1);
                }
                var result = _mapper.Map<List<GetHousingQueryResult>>(results);

                return Response<List<GetHousingQueryResult>>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<List<GetHousingQueryResult>>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
