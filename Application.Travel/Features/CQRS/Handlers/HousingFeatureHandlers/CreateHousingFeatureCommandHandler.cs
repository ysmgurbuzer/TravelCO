﻿ using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.HousingFeatureCommands;
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

namespace Application.Travel.Features.CQRS.Handlers.HousingFeatureHandlers
{
    public class CreateHousingFeatureCommandHandler : IRequestHandler<CreateHousingFeatureCommand, Response<HousingFeatures>>
    {
        private readonly IRepository<HousingFeatures> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Housing> _HousingRepository;

        public CreateHousingFeatureCommandHandler(IRepository<HousingFeatures> repository, 
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IRepository<Housing> housingRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _HousingRepository = housingRepository; 
        }

        public async Task<Response<HousingFeatures>> Handle(CreateHousingFeatureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var owners = await _HousingRepository.GetListAsync();
                var OwnerValues = owners.Where(x => x.OwnerId.ToString() == userIdClaim).ToList();

                var newHousing = _mapper.Map<HousingFeatures>(request);

               
                if (OwnerValues.Any(owner => owner.Id == newHousing.HousingId))
                {
                    await _repository.AddAsync(newHousing);

                    return Response<HousingFeatures>.Success(newHousing);
                }
                else
                {
                    return Response<HousingFeatures>.Fail("No such ad was found.");
                }
            }
            catch (ValidateException ex)
            {
                return Response<HousingFeatures>.Fail($"Validation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<HousingFeatures>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
