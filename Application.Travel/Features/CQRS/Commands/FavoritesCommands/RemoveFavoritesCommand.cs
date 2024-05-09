using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.FavoritesCommands
{
    public class RemoveFavoritesCommand : IRequest<Response<Favorites>>
    {
        public int HousingId { get; set; }
        public RemoveFavoritesCommand(int housingId)
        {
            HousingId = housingId;
        }
    }
}
