//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Application.Travel.Features.CQRS.Commands.AIRecommendationCommands;
//using Application.Travel.Interfaces;
//using Domain.Travel.Entities;
//using MediatR;
//using OfficeOpenXml;

//namespace Application.Travel.Features.CQRS.Handlers.AIRecommendationHandlers
//{
//   public class ExportToExcelHandler : IRequestHandler<ExportToExcelCommand, string>
//   {
//      private readonly IRepository<AIRecommendation> _repo;

//     public ExportToExcelHandler(IRepository<AIRecommendation> repo)
//     {
//            _repo = repo;
//     }

//        public async Task<string> Handle(ExportToExcelCommand request, CancellationToken cancellationToken)
//        {
//            var allAITable = await _repo.GetListAsync();
//            foreach (var item in allAITable) 
//            {
//                var homelong = item.HomeLongitude;
//                var homelat=item.HomeLatitude;

              


//            }
//        }



//    }
//}
