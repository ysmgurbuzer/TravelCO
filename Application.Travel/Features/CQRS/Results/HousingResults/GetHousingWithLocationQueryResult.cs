using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Results.HousingResults
{
    public class GetHousingWithLocationQueryResult
    {
        public int HousingId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
