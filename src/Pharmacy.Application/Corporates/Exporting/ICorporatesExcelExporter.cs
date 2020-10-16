using Pharmacy.Dto;
using System.Collections.Generic;
using Pharmacy.Corporates.Dtos;

namespace Pharmacy.Corporates.Exporting
{
    public interface ICorporatesExcelExporter
    {
        FileDto ExportCorporatesToFile(List<CorporatesListDto> Corporates);
    }
}
