using Application.Travel.Features.CQRS.Results.FavoritesResults;
using Application.Travel.Features.CQRS.Results.HousingDescriptionResults;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Queries.HousingDescriptionQueries
{
    public class GetDescriptionByIdQuery : IRequest<Response<GetDescriptionByIdQueryResult>>
    {
        public int HousingId { get; set; }
        public GetDescriptionByIdQuery(int housingId)
        {
            HousingId = housingId;
        }

    }
}
