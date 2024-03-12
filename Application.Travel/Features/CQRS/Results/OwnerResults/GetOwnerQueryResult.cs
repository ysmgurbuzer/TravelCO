using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Results.OwnerResults
{
    public class GetOwnerQueryResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ResponseTime { get; set; }
        public HomeownerTitle Title { get; set; }
    }
}
