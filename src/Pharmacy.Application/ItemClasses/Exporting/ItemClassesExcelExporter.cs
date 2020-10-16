using Pharmacy.ItemClasses.Dtos;
using Pharmacy.DataExporting.Excel.EpPlus;
using Pharmacy.Dto;
using Pharmacy.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.ItemClasses.Exporting
{
    public class ItemClassesExcelExporter : EpPlusExcelExporterBase, IItemClassesExcelExporter
    {
        public ItemClassesExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }
        public FileDto ExportItemClassesToFile(List<ItemClassesListDto> ItemClasses)
        {
            return CreateExcelPackage(
                L("ItemClasses") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ItemClasses"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("Name"), L("Code"), L("IsActive"));
                    AddObjects(sheet, 2, ItemClasses,
                        _ => _.Name,
                        _ => _.Code,
                        _=>_.IsActive
                       );
                    for (int i = 1; i <= 3; i++)
                        sheet.Column(i).AutoFit();
                });
        }
    }
}
