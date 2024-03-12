using Application.Travel.Features.CQRS.Queries.HousingFeaturesQueries;
using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Results.HousingFeatureResults;
using Application.Travel.Features.CQRS.Results.HousingResults;
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
    public class GetHousingFeatureByIdQueryHandler : IRequestHandler<GetHousingFeatureByIdQuery, Response<GetHousingFeatureByIdQueryResult>>
    {
        private readonly IRepository<HousingFeatures> _repository;
        private readonly IMapper _mapper;

        public GetHousingFeatureByIdQueryHandler(IRepository<HousingFeatures> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<GetHousingFeatureByIdQueryResult>> Handle(GetHousingFeatureByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var objectId = request.Id;
                if (objectId != null)
                {
                    var values = await _repository.GetByIdAsync(objectId);
                    if (values != null)
                    {

                        var result = _mapper.Map<GetHousingFeatureByIdQueryResult>(values);

                        return Response<GetHousingFeatureByIdQueryResult>.Success(result);
                    }
                    else
                    {
                        return Response<GetHousingFeatureByIdQueryResult>.Fail($"Housing feature not found with ID: {request.Id}");
                    }
                }
                else
                {
                    return Response<GetHousingFeatureByIdQueryResult>.Fail($"Invalid ID format: {request.Id}");
                }
            }
            catch (Exception ex)
            {
                return Response<GetHousingFeatureByIdQueryResult>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
