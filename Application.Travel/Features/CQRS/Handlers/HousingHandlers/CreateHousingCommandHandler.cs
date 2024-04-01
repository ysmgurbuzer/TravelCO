using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.RoleCommands;
using Application.Travel.Interfaces;
using Application.Travel.Services;
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
        private readonly IUow _uow;
        public CreateHousingCommandHandler(IRepository<Housing> repository, 
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IRepository<User> userRepository,
            IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _UserRepository = userRepository;   
            _uow = uow;
        }

        public async Task<Response<Housing>> Handle(CreateHousingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (userIdClaim==null)
                {
                    return Response<Housing>.Fail("User information not found.");
                }

                var newHousing = _mapper.Map<Housing>(request);
                newHousing.OwnerId = userIdClaim;
                newHousing.CategoryName = ((CategoryTypes.Category)request.CategoryId);

              var data=  await _repository.AddAsync(newHousing);
                data.Id = 0;
                await UpdateUserRole();
                await _uow.SaveChangeAsync();

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
            var userIdClaim = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userRoleClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (userIdClaim!=null && !string.IsNullOrEmpty(userRoleClaim))
            {
               
                var user = await _UserRepository.GetByIdAsync(userIdClaim);

                var a = (int)RoleTypes.User;
                if (int.Parse(userRoleClaim) == a)
                {
                    
                    user.RoleId = (int)RoleTypes.Owner;
                }

               
                 _UserRepository.Update(user,user);

            }
        }
    }
}
