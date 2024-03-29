using Application.Travel.Features.CQRS.Results.CategoryResults;
using Application.Travel.Features.CQRS.Results.FavoritesResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Queries.CategoryQueries
{
    public class GetCategoryQuery : IRequest<Response<List<GetCategoryQueryResult>>>
    {
    }
}
