using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Results.HousingResults;
using Application.Travel.Features.CQRS.Results.UserResults;
using Application.Travel.Interfaces;
using AutoMapper;
using Domain.Travel.Entities;
using Domain.Travel.Enums;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.HousingHandlers
{
    public class GetHousingByOwnerQueryHandler : IRequestHandler<GetHousingByOwnerQuery, Response<List<GetHousingByOwnerResult>>>
    {
        private readonly IRepository<Housing> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetHousingByOwnerQueryHandler(IRepository<Housing> repository, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
                _contextAccessor = contextAccessor;
               _repository = repository;   
               _mapper = mapper;   
        }

        //ilanlarım sayfası
        public async Task<Response<List<GetHousingByOwnerResult>>> Handle(GetHousingByOwnerQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var userIdClaim = int.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var userRoleClaim = _contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Role)?.Value;

                var roleType = (int)RoleTypes.Owner;
                if (userIdClaim!=null && userRoleClaim == roleType.ToString())
                {
                    Expression<Func<Housing, bool>> filter = h => h.OwnerId == userIdClaim;
                    var values = _repository.GetList(filter);

                    var result = _mapper.Map<List<GetHousingByOwnerResult>>(values);

                    return Response<List<GetHousingByOwnerResult>>.Success(result);
                }
                else
                {
                    return Response<List<GetHousingByOwnerResult>>.Fail("Herhangi bir ilanınız yok");
                }
            }
            catch (Exception ex)
            {
                return Response<List<GetHousingByOwnerResult>>.Fail($"Internal Server Error: {ex.Message}");
            }
        }

   
    }
}
