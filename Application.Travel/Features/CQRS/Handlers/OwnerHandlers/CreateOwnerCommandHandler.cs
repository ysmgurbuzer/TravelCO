using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.OwnerCommands;
using Application.Travel.Interfaces;
using AutoMapper;
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
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, Response<Owner>>
    {
        private readonly IRepository<Owner> _repository;
        private readonly IMapper _mapper;

        public CreateOwnerCommandHandler(IRepository<Owner> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<Owner>> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newHousing = _mapper.Map<Owner>(request);

                await _repository.AddAsync(newHousing);

                return Response<Owner>.Success(newHousing);
            }
            catch (ValidateException ex)
            {
                return Response<Owner>.Fail($"Validation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Response<Owner>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
