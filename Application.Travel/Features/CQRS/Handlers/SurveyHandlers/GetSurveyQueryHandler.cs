using Application.Travel.Features.CQRS.Queries.ReservationQueries;
using Application.Travel.Features.CQRS.Queries.SurveyQueries;
using Application.Travel.Features.CQRS.Results.ReservationResults;
using Application.Travel.Features.CQRS.Results.SurveyResults;
using Application.Travel.Interfaces;
using AutoMapper;
using Braintree;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.SurveyHandlers
{
    public class GetSurveyQueryHandler : IRequestHandler<GetSurveyQuery, Response<GetSurveyQueryResult>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Survey> _surveyRepository;
        private readonly IMapper _mapper;
        public GetSurveyQueryHandler(IHttpContextAccessor httpContextAccessor, IRepository<Survey> surveyRepository, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _surveyRepository = surveyRepository;
            _mapper = mapper;
                
        }

        public async Task<Response<GetSurveyQueryResult>> Handle(GetSurveyQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var value =  _surveyRepository.GetByFilter(x=>x.UserId==userIdClaim);

            if (userIdClaim==null)
            {
                return Response<GetSurveyQueryResult>.Fail("User information not found.");
            }
            if (value == null)
            {
                return Response<GetSurveyQueryResult>.Fail("Survey not found.");
            }

            var result = _mapper.Map<GetSurveyQueryResult>(value);
            return Response<GetSurveyQueryResult>.Success(result);
        }
    }
}
