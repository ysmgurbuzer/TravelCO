using Application.Travel.Features.CQRS.Results.HousingDescriptionResults;
using Application.Travel.Features.CQRS.Results.HousingFeatureResults;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Queries.HousingFeaturesQueries
{
    public class GetHousingFeatureQuery : IRequest<Response<List<GetHousingFeaturesQueryResult>>>
    {

    }
}
