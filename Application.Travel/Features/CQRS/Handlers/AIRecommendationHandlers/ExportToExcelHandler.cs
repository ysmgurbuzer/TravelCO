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
//    public class ExportToExcelHandler : IRequestHandler<ExportToExcelCommand, string>
//    {
//        private readonly IRepository<AIRecommendation> _repo;

//        public ExportToExcelHandler(IRepository<AIRecommendation> repo)
//        {
//            _repo = repo;
//        }

//        public async Task<string> Handle(ExportToExcelCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                await Task.Delay(100);
//                var recommendations = await _repo.GetListAsync();
//                await Task.Delay(300);
                 

//                DataTable dataTable = new DataTable();
//                dataTable.Columns.Add("UserId");
//                dataTable.Columns.Add("PreferredCategories");
//                dataTable.Columns.Add("HomeLatitude");
//                dataTable.Columns.Add("HomeLongitude");

//                for (int i = 1; i <= 10; i++)
//                {
//                    dataTable.Columns.Add($"Place{i} Latitude");
//                    dataTable.Columns.Add($"Place{i} Longitude");
//                    dataTable.Columns.Add($"Place{i} Types");
//                    dataTable.Columns.Add($"Place{i} Score");
//                }

//                foreach (var recommendation in recommendations)
//                {
//                    var row = new List<object>
//                    {
//                        recommendation.UserId,
//                        string.Join(",", recommendation.PreferredCategories),
//                        recommendation.HomeLatitude,
//                        recommendation.HomeLongitude
//                    };

//                    foreach (var place in recommendation.Places)
//                    {
//                        //////row.Add(place.Latitude);
//                        row.Add(place.Longitude);
//                        row.Add(string.Join(",", place.Types));
//                        row.Add(place.Score);
//                    }

//                    dataTable.Rows.Add(row.ToArray());
//                }

//                var excelFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "AIRecommendations1.xlsx");
//                ExcelHelper.WriteToExcel(dataTable, excelFile);

//                return excelFile;
//            }
//            catch (Exception ex)
//            {
//                return $"Hata oluştu: {ex.Message}";
//            }
//        }
//    }

//    public static class ExcelHelper
//    {
//        static ExcelHelper()
//        {
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//        }

//        public static void WriteToExcel(DataTable dataTable, string filePath)
//        {
//            using (var package = new ExcelPackage(new FileInfo(filePath)))
//            {
//                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
//                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
//                package.Save();
//            }
//        }
//    }
//}
