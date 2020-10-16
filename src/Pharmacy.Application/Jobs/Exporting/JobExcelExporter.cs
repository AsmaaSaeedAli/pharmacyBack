using Pharmacy.Address.Exporting;
using Pharmacy.DataExporting.Excel.EpPlus;
using Pharmacy.Dto;
using Pharmacy.Jobs.Dtos;
using Pharmacy.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Jobs.Exporting
{
    public class JobExcelExporter: EpPlusExcelExporterBase, IJobExcelExporter
    {
        public JobExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        public FileDto ExportJobsToFile(List<JobsListDto> Jobs)
        {
            return CreateExcelPackage(
                L("Jobs") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Jobs"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("Code"), L("Name"), L("MaxNoOfPositions"), L("IsActive"));
                    AddObjects(sheet, 2, Jobs, _ => _.Code, _ => _.Name, _ => _.MaxNoOfPositions, _ => _.IsActive);
                    for (int i = 1; i <= 4; i++)
                        sheet.Column(i).AutoFit();
                });
        }

    }
}
