using Application.Travel.Features.CQRS.Queries.FavoritesQueries;
using Application.Travel.Features.CQRS.Queries.HousingQueries;
using Application.Travel.Features.CQRS.Results.FavoritesResults;
using Application.Travel.Features.CQRS.Results.HousingResults;
using Application.Travel.Interfaces;
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

namespace Application.Travel.Features.CQRS.Handlers.FavoritesHandlers
{
    public class GetFavoritesByIdQueryHandler : IRequestHandler<GetFavoritesByIdQuery, Response<GetFavoritesByIdQueryResult>>
    {
        private readonly IRepository<Favorites> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetFavoritesByIdQueryHandler(IRepository<Favorites> repository, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
        public async Task<Response<GetFavoritesByIdQueryResult>> Handle(GetFavoritesByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                
                var userIdClaim = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (request.Id != null)
                {
                    var values = await _repository.GetByIdAsync(request.Id);
                    if (values != null)
                    {
                        if (values.UserId.ToString() != userIdClaim) { return Response<GetFavoritesByIdQueryResult>.Fail("Favorites not found"); }
                        var result = _mapper.Map<GetFavoritesByIdQueryResult>(values);

                        return Response<GetFavoritesByIdQueryResult>.Success(result);
                    }

                    return Response<GetFavoritesByIdQueryResult>.Success("Başarılı");
                }
                else
                {
                    return Response<GetFavoritesByIdQueryResult>.Fail("Favorites not found");
                }
            }
            catch (Exception ex)
            {
                return Response<GetFavoritesByIdQueryResult>.Fail($"Error: {ex.Message}");
            }
        }

    }
}
