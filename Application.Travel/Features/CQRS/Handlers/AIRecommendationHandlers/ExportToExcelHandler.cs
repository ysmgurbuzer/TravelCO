using Application.Travel.Features.CQRS.Commands.AIRecommendationCommands;
using Application.Travel.Interfaces;
using Domain.Travel.Entities;
using MediatR;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.AIRecommendationHandlers
{
    public class ExportToExcelHandler : IRequestHandler<ExportToExcelCommand, string>
    {
        private readonly IRepository<AIRecommendation> _repo;

        public ExportToExcelHandler(IRepository<AIRecommendation> repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(ExportToExcelCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var recommendations = await _repo.GetListAsync();


                // DataTable oluştur
                DataTable dataTable = new DataTable();
             
                dataTable.Columns.Add("UserId");
                dataTable.Columns.Add("PreferredCategories");
                dataTable.Columns.Add("HomeLatitude");
                dataTable.Columns.Add("HomeLongitude");
                dataTable.Columns.Add("Place1Latitude");
                dataTable.Columns.Add("Place1Longitude");
                dataTable.Columns.Add("Place1Type");
                dataTable.Columns.Add("Place2Latitude");
                dataTable.Columns.Add("Place2Longitude");
                dataTable.Columns.Add("Place2Type");
                dataTable.Columns.Add("Place3Latitude");
                dataTable.Columns.Add("Place3Longitude");
                dataTable.Columns.Add("Place3Type");
                dataTable.Columns.Add("Place1Score");
                dataTable.Columns.Add("Place2Score");
                dataTable.Columns.Add("Place3Score");

                foreach (var recommendation in recommendations)
                {
                    dataTable.Rows.Add(
                        
                        recommendation.UserId,
                        string.Join(",", recommendation.PreferredCategories),
                        recommendation.HomeLatitude,
                        recommendation.HomeLongitude,
                        recommendation.Place1Latitude,
                        recommendation.Place1Longitude,
                        string.Join(",", recommendation.Place1Type),
                        recommendation.Place2Latitude,
                        recommendation.Place2Longitude,
                        string.Join(",", recommendation.Place2Type),
                        recommendation.Place3Latitude,
                        recommendation.Place3Longitude,
                        string.Join(",", recommendation.Place3Type),
                        recommendation.Place1Score,
                        recommendation.Place2Score,
                        recommendation.Place3Score
                    );
                }

                // Excel dosyasını oluştur
                var excelFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "AIRecommendations.xlsx");
                ExcelHelper.WriteToExcel(dataTable, excelFile);

                return excelFile;
            }
            catch (Exception ex)
            {
                // Hata durumunda ilgili mesajı döndür
                return $"Hata oluştu: {ex.Message}";
            }
        }

        
    }
    public static class ExcelHelper
    {
        static ExcelHelper()
        {
           
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public static void WriteToExcel(DataTable dataTable, string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                package.Save();
            }
        }
    }
}
