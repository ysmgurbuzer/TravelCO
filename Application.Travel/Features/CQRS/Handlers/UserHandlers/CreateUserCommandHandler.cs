using Application.Travel.Features.CQRS.Commands.UserCommands;
using Application.Travel.Interfaces;
using Application.Travel.Services;
using Domain.Travel.Entities;
using Domain.Travel.Enums;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.UserHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<User>>
    {
        private readonly IRepository<User> _repository;
        private readonly IUow _uow;
        public CreateUserCommandHandler(IRepository<User> repository,IUow uow)
        {
                _repository = repository;
            _uow = uow;
        }
        public async Task<Response<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
                
                await _repository.AddAsync(new User
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    Name = request.Name,
                    Surname = request.Surname,
                    PhoneNumber = request.PhoneNumber,
                    GenderId = request.GenderId,
                    RoleId = ((int)RoleTypes.User),
                    Password = hashedPassword, 
                });
                try
                {
                   await _uow.SaveChangeAsync();
                    return Response<User>.Success("User created successfully");
                }
                catch (Exception ex)
                {
                    return Response<User>.Fail("Verileriniz kaydedilirken bir hata oluştu.");
                }
              
            }
            catch (Exception ex)
            {
                return Response<User>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
