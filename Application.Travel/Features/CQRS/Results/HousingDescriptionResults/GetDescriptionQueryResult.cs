
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Results.HousingDescriptionResults
{
    public class GetDescriptionQueryResult
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public int HousingId { get; set; }
    }
}
