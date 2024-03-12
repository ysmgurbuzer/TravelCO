using Application.Travel.Features.CQRS.Queries.ReservationQueries;
using Application.Travel.Features.CQRS.Results.ReservationResults;
using Application.Travel.Interfaces;
using Application.Travel.Models;
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
    public class GetReservationWithLocationQueryHandler : IRequestHandler<GetReservationWithLocationQuery, Response<List<GetReservationWithLocationQueryResult>>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _UserRepository;
        private readonly IRepository<Housing> _HousingRepository;
        public GetReservationWithLocationQueryHandler(IRepository<Reservation> repository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IRepository<User> UserRepository,
            IRepository<Housing> HousingRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _UserRepository = UserRepository;
            _HousingRepository = HousingRepository;
        }

        public async Task<Response<List<GetReservationWithLocationQueryResult>>> Handle(GetReservationWithLocationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Response<List<GetReservationWithLocationQueryResult>>.Fail("User information not found.");
                }

                var values = await _repository.GetListAsync();
                var MYReservations = values.Where(r => r.UserId.ToString() == userIdClaim).ToList();

                var result = new List<GetReservationWithLocationQueryResult>();

               

                foreach (var reservation in MYReservations)
                {
                    var housing=await _HousingRepository.GetByIdAsync(reservation.HousingId);
                    var ne = housing.Location;

                    if (housing.Location != null)
                    {
                        var reservationResult = _mapper.Map<GetReservationWithLocationQueryResult>(ne);

                      

                        LocationStorage.AddLoc(reservation.Id, reservationResult.City, reservationResult.Country, reservationResult.latitude, reservationResult.longitude);
                        result.Add(reservationResult);
                    }
                }

                return Response<List<GetReservationWithLocationQueryResult>>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<List<GetReservationWithLocationQueryResult>>.Fail($"Internal Server Error: {ex.Message}");
            }
           
        }

    }
}