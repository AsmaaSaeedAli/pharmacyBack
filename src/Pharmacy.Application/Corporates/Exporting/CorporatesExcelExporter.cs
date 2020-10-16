using Pharmacy.Corporates.Dtos;
using Pharmacy.DataExporting.Excel.EpPlus;
using Pharmacy.Dto;
using Pharmacy.Storage;
using System.Collections.Generic;

namespace Pharmacy.Corporates.Exporting
{
    public class CorporatesExcelExporter : EpPlusExcelExporterBase, ICorporatesExcelExporter
    {
        public CorporatesExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }
        public FileDto ExportCorporatesToFile(List<CorporatesListDto> Corporates)
        {
            return CreateExcelPackage(
                L("Corporates") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Corporates"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("Name"), L("ContactName"), L("ContactPhone"), L("ContactEmail"), L("Notes"),L("Country"), L("City"),L("IsActive"));
                    AddObjects(sheet, 2, Corporates, 
                        _ => _.Name, 
                        _ => _.ContactName, 
                        _ => _.ContactPhone,
                        _ => _.ContactEmail,
                        _ => _.Notes,
                        _ => _.CountryName,
                        _ => _.CityName,
                        _ => _.IsActive);
                    for (int i = 1; i <= 6; i++)
                        sheet.Column(i).AutoFit();
                });
        }
    }
}
