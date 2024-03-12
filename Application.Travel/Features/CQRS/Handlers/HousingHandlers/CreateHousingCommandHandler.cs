using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.RoleCommands;
using Application.Travel.Interfaces;
using AutoMapper;
using Domain.Travel.Entities;
using Domain.Travel.Enums;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.AspNetCore.Http;

using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.HousingHandlers
{
    public class CreateHousingCommandHandler : IRequestHandler<CreateHousingCommand, Response<Housing>>
    {
        private readonly IRepository<Housing> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _UserRepository;
        public CreateHousingCommandHandler(IRepository<Housing> repository, 
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IRepository<User> userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _UserRepository = userRepository;   
        }

        public async Task<Response<Housing>> Handle(CreateHousingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userIdClaim!=null)
                {
                    return Response<Housing>.Fail("User information not found.");
                }

                var newHousing = _mapper.Map<Housing>(request);
                newHousing.OwnerId = userIdClaim;
                newHousing.CategoryName = ((CategoryTypes.Category)request.CategoryId);

                await _repository.AddAsync(newHousing);
                await UpdateUserRole();

                return Response<Housing>.Success(newHousing);
            }
            catch (ValidateException ex)
            {
                return Response<Housing>.Fail($"Validation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<Housing>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
        private async Task UpdateUserRole()
        {
            var userIdClaim = Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userRoleClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (userIdClaim!=null && !string.IsNullOrEmpty(userRoleClaim))
            {
               
                var user = await _UserRepository.GetByIdAsync(userIdClaim);

                
                if (userRoleClaim == "User")
                {
                    
                    user.RoleId = Int32.Parse(RoleTypes.Owner.ToString());
                }

               
                 _UserRepository.Update(user,user);
            }
        }
    }
}
