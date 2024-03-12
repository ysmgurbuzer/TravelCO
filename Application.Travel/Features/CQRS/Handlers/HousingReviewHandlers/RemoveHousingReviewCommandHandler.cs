using Application.Travel.Features.CQRS.Commands.HousingDescriptionCommands;
using Application.Travel.Features.CQRS.Commands.HousingReviewCommands;
using Application.Travel.Interfaces;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.HousingReviewHandlers
{
    public class RemoveHousingReviewCommandHandler 
    {

        private readonly IRepository<HousingReview> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Housing> _HousingRepository;
        public RemoveHousingReviewCommandHandler(IRepository<HousingReview> repository, IHttpContextAccessor httpContextAccessor, IRepository<Housing> HousingRepository)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _HousingRepository = HousingRepository;
        }

      
    }
}
