using Application.Travel.Features.CQRS.Commands.FavoritesCommands;
using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Interfaces;
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
    public class RemoveFavoritesCommandHandler:IRequestHandler<RemoveFavoritesCommand,Response<Favorites>>
    {
        private readonly IRepository<Favorites> _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        public RemoveFavoritesCommandHandler(IRepository<Favorites> repository, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
        }



        public async Task<Response<Favorites>> Handle(RemoveFavoritesCommand request, CancellationToken cancellationToken)
        {
            try 
            {

                var userIdClaim = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           
                if (request.Id != null)
                {
                    var value = await _repository.GetByIdAsync(request.Id);
                    if (value.UserId.ToString() != userIdClaim) 
                    { 
                        return Response<Favorites>.Fail($"Record not found");
                    }
                     _repository.Delete(value);
                    
                  

                }
                return Response<Favorites>.Success($"Favorite delete transaction is success");

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
