using Application.Travel.Features.CQRS.Commands.FavoritesCommands;
using Application.Travel.Interfaces;
using Application.Travel.Services;
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
    public class CreateFavoritesCommandHandler: IRequestHandler<CreateFavoritesCommand, Response<Favorites>>
    {
        private readonly IRepository<Favorites> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUow _uow;
        public CreateFavoritesCommandHandler(IRepository<Favorites> repository, IMapper mapper, IHttpContextAccessor contextAccessor,IUow uow)
        {
            _repository = repository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _uow = uow;
        }
        public async Task<Response<Favorites>> Handle(CreateFavoritesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = Int32.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var HousingId = command.HousingId;
                var values = new Favorites
                {
                   
                    UserId =userIdClaim,
                   HousingId = HousingId,
                };

                await _repository.AddAsync(values);
                await _uow.SaveChangeAsync();
                return Response<Favorites>.Success(values);
            }
            catch (ValidateException ex)
            {
                return Response<Favorites>.Fail($"Validation Error: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                
                return Response<Favorites>.Fail($"Record not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<Favorites>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }

}

