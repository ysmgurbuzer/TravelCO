 using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Interfaces;
using AutoMapper;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.HousingHandlers
{
    public class UpdateHousingCommandHandler : IRequestHandler<UpdateHousingCommand, Response<Housing>>
    {
        private readonly IRepository<Housing> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor; 

        public UpdateHousingCommandHandler(IRepository<Housing> repository,IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<Response<Housing>> Handle(UpdateHousingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRoleClaim=_contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(userIdClaim) && userRoleClaim=="Owner" )
                {
                    var unchangedHousing = await _repository.GetByIdAsync(request.Id);
                    if (unchangedHousing == null ) { return Response<Housing>.Fail("Housing not found"); }


                    if (unchangedHousing.OwnerId.ToString() == userIdClaim)
                    {
                        var updatedHousing = _mapper.Map<Housing>(request.Command);
                       
                         _repository.Update(updatedHousing, unchangedHousing);

                        return Response<Housing>.Success(updatedHousing);
                    }
                    else
                    {
                        return Response<Housing>.Unauthorized("Unauthorized. You are not the owner of this housing.");
                    }
                }
                else
                {
                    return Response<Housing>.Unauthorized("Unauthorized. Only owners can update housing.");
                }
            }
            catch (Exception ex)
            {
                return Response<Housing>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
