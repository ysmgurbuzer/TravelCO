using Application.Travel.Features.CQRS.Results.FavoritesResults;
using Application.Travel.Features.CQRS.Results.HousingResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Queries.FavoritesQueries
{
    public class GetFavoritesByIdQuery : IRequest<Response<GetFavoritesByIdQueryResult>>
    {
        public int Id { get; set; }
        public GetFavoritesByIdQuery(int id)
        {
            Id = id;
        }
    }
}
