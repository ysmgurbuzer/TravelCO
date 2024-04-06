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
    public class GetHousingFeatureByIdQueryHandler : IRequestHandler<GetHousingFeatureByIdQuery, Response<List<GetHousingFeatureByIdQueryResult>>>
    {
        private readonly IRepository<HousingFeatures> _repository;
        private readonly IMapper _mapper;

        public GetHousingFeatureByIdQueryHandler(IRepository<HousingFeatures> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetHousingFeatureByIdQueryResult>>> Handle(GetHousingFeatureByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var objectId = request.HousingId;
                if (objectId != null)
                {
                    var values = _repository.GetList(x => x.HousingId == objectId);
                    if (values != null)
                    {

                        var result = _mapper.Map<List<GetHousingFeatureByIdQueryResult>>(values);

                        return Response<List<GetHousingFeatureByIdQueryResult>>.Success(result);
                    }
                    else
                    {
                        return Response<List<GetHousingFeatureByIdQueryResult>>.Fail($"Housing feature not found with ID: {request.HousingId}");
                    }
                }
                else
                {
                    return Response<List<GetHousingFeatureByIdQueryResult>>.Fail($"Invalid ID format: {request.HousingId}");
                }
            }
            catch (Exception ex)
            {
                return Response<List<GetHousingFeatureByIdQueryResult>>.Fail($"Internal Server Error: {ex.Message}");
            }
        }



            
    }
}
