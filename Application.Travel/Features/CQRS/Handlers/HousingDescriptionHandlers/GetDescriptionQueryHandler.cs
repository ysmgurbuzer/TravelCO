using Application.Travel.Features.CQRS.Queries.HousingDescriptionQueries;
using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Results.HousingDescriptionResults;
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

namespace Application.Travel.Features.CQRS.Handlers.HousingDescriptionHandlers
{
    public class GetDescriptionQueryHandler : IRequestHandler<GetDescriptionQuery,Response<List<GetDescriptionQueryResult>>>
    {
        private readonly IRepository<HousingDescriptions> _repository;
        private readonly IMapper _mapper;

        public GetDescriptionQueryHandler(IRepository<HousingDescriptions> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetDescriptionQueryResult>>> Handle(GetDescriptionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _repository.GetListAsync();

                var result = _mapper.Map<List<GetDescriptionQueryResult>>(values);

                return Response<List<GetDescriptionQueryResult>>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<List<GetDescriptionQueryResult>>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
