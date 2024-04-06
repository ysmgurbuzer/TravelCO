using Application.Travel.Features.CQRS.Queries.ReservationQueries;
using Application.Travel.Features.CQRS.Results.ReservationResults;
using Application.Travel.Interfaces;
using AutoMapper;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.ReservationHandlers
{
    public class GetReservationByIdQueryHandler:IRequestHandler<GetReservationByIdQuery, Response<GetReservationByIdQueryResult>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Housing> _housingRepository;
        private readonly IMapper _mapper;

        public GetReservationByIdQueryHandler(IRepository<Reservation> repository, IHttpContextAccessor httpContextAccessor, IRepository<Housing> housingRepository,IMapper mapper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _housingRepository = housingRepository;
            _mapper = mapper;
        }


        //ownerın ilanlarına gelen rezervasyonlar  ??
        public async Task<Response<GetReservationByIdQueryResult>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Response<GetReservationByIdQueryResult>.Fail("User information not found.");
                }

                var reservation = await _repository.GetByIdAsync(request.Id);
                if (reservation == null)
                {
                    return Response<GetReservationByIdQueryResult>.Fail("Reservation not found.");
                }

                var housingReservations =  _housingRepository.GetByFilter(r => r.Id == reservation.HousingId);
                var userIsOwner = await CheckUserOwnershipAsync(reservation.HousingId, userIdClaim);

                if (housingReservations!=null && userIsOwner)
                {
                    var result = _mapper.Map<GetReservationByIdQueryResult>(housingReservations);

                    return Response<GetReservationByIdQueryResult>.Success(result);
                }
                else
                {
                    return Response<GetReservationByIdQueryResult>.Unauthorized("Unauthorized. You are not the owner of this housing or the reservation does not exist.");
                }
            }
            catch (Exception ex)
            {
                return Response<GetReservationByIdQueryResult>.Fail($"Internal Server Error: {ex.Message}");
            }
        }

        private async Task<bool> CheckUserOwnershipAsync(int housingId, string userId)
        {
            var housing = await _housingRepository.GetByIdAsync(housingId);
            return housing != null && housing.OwnerId.ToString() == userId;
        }
    }
}
