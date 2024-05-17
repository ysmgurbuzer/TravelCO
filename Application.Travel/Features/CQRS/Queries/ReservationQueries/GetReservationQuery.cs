using Application.Travel.Features.CQRS.Results.ReservationResults;
using Application.Travel.Features.CQRS.Results.UserResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Queries.ReservationQueries
{
    public class GetReservationQuery : IRequest<Response<List<GetReservationQueryResult>>>
    {
      
    }
}
