using Pharmacy.DataExporting.Excel.EpPlus;
using System.Collections.Generic;
using Pharmacy.Lookups.Dtos;
using Pharmacy.Dto;
using Pharmacy.Storage;

namespace Pharmacy.Lookups.Exporting
{
    public class LookupsExcelExporter : EpPlusExcelExporterBase, ILookupsExcelExporter
    {
        public LookupsExcelExporter(ITempFileCacheManager tempFileCacheManager) :base(tempFileCacheManager)
        {
        }
        public FileDto ExportToFile(List<LookupListDto> lookups, string fileName)
        {
            return CreateExcelPackage(
                fileName + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Lookups"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet,L("Code"),L("Name"),L("IsActive"));
                    AddObjects(sheet, 2, lookups,_ => _.Code,_ => _.Name,_ => _.IsActive);
                    for (int i = 1; i <= 3; i++)
                        sheet.Column(i).AutoFit();
                });
        }
    }
}
