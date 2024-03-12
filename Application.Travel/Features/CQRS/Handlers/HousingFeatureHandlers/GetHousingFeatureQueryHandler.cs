using Application.Travel.Features.CQRS.Queries.HousingFeaturesQueries;
using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Results.HousingFeatureResults;
using Application.Travel.Features.CQRS.Results.UserResults;
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

namespace Application.Travel.Features.CQRS.Handlers.HousingFeatureHandlers
{
    public class GetHousingFeatureQueryHandler : IRequestHandler<GetHousingFeatureQuery, Response<List<GetHousingFeaturesQueryResult>>>
    {
        private readonly IRepository<HousingFeatures> _repository;
        private readonly IMapper _mapper;

        public GetHousingFeatureQueryHandler(IRepository<HousingFeatures> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetHousingFeaturesQueryResult>>> Handle(GetHousingFeatureQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _repository.GetListAsync();

                var result = _mapper.Map<List<GetHousingFeaturesQueryResult>>(values);

                return Response<List<GetHousingFeaturesQueryResult>>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<List<GetHousingFeaturesQueryResult>>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
