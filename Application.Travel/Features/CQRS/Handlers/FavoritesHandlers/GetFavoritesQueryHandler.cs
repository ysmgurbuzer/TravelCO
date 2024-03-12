using Application.Travel.Features.CQRS.Queries.FavoritesQueries;
using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Results.FavoritesResults;
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

namespace Application.Travel.Features.CQRS.Handlers.FavoritesHandlers
{
    public class GetFavoritesQueryHandler : IRequestHandler<GetFavoritesQuery, Response<List<GetFavoritesQueryResult>>>
    {
        private readonly IRepository<Favorites> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetFavoritesQueryHandler(IRepository<Favorites> repository, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }


        public async Task<Response<List<GetFavoritesQueryResult>>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var values = await _repository.GetListAsync();
               var userFavorites=values.Where(x=>x.UserId.ToString()==userIdClaim).ToList();   
                var result = _mapper.Map<List<GetFavoritesQueryResult>>(userFavorites);

                return Response<List<GetFavoritesQueryResult>>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<List<GetFavoritesQueryResult>>.Fail($"Error: {ex.Message}");
            }
        }

    }
}
