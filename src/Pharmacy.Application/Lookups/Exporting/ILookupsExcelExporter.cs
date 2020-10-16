using Pharmacy.Dto;
using System.Collections.Generic;
using Pharmacy.Lookups.Dtos;

namespace Pharmacy.Lookups.Exporting
{
    public interface ILookupsExcelExporter
    {
        FileDto ExportToFile(List<LookupListDto> lookups, string fileName);
    }
}
