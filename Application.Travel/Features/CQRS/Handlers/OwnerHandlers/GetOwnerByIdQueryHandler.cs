using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Queries.OwnerQueries;
using Application.Travel.Features.CQRS.Results.HousingResults;
using Application.Travel.Features.CQRS.Results.OwnerResults;
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
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, Response<GetOwnerByIdQueryResult>>
    {
        private readonly IRepository<Owner> _repository;
        private readonly IMapper _mapper;

        public GetOwnerByIdQueryHandler(IRepository<Owner> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<GetOwnerByIdQueryResult>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
            
                if (request.Id != null)
                {
                    var values = await _repository.GetByIdAsync(request.Id);
                    if (values != null)
                    {

                        var result = _mapper.Map<GetOwnerByIdQueryResult>(values);

                        return Response<GetOwnerByIdQueryResult>.Success(result);
                    }
                    else
                    {
                        return Response<GetOwnerByIdQueryResult>.Fail($"Housing not found with ID: {request.Id}");
                    }
                }
                else
                {
                    return Response<GetOwnerByIdQueryResult>.Fail($"Invalid ID format: {request.Id}");
                }
            }
            catch (Exception ex)
            {
                return Response<GetOwnerByIdQueryResult>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
