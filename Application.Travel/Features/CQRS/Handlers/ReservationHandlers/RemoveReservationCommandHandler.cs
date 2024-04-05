using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.ReservationCommands;
using Application.Travel.Interfaces;
using Application.Travel.Services;
using Domain.Travel.Entities;
using Domain.Travel.Enums;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Domain.Travel.Enums.ReservationStatus;

namespace Application.Travel.Features.CQRS.Handlers.ReservationHandlers
{
    public class RemoveReservationCommandHandler : IRequestHandler<RemoveReservationCommand, Response<Reservation>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IRepository<Housing> _Housingrepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUow _uow;

        public RemoveReservationCommandHandler(IRepository<Reservation> repository, IHttpContextAccessor httpContextAccessor, IRepository<Housing> housingrepository,IUow uow)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _Housingrepository = housingrepository; 
            _uow = uow;
        }

        public async Task<Response<Reservation>> Handle(RemoveReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                

                if (string.IsNullOrEmpty(userIdClaim) )
                {
                    return Response<Reservation>.Unauthorized("Unauthorized. Only users can remove reservation.");
                }

                var reservation = await _repository.GetByIdAsync(request.Id);
                var house = await _Housingrepository.GetByIdAsync(reservation.HousingId);

                if (reservation == null)
                {
                    return Response<Reservation>.Fail("Reservation not found");
                }

                if (reservation.UserId.ToString() != userIdClaim)
                {
                    return Response<Reservation>.Unauthorized("Unauthorized. You are not the owner of this reservation.");
                }
                if ((reservation.CheckInDate - DateTime.Now).TotalDays <= 7)
                {
                    return Response<Reservation>.Fail("Cannot cancel reservation. Minimum cancellation period is 7 days.");
                }

                reservation.Status=ReservationStatus.Confirmed.ToString();
                Console.WriteLine(reservation.Status);
                house.Reservations.Remove(reservation);
                 _repository.Delete(reservation);
                //PARA İADESİ İŞLEMİ YAPILACAK
                await _uow.SaveChangeAsync();

                return Response<Reservation>.Success("Housing removed successfully.");
            }
            catch (Exception ex)
            {
                return Response<Reservation>.Fail($"Error: {ex.Message}");
            }
        }
    }
}
