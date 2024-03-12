using Application.Travel.Features.CQRS.Results.OwnerResults;
using Application.Travel.Features.CQRS.Results.UserResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Queries.OwnerQueries
{
    public class GetOwnerQuery : IRequest<Response<List<GetOwnerQueryResult>>>
    {
    }
}
