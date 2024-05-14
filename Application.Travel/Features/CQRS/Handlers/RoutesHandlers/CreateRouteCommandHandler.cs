using Application.Travel.Features.CQRS.Commands.ReservationCommands;
using Application.Travel.Features.CQRS.Commands.RouteCommands;
using Application.Travel.Interfaces;
using Application.Travel.Models;
using Application.Travel.Services;
using AutoMapper.Configuration.Annotations;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.RoutesHandlers
{
    public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, Response<Routes>>
    {
        private readonly IRepository<Routes> _routesRepository;
        private readonly IRepository<Location> _locationsRepository;
        private readonly IRepository<Housing> _housingRepository;
        private readonly IUow _uow;
        public CreateRouteCommandHandler(IRepository<Routes> routesRepository, IRepository<Location> locationsRepository, IRepository<Housing> housingRepository, IUow uow)
        {
                _routesRepository = routesRepository;   
            _locationsRepository = locationsRepository;
            _housingRepository = housingRepository;
            _uow = uow;
        }


        public async Task<Response<Routes>> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Response<Routes>.Fail("Rota verileri boş");
            }
          

            foreach (var command in request.RoutesList)
            {
                command.HousingId = ReservationWithHosing.HousingId;

                var route = new Routes
                {
                    HousingId = command.HousingId,
                    Distance = command.Distance,
                    Puan = command.Puan,    
                    RezNo   = command.RezNo,
                    Route_ID = command.Route_ID,    
                    Source_Lat = command.Source_Lat,    
                    Source_Long = command.Source_Long,
                    User_ID = command.User_ID,
                    Target_Lat = command.Target_Lat,
                    Target_Long = command.Target_Long,
                    Time = command.Time,
                    TravelMode = command.TravelMode,
                    VehicleName = command.VehicleName,
                };
                
               await _routesRepository.AddAsync(route);   
            }

            await _uow.SaveChangeAsync();

            return Response<Routes>.Success("Rota başarıyla database'e eklendi");
        }
    }
}
