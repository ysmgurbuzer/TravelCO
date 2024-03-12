using Application.Travel.Features.CQRS.Results.FavoritesResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Queries.FavoritesQueries
{
    public class GetFavoritesQuery: IRequest<Response<List<GetFavoritesQueryResult>>>
    {
    }
}
