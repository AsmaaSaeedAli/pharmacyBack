using Pharmacy.DataExporting.Excel.EpPlus;
using Pharmacy.Dto;
using Pharmacy.Storage;
using Pharmacy.SubCategories.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.SubCategories.Exporting
{
    public class SubCategoriesExcelExporter : EpPlusExcelExporterBase, ISubCategoriesExcelExporter
    {
        public SubCategoriesExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        public FileDto ExportSubCategoriesToFile(List<SubCategoriesListDto> subCategories)
        {
            return CreateExcelPackage(
                L("SubCategories") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("SubCategories"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("Name"), L("Category"), L("IsActive"));
                    AddObjects(sheet, 2, subCategories,
                        _ => _.Name,
                        _ => _.CategoryName,
                        _ => _.IsActive);
                    for (int i = 1; i <= 3; i++)
                        sheet.Column(i).AutoFit();
                });
        }
    }
}
