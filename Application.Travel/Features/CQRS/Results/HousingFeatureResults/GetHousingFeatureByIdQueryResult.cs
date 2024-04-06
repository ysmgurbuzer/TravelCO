using Domain.Travel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Travel.Enums.HomeServiceTypesClass;

namespace Application.Travel.Features.CQRS.Results.HousingFeatureResults
{
    public class GetHousingFeatureByIdQueryResult
    {
        public int Id { get; set; }
        public int HousingId { get; set; }
        public string Name { get; set; }
        public bool Available { get; set; }
    }
}
