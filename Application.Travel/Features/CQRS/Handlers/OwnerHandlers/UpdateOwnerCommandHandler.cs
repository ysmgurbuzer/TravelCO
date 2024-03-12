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
    public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand, Response<Owner>>
    {
        private readonly IRepository<Owner> _repository;
        private readonly IMapper _mapper;

        public UpdateOwnerCommandHandler(IRepository<Owner> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<Owner>> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            try
            {
             
                if (request.Id != null)
                {
                    var unchangedHousing = await _repository.GetByIdAsync(request.Id);

                    var updatedHousing = _mapper.Map(request.Command, unchangedHousing);


                     _repository.Update(updatedHousing, unchangedHousing);

                    return Response<Owner>.Success("Housing update transaction is success");
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
