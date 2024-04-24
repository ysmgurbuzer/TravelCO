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
    public class GetReservationByHousingIdForOwnerQueryHandler : IRequestHandler<GetReservationByHousingIdForOwnerQuery, Response<List<GetReservationByHousingIdForOwnerQueryResult>>>
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Housing> _housingRepository;
        private readonly IMapper _mapper;

        public GetReservationByHousingIdForOwnerQueryHandler(IRepository<Reservation> repository, IHttpContextAccessor httpContextAccessor, IRepository<Housing> housingRepository, IMapper mapper)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _housingRepository = housingRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<GetReservationByHousingIdForOwnerQueryResult>>> Handle(GetReservationByHousingIdForOwnerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var ownerHouses =  await _housingRepository.GetByIdAsync(request.HousingId);
                if(ownerHouses.OwnerId== userIdClaim)
                {
                    var allReservation =  _repository.GetList(x=>x.HousingId==request.HousingId);
                    var result = _mapper.Map<List<GetReservationByHousingIdForOwnerQueryResult>>(allReservation);

                    return Response<List<GetReservationByHousingIdForOwnerQueryResult>>.Success(result);
                }
                else
                {
                    return Response<List<GetReservationByHousingIdForOwnerQueryResult>>.Fail("Reservation not found.");
                }

               


            }
            catch (Exception ex)
            {
                return Response<List<GetReservationByHousingIdForOwnerQueryResult>>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
//owner her eve özel rezervasyonları görüntülesin.
//admin tüm rezervasyonları görüntülesin
//admin tüm ownerları görüntülesin
