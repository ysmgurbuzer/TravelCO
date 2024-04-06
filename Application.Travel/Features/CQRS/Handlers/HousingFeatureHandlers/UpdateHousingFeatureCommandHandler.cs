using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.HousingFeatureCommands;
using Application.Travel.Interfaces;
using Application.Travel.Services;
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
    public class UpdateHousingFeatureCommandHandler : IRequestHandler<UpdateHousingFeatureCommand, Response<HousingFeatures>>
    {
        private readonly IRepository<HousingFeatures> _repository;
        private readonly IMapper _mapper;
        private readonly IRepository<Housing> _housingRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUow _uow;
        public UpdateHousingFeatureCommandHandler(IRepository<HousingFeatures> repository, 
            IMapper mapper,
            IRepository<Housing> housingRepository,
            IHttpContextAccessor contextAccessor,
            IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _housingRepository = housingRepository;
            _contextAccessor = contextAccessor;
            _uow = uow;
        }

        public async Task<Response<HousingFeatures>> Handle(UpdateHousingFeatureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var owners = await _housingRepository.GetListAsync();
                var ownerValues = owners.Where(x => x.OwnerId.ToString() == userIdClaim).ToList();

               
                if (request.Id !=null)
                {
                    var unchangedHousing = await _repository.GetByIdAsync(request.Id);
                    var ownedProperty = ownerValues.FirstOrDefault(property => property.Id == unchangedHousing.HousingId);
                    if (ownedProperty != null)
                    {

                        if (unchangedHousing == null)
                        {
                            return Response<HousingFeatures>.Fail("Housing feature not found");
                        }
                            var updatedHousing = _mapper.Map(request.Command, unchangedHousing);
                            updatedHousing.Id = unchangedHousing.Id;
                             _repository.Update(updatedHousing, unchangedHousing);
                            await _uow.SaveChangeAsync();
                            return Response<HousingFeatures>.Success("Housing feature update transaction is success");
                       
                    }

                    return Response<HousingFeatures>.Success("Housing feature update transaction is success");
                }
                else
                {
                    return Response<HousingFeatures>.Fail("Invalid ObjectId provided");
                }
            }
            catch (Exception ex)
            {
                return Response<HousingFeatures>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
