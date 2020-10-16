using Pharmacy.DataExporting.Excel.EpPlus;
using Pharmacy.Dto;
using Pharmacy.ManuFactories.Dtos;
using Pharmacy.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.ManuFactories.Exporting
{
    public class ManuFactoriesExcelExporter : EpPlusExcelExporterBase, IManuFactoriesExcelExporter
    {
        public ManuFactoriesExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }
        public FileDto ExportManuFactoriesToFile(List<ManuFactoriesListDto> manuFactories)
        {
            return CreateExcelPackage(
               L("ManuFactories") + ".xlsx",
               excelPackage =>
               {
                   var sheet = excelPackage.Workbook.Worksheets.Add(L("ManuFactories"));
                   sheet.OutLineApplyStyle = true;
                   AddHeader(sheet, L("Name"), L("ContactName"), L("ContactPhone"), L("ContactEmail"), L("Notes"), L("IsActive"));
                   AddObjects(sheet, 2, manuFactories,
                       _ => _.Name,
                       _ => _.ContactName,
                       _ => _.ContactPhone,
                       _ => _.ContactEmail,
                       _ => _.Notes,
                       _ => _.IsActive);
                   for (int i = 1; i <= 6; i++)
                       sheet.Column(i).AutoFit();
               });
        }
    }
}
