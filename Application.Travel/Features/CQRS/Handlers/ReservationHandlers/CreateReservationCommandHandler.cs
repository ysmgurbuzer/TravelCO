using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.ReservationCommands;
using Application.Travel.Interfaces;
using Application.Travel.Services;
using AutoMapper;
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

namespace Application.Travel.Features.CQRS.Handlers.ReservationHandlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Response<Reservation>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Housing> _HousingRepository;
        private readonly IUow _uow;
        public CreateReservationCommandHandler(IRepository<Reservation> repository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IRepository<Housing> housingRepository,
            IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _HousingRepository = housingRepository;
            _uow = uow; 
        }

        public async Task<Response<Reservation>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
               var userIdClaim= _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
               if (string.IsNullOrEmpty(userIdClaim))
               {
                  return Response<Reservation>.Fail("User information not found.");
               }

                var mappedRequest= _mapper.Map<Reservation>(request);
                var house = await _HousingRepository.GetByIdAsync(mappedRequest.HousingId);
                if (IsReservationAvailable(house, mappedRequest.CheckInDate, mappedRequest.CheckOutDate))
                {
                    mappedRequest.UserId = Int32.Parse(userIdClaim);
                    mappedRequest.Status = ReservationStatus.Confirmed.ToString();
                    mappedRequest.HousingId=request.HousingId;
                    mappedRequest.TotalPrice = CalculateTotalAmount(mappedRequest.CheckOutDate, mappedRequest.CheckInDate, mappedRequest.NumberOfAdults, house);

                    house.Reservations.Add(mappedRequest);

                    await _repository.AddAsync(mappedRequest);
                    await _uow.SaveChangeAsync();

                    return Response<Reservation>.Success(mappedRequest);
                }
                else
                {
                    return Response<Reservation>.Fail("Housing is not available for the specified dates.");
                }

            }
            catch (ValidateException ex)
            {
                return Response<Reservation>.Fail($"Validation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<Reservation>.Fail($"Internal Server Error: {ex.Message}");
            }





        }
        private bool IsReservationAvailable(Housing house, DateTime checkInDate, DateTime checkOutDate)
        {
          
            return house.Reservations.All(reservation =>
                checkOutDate <= reservation.CheckInDate || checkInDate >= reservation.CheckOutDate);
        }
        private decimal CalculateTotalAmount( DateTime CheckOutDate, DateTime CheckInDate,int NumberOfAdults,Housing house)
        {
            
            int numberOfDays = (int)(CheckOutDate - CheckInDate).TotalDays;
            decimal totalAmount = numberOfDays * house.Price * NumberOfAdults ;

            return totalAmount;
        }
    }
}
