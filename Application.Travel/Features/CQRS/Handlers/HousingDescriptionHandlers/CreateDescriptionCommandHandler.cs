using Application.Travel.Features.CQRS.Commands.HousingDescriptionCommands;
using Application.Travel.Interfaces;
using AutoMapper;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.HousingDescriptionHandlers
{
    public class CreateDescriptionCommandHandler:IRequestHandler<CreateDescriptionCommand,Response<HousingDescriptions>>
    {
        private readonly IRepository<HousingDescriptions> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Housing> _HousingRepository;

        public CreateDescriptionCommandHandler(IRepository<HousingDescriptions> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IRepository<Housing> HousingRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor; 
            _HousingRepository = HousingRepository;
        }


        public async Task<Response<HousingDescriptions>> Handle(CreateDescriptionCommand request,CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var owners = await _HousingRepository.GetListAsync();
                var OwnerValues=owners.Where(x=>x.OwnerId.ToString()==userIdClaim).ToList();

                var newDescription=_mapper.Map<HousingDescriptions>(request);

                if (OwnerValues.Any(owner => owner.Id == newDescription.HousingId))
                {
                    await _repository.AddAsync(newDescription);
                    return Response<HousingDescriptions>.Success(newDescription);
                }
                else
                {
                    return Response<HousingDescriptions>.Fail("No such ad was found.");
                }
            }
            catch (Exception ex) { return Response<HousingDescriptions>.Fail($"Internal Server Error: {ex.Message}"); }
        }
    }
}
