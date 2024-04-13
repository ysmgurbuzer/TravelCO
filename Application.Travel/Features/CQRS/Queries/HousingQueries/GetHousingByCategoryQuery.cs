using Application.Travel.Features.CQRS.Results.HousingResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Queries.HousingQueries
{
    public class GetHousingByCategoryQuery : IRequest<Response<List<GetHousingByCategoryQueryResult>>>
    {
        public int CategoryId { get; set; }
        public GetHousingByCategoryQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
