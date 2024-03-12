using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Results.FavoritesResults
{
    public class GetFavoritesByIdQueryResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HousingId { get; set; }
    }
}
