using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.OwnerCommands;
using Application.Travel.Interfaces;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.OwnerHandlers
{
    public class RemoveOwnerCommandHandler : IRequestHandler<RemoveOwnerCommand, Response<Owner>>
    {
        private readonly IRepository<Owner> _repository;

        public RemoveOwnerCommandHandler(IRepository<Owner> repository)
        {
            _repository = repository;
        }

        public async Task<Response<Owner>> Handle(RemoveOwnerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                if (request.Id != null)
                {
                    var value = await _repository.GetByIdAsync(request.Id);
                     _repository.Delete(value);

                    return Response<Owner>.Success("Housing delete transaction is success");
                }
                else
                {
                    return Response<Owner>.Fail("Invalid ObjectId provided");
                }
            }
            catch (Exception ex)
            {
                return Response<Owner>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
