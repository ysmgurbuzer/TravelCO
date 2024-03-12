using Domain.Travel.Entities;
using Domain.Travel.Enums;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.RoleCommands
{
    public class UpdateRoleCommand : IRequest<Response<Roles>>
    {
        public int Id { get; set; }
        public DescriptionCommand Command { get; set; }

    }
    public class DescriptionCommand
    {
      
        public RoleTypes RoleName { get; set; }
    }
}

