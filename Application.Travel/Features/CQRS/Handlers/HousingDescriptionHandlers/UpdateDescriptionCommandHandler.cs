using Application.Travel.Features.CQRS.Commands.HousingCommands;
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
    public class UpdateDescriptionCommandHandler : IRequestHandler<UpdateDescriptionCommand, Response<HousingDescriptions>>
    {
        private readonly IRepository<HousingDescriptions> _repository;
        private readonly IMapper _mapper;
        private readonly IRepository<Housing> _housingRepository;
        private readonly IHttpContextAccessor _contextAccessor; 

        public UpdateDescriptionCommandHandler(IRepository<HousingDescriptions> repository, IMapper mapper, IRepository<Housing> housingRepository, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _housingRepository = housingRepository;
            _contextAccessor = contextAccessor; 
        }

        public async Task<Response<HousingDescriptions>> Handle(UpdateDescriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var owners = await _housingRepository.GetListAsync();
                var ownerValues = owners.Where(x => x.OwnerId.ToString() == userIdClaim).ToList();

                var objectId = request.Id;
                if (objectId != null)
                {
                    var unchangedHousing = await _repository.GetByIdAsync(objectId);
                    var ownedProperty = ownerValues.FirstOrDefault(property => property.Id == unchangedHousing.HousingId);
                    if (ownedProperty != null)
                    {
                       

                        if (unchangedHousing == null)
                        {
                            return Response<HousingDescriptions>.Fail("Housing not found");
                        }

                      
                        if (ownerValues.Any(property => property.Id == unchangedHousing.HousingId))
                        {
                            var updatedHousing = _mapper.Map(request.Command, unchangedHousing);
                            updatedHousing.Id = unchangedHousing.Id;
                             _repository.Update(updatedHousing, unchangedHousing);

                            return Response<HousingDescriptions>.Success("Housing description update transaction is success");
                        }
                        else
                        {
                            return Response<HousingDescriptions>.Fail("You do not have permission to update this housing description.");
                        }
                    }
                    else
                    {
                        return Response<HousingDescriptions>.Fail("Invalid ObjectId provided");
                    }
                }
                else
                {
                    return Response<HousingDescriptions>.Fail("Invalid ObjectId provided");
                }
            }
            catch (Exception ex)
            {
                return Response<HousingDescriptions>.Fail($"Internal Server Error: {ex.Message}");
            }
        }

    }
}
