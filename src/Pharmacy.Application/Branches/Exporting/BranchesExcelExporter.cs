using Pharmacy.Branches.Dtos;
using Pharmacy.DataExporting.Excel.EpPlus;
using Pharmacy.Dto;
using Pharmacy.Storage;
using System.Collections.Generic;

namespace Pharmacy.Branches.Exporting
{
    public class BranchesExcelExporter : EpPlusExcelExporterBase, IBranchesExcelExporter
    {
        public BranchesExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }
        public FileDto ExportBranchesToFile(List<BranchesListDto> branches)
        {
            return CreateExcelPackage(
                L("Branches") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Branches"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("Name"), L("PhoneNumber"), L("Address"),L("BranchType"), L("City"),L("IsActive"));
                    AddObjects(sheet, 2, branches, 
                        _ => _.Name, 
                        _ => _.PhoneNumber, 
                        _ => _.Address,
                        _ => _.BranchTypeName,
                        _ => _.CityName,
                        _ => _.IsActive);
                    for (int i = 1; i <= 6; i++)
                        sheet.Column(i).AutoFit();
                });
        }
    }
}
