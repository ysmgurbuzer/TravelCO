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
    public class CreateFavoritesCommand : IRequest<Response<Favorites>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int HousingId { get; set; }

    }
}
