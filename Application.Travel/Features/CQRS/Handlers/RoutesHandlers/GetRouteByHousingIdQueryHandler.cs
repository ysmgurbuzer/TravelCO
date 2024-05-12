using Application.Travel.Features.CQRS.Queries.ReservationQueries;
using Application.Travel.Features.CQRS.Queries.RouteQueries;
using Application.Travel.Features.CQRS.Results.ReservationResults;
using Application.Travel.Features.CQRS.Results.RouteResults;
using Application.Travel.Interfaces;
using AutoMapper;
using Braintree;
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

namespace Application.Travel.Features.CQRS.Handlers.RoutesHandlers
{
    public class GetRouteByHousingIdQueryHandler : IRequestHandler<GetRouteByHousingIdQuery, Response<List<GetRouteByHousingIdQueryResult>>>
    {
        private readonly IRepository<Routes> _routesRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetRouteByHousingIdQueryHandler(IRepository<Routes> routesRepo, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _routesRepo = routesRepo;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;   
        }


        public async Task<Response<List<GetRouteByHousingIdQueryResult>>> Handle(GetRouteByHousingIdQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Response<List<GetRouteByHousingIdQueryResult>>.Unauthorized("Unauthorized. ");
            }
            if (request == null)
            {
                return Response<List<GetRouteByHousingIdQueryResult>>.Fail("Housing Boş");
            }

            var values =  _routesRepo.GetList(item=>item.HousingId==request.HousingId && item.User_ID.ToString()==userIdClaim);
            if (values == null)
            {
                return Response<List<GetRouteByHousingIdQueryResult>>.Fail("İlgili kullanıcı veya eve ait rota bilgisi bulunamadı");
            }
            else
            {
                var result = _mapper.Map<List<GetRouteByHousingIdQueryResult>>(values);
                return Response<List<GetRouteByHousingIdQueryResult>>.Success(result);
            }
            
        }
    }
}
