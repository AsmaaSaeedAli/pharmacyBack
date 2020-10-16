using Pharmacy.Dto;
using Pharmacy.Jobs.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Jobs.Exporting
{
   public interface IJobExcelExporter
    {
        FileDto ExportJobsToFile(List<JobsListDto> Jobs);
    }
}
