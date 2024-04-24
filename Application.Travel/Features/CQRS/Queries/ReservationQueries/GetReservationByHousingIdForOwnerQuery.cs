using Application.Travel.Features.CQRS.Results.ReservationResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Queries.ReservationQueries
{
    public class GetReservationByHousingIdForOwnerQuery : IRequest<Response<List<GetReservationByHousingIdForOwnerQueryResult>>>
    {

        public int HousingId { get; set; }
        public GetReservationByHousingIdForOwnerQuery(int housingId)
        {
            HousingId = housingId;
        }
    }
}
