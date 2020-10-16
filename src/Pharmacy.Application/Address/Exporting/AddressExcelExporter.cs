using Pharmacy.Address.CityDtos;
using Pharmacy.Address.CountryDtos;
using Pharmacy.Address.RegionDtos;
using Pharmacy.DataExporting.Excel.EpPlus;
using Pharmacy.Dto;
using Pharmacy.Storage;
using System.Collections.Generic;
namespace Pharmacy.Address.Exporting
{
    public class AddressExcelExporter : EpPlusExcelExporterBase, IAddressExcelExporter
    {
        public AddressExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        public FileDto ExportCitiesToFile(List<CityListDto> cities)
        {
            return CreateExcelPackage(
                L("Cities") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Cities"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet,L("Code"),L("Name"),L("Region"),L("IsActive"));
                    AddObjects(sheet, 2, cities,_ => _.Code,_ => _.Name,_ => _.RegionName,_ => _.IsActive);
                    for (int i = 1; i <= 4; i++)
                        sheet.Column(i).AutoFit();
                });
        }

        public FileDto ExportCountriesToFile(List<CountryListDto> countries)
        {
            return CreateExcelPackage(
                L("Countries") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Cities"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("Code"), L("Name"), L("Nationality"), L("Currency"), L("IsActive"));
                    AddObjects(sheet, 2, countries, _ => _.Code, _ => _.Name,_=>_.Nationality, _ => _.CurrencyName, _ => _.IsActive);
                    for (int i = 1; i <= 4; i++)
                        sheet.Column(i).AutoFit();
                });
        }

        public FileDto ExportRegionsToFile(List<RegionListDto> regions)
        {
            return CreateExcelPackage(
                L("Regions") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Cities"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("Code"), L("Name"), L("Country"), L("IsActive"));
                    AddObjects(sheet, 2, regions, _ => _.Code, _ => _.Name, _ => _.CountryName, _ => _.IsActive);
                    for (int i = 1; i <= 4; i++)
                        sheet.Column(i).AutoFit();
                });
        }
    }
}
