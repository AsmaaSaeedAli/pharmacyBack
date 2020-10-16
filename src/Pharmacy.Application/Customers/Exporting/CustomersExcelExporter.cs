using Pharmacy.Customers.Dtos;
using Pharmacy.DataExporting.Excel.EpPlus;
using Pharmacy.Dto;
using Pharmacy.Storage;
using System.Collections.Generic;

namespace Pharmacy.Customers.Exporting
{
    public class CustomersExcelExporter : EpPlusExcelExporterBase, ICustomersExcelExporter
    {
        public CustomersExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }
        public FileDto ExportCustomersToFile(List<CustomerListDto> branches)
        {
            return CreateExcelPackage(
                L("Customers") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Customers"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("Code"), L("FullName"), L("PrimaryPhoneNumber"),L("Email"), L("Nationality"), L("NoOfDependencies"), L("IsActive"));
                    AddObjects(sheet, 2, branches, 
                        _ => _.Code, 
                        _ => _.FullName, 
                        _ => _.PrimaryPhoneNumber, 
                        _ => _.Email, 
                        _ => _.Nationality,
                        _ => _.NoOfDependencies,
                        _ => _.IsActive);
                    for (int i = 1; i <= 7; i++)
                        sheet.Column(i).AutoFit();
                });
        }
    }
}
