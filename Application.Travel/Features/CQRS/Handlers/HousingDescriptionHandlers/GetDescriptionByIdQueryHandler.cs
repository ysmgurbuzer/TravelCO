
using Application.Travel.Features.CQRS.Queries.HousingDescriptionQueries;
using Application.Travel.Features.CQRS.Results.HousingDescriptionResults;
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

namespace Application.Travel.Features.CQRS.Handlers.HousingDescriptionHandlers
{
    public class GetDescriptionByIdQueryHandler:IRequestHandler<GetDescriptionByIdQuery,Response<GetDescriptionByIdQueryResult>>
    {
        private readonly IRepository<HousingDescriptions> _repository;
        private readonly IMapper _mapper;

        public GetDescriptionByIdQueryHandler(IRepository<HousingDescriptions> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<GetDescriptionByIdQueryResult>> Handle(GetDescriptionByIdQuery query ,CancellationToken cancellationToken)
        {
            try
            {
                var housingIdObjectId = query.HousingId;
                if (housingIdObjectId != null)
                {
                    var values =  _repository.GetList(x => x.HousingId == housingIdObjectId);
                    if (values!=null) 
                    {
                        var result =_mapper.Map<GetDescriptionByIdQueryResult>(values);
                        return Response<GetDescriptionByIdQueryResult>.Success(result);
                    }
                    else
                    {
                        return Response<GetDescriptionByIdQueryResult>.Fail($"Housing Description not found with ID: {query.HousingId}");
                    }
                }
                else
                {
                    return Response<GetDescriptionByIdQueryResult>.Fail($"Invalid ID format: {query.HousingId}");
                }
            }
            catch (Exception ex) { return Response<GetDescriptionByIdQueryResult>.Fail($"Internal Server Error: {ex.Message}"); }
        }
    }
}
