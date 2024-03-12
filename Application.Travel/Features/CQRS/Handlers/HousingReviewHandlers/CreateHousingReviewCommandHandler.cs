using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.HousingReviewCommands;
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

namespace Application.Travel.Features.CQRS.Handlers.HousingReviewHandlers
{
    public class CreateHousingReviewCommandHandler : IRequestHandler<CreateHousingReviewCommand, Response<HousingReview>>
    {
        private readonly IRepository<HousingReview> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Housing> _HousingRepository;
        public CreateHousingReviewCommandHandler(IRepository<HousingReview> repository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IRepository<Housing> housingRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _HousingRepository = housingRepository;
        }
        //reservationa göre bakılacak 

        public async Task<Response<HousingReview>> Handle(CreateHousingReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var owners = await _HousingRepository.GetListAsync();
                var OwnerValues = owners.Where(x => x.OwnerId.ToString() == userIdClaim).ToList();

                var newDescription = _mapper.Map<HousingReview>(request);

                if (OwnerValues.Any(owner => owner.Id == newDescription.HousingId))
                {
                    await _repository.AddAsync(newDescription);
                    return Response<HousingReview>.Success(newDescription);
                }
                else
                {
                    return Response<HousingReview>.Fail("No such ad was found.");
                }
            }
            catch (Exception ex) { return Response<HousingReview>.Fail($"Internal Server Error: {ex.Message}"); }
        }
    }
}
