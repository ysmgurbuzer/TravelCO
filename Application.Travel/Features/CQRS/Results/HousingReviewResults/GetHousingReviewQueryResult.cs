
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Results.HousingReviewResults
{
    public class GetHousingReviewQueryResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HousingId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
