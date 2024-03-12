
using Application.Travel.Features.CQRS.Queries.OwnerQueries;
using Application.Travel.Features.CQRS.Results.OwnerResults;
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

namespace Application.Travel.Features.CQRS.Handlers.OwnerHandlers
{
    public class GetOwnerQueryHandler:IRequestHandler<GetOwnerQuery,Response<List<GetOwnerQueryResult>>>
    {
        private readonly IRepository<Owner> _repository;
        private readonly IMapper _mapper;

        public GetOwnerQueryHandler(IRepository<Owner> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<List<GetOwnerQueryResult>>> Handle(GetOwnerQuery query,CancellationToken cancellationToken)
        {
            try
            {
                var values = await _repository.GetListAsync();

                var result = _mapper.Map<List<GetOwnerQueryResult>>(values);

                return Response<List<GetOwnerQueryResult>>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<List<GetOwnerQueryResult>>.Fail($"Internal Server Error: {ex.Message}");
            }

        }
    }
}
