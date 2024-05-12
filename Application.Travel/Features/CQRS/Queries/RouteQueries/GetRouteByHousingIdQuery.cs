using Application.Travel.Features.CQRS.Results.ReservationResults;
using Application.Travel.Features.CQRS.Results.RouteResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Queries.RouteQueries
{
    public class GetRouteByHousingIdQuery : IRequest<Response<List<GetRouteByHousingIdQueryResult>>>
    {
        public int HousingId { get; set; }
        public GetRouteByHousingIdQuery(int housingId)
        {
            HousingId = housingId;
        }
    }
}
