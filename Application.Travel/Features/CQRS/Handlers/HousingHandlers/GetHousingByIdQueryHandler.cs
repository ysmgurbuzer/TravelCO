using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Results.HousingResults;
using Application.Travel.Interfaces;
using AutoMapper;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.HousingHandlers
{
    public class GetHousingByIdQueryHandler : IRequestHandler<GetHousingByIdQuery, Response<GetHousingByIdQueryResult>>
    {
        private readonly IRepository<Housing> _repository;
        private readonly IRepository<Location> _locRepository;
        private readonly IMapper _mapper;
        

        public GetHousingByIdQueryHandler(IRepository<Housing> repository,IMapper mapper, IRepository<Location> locRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _locRepository = locRepository;
        }

        public async Task<Response<GetHousingByIdQueryResult>> Handle(GetHousingByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
              
                if (request.Id != null)
                {
                    var values = await _repository.GetByIdAsync(request.Id);
                    
                    if (values != null)
                    {
                        
                        var result= _mapper.Map<GetHousingByIdQueryResult>(values);
                        var location = await _locRepository.GetByIdAsync(values.LocationId);

                        if (location != null)
                        {
               
                            result.Location = location;
                            return Response<GetHousingByIdQueryResult>.Success(result);
                        }
                        else
                        {
                            return Response<GetHousingByIdQueryResult>.Fail($"Location not found with ID: {values.LocationId}");
                        }
                   
                    }
                    else
                    {
                        return Response<GetHousingByIdQueryResult>.Fail($"Housing not found with ID: {request.Id}");
                    }
                }
                else
                {
                    return Response<GetHousingByIdQueryResult>.Fail($"Invalid ID format: {request.Id}");
                }
            }
            catch (Exception ex)
            {
                return Response<GetHousingByIdQueryResult>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
