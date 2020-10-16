using Pharmacy.Categories.Dtos;
using Pharmacy.DataExporting.Excel.EpPlus;
using Pharmacy.Dto;
using Pharmacy.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Categories.Exporting
{
    public class CategoriesExcelExporter : EpPlusExcelExporterBase, ICategoriesExcelExporter
    {
        public CategoriesExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }
        public FileDto ExportCategoriesToFile(List<CategoriesListDto> categories)
        {
            return CreateExcelPackage(
                L("Categories") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Categories"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("CategoryName"), L("Description"), L("ItemClass"), L("IsActive"));
                    AddObjects(sheet, 2, categories,
                        _ => _.Name,
                        _ => _.Description,
                        _=>_.ItemClassName,
                        _=>_.IsActive
                       );
                    for (int i = 1; i <= 4; i++)
                        sheet.Column(i).AutoFit();
                });
        }
    }
}
