using Application.Travel.Features.CQRS.Commands.HousingCommands;
using Application.Travel.Features.CQRS.Commands.HousingFeatureCommands;
using Application.Travel.Interfaces;
using Application.Travel.Services;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.HousingFeatureHandlers
{
    public class RemoveHousingFeatureCommandHandler : IRequestHandler<RemoveHousingFeatureCommand, Response<HousingFeatures>>
    {
        private readonly IRepository<HousingFeatures> _repository;
        private readonly IUow _uow;

        public RemoveHousingFeatureCommandHandler(IRepository<HousingFeatures> repository,IUow uow)
        {
            _repository = repository;
            _uow = uow; 
        }
        //şimdilik remove işlemini pas geçtik 2.02.2024
        public async Task<Response<HousingFeatures>> Handle(RemoveHousingFeatureCommand request, CancellationToken cancellationToken)
        {
            try
            {
             
                if (request.Id != null)
                {
                    var value = await _repository.GetByIdAsync(request.Id);
                     _repository.Delete(value);
                    await _uow.SaveChangeAsync();
                    return Response<HousingFeatures>.Success("Housing delete transaction is success");
                }
                else
                {
                    return Response<HousingFeatures>.Fail("Invalid ObjectId provided");
                }
            }
            catch (Exception ex)
            {
                return Response<HousingFeatures>.Fail($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
