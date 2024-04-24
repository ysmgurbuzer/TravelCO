using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Interfaces;
using Application.Travel.Services;
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
    public class RemoveHousingCommandHandler : IRequestHandler<RemoveHousingCommand, Response<Housing>>
    {
        private readonly IRepository<Housing> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUow _uow;

        public RemoveHousingCommandHandler(IRepository<Housing> repository, IHttpContextAccessor httpContextAccessor,IUow uow)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _uow = uow;
        }

        public async Task<Response<Housing>> Handle(RemoveHousingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRoleClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || userRoleClaim != "Owner")
                {
                    return Response<Housing>.Unauthorized("Unauthorized. Only owners can remove housing.");
                }

                var housing = await _repository.GetByIdAsync(request.Id);

                if (housing == null)
                {
                    return Response<Housing>.Fail("Housing not found");
                }

                if (housing.OwnerId.ToString() != userIdClaim)
                {
                    return Response<Housing>.Unauthorized("Unauthorized. You are not the owner of this housing.");
                }

                 _repository.Delete(housing);
                await _uow.SaveChangeAsync();
                return Response<Housing>.Success("Housing removed successfully.");
            }
            catch (Exception ex)
            {
                return Response<Housing>.Fail($"Error: {ex.Message}");
            }
        }
    }
}
