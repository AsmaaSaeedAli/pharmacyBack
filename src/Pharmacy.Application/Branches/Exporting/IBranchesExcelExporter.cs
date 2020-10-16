using Pharmacy.Dto;
using System.Collections.Generic;
using Pharmacy.Branches.Dtos;

namespace Pharmacy.Branches.Exporting
{
    public interface IBranchesExcelExporter
    {
        FileDto ExportBranchesToFile(List<BranchesListDto> branches);
    }
}
