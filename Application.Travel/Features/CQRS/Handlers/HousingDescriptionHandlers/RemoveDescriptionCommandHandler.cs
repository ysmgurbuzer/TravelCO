using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.HousingDescriptionCommands;
using Application.Travel.Interfaces;
using Application.Travel.Services;
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
    public class RemoveDescriptionCommandHandler : IRequestHandler<RemoveDescriptionCommand, Response<HousingDescriptions>>
    {
        private readonly IRepository<HousingDescriptions> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Housing> _HousingRepository;
        private readonly IUow _uow;
        public RemoveDescriptionCommandHandler(IRepository<HousingDescriptions> repository, IHttpContextAccessor httpContextAccessor, IRepository<Housing> HousingRepository,IUow uow)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _HousingRepository = HousingRepository;
            _uow = uow;
        }

        public async Task<Response<HousingDescriptions>> Handle(RemoveDescriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var owners = await _HousingRepository.GetListAsync();
                var OwnerValues = owners.Where(x => x.OwnerId.ToString() == userIdClaim).ToList();

                var objectId = request.HousingId;
                if (objectId != null)
                {
                    var ownedProperty = OwnerValues.FirstOrDefault(property => property.Id == objectId);
                    if (ownedProperty != null)
                    {
                        var RemoveId = await _repository.GetListAsync();
                        var matchingDescription = RemoveId.FirstOrDefault(x => x.HousingId == objectId);
                         _repository.Delete(matchingDescription);
                        await _uow.SaveChangeAsync();

                        return Response<HousingDescriptions>.Success("Housing description delete transaction is success");
                    }
                    else
                    {
                        return Response<HousingDescriptions>.Fail("You do not have permission to remove this housing description.");
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
