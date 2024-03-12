using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Queries.ReservationQueries;
using Application.Travel.Features.CQRS.Results.ReservationResults;
using Application.Travel.Features.CQRS.Results.UserResults;
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
    public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, Response<List<GetReservationQueryResult>>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _UserRepository;
        public GetReservationQueryHandler(IRepository<Reservation> repository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IRepository<User> UserRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _UserRepository = UserRepository;
        }
        //userın rezervasyon geçmişi
        public async Task<Response<List<GetReservationQueryResult>>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Response<List<GetReservationQueryResult>>.Fail("User information not found.");
                }

                var values = await _repository.GetListAsync();
                foreach ( var value in values )
                {
                   var değer= value.UserId.ToString();
                    Console.WriteLine(değer);
                }
                var filteredReservations = values.Where(r => r.UserId.ToString() == userIdClaim).ToList();

                var result = _mapper.Map<List<GetReservationQueryResult>>(filteredReservations);

                return Response<List<GetReservationQueryResult>>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<List<GetReservationQueryResult>>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
