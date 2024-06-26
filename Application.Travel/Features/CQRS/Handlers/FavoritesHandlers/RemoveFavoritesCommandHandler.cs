﻿using Application.Travel.Features.CQRS.Commands.FavoritesCommands;
using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Interfaces;
using Application.Travel.Services;
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
        private readonly IUow _uow;
        public RemoveFavoritesCommandHandler(IRepository<Favorites> repository, IHttpContextAccessor contextAccessor,IUow uow)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            _uow = uow;
        }



        public async Task<Response<Favorites>> Handle(RemoveFavoritesCommand request, CancellationToken cancellationToken)
        {
            try 
            {

                var userIdClaim = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           
                if (request.HousingId != null)
                {
                    
                    var value =  _repository.GetByFilter(item=>item.HousingId==request.HousingId && item.UserId.ToString()==userIdClaim);
                    if (value.UserId.ToString() != userIdClaim) 
                    { 
                        return Response<Favorites>.Fail($"Record not found");
                    }
                    if (value == null)
                    {
                        return Response<Favorites>.Fail($"Record not found");
                    }
                     _repository.Delete(value);
                  await _uow.SaveChangeAsync();
                    
                  

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
