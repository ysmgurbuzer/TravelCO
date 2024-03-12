using Application.Travel.Features.CQRS.Results.HousingResults;
using Application.Travel.Features.CQRS.Results.OwnerResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Queries.OwnerQueries
{
    public class GetOwnerByIdQuery : IRequest<Response<GetOwnerByIdQueryResult>>
    {
        public int Id { get; set; }
        public GetOwnerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
