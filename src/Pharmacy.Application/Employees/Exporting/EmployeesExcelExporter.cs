using System;
using System.Collections.Generic;
using System.Text;
using Pharmacy.DataExporting.Excel.EpPlus;
using Pharmacy.Dto;
using Pharmacy.Employees.Dtos;
using Pharmacy.Storage;

namespace Pharmacy.Employees.Exporting
{
   public class EmployeesExcelExporter : EpPlusExcelExporterBase, IEmployeesExcelExporter
    {
        public EmployeesExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<EmployeeListDto> employees)
        {
            return CreateExcelPackage(
                L("Branches") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Cities"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("Code"), L("Name"), L("PhoneNumber"), L("NationalId"), L("DateOfBirth"), L("Nationality"), L("Branch"), L("IsActive"));
                    AddObjects(sheet, 2, employees,
                        _ => _.Code,
                        _ => _.FullName,
                        _ => _.PhoneNumber,
                        _ => _.NationalId,
                        _ => _.DateOfBirth,
                        _ => _.Nationality,
                        _ => _.BranchName,
                        _ => _.IsActive);
                    for (int i = 1; i <= 7; i++)
                        sheet.Column(i).AutoFit();
                });
        }
    }
}
