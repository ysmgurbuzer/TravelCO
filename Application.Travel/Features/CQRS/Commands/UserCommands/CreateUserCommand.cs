using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.UserCommands
{
    public class CreateUserCommand:IRequest<Response<User>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int GenderId { get; set; }
        public decimal? WalletAmount { get; set; } = 0.0m;
        public int? RoleId { get; set; }
        public string Language { get; set; } = "English";
    }
}
