using Pharmacy.Dto;
using Pharmacy.ManuFactories.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.ManuFactories.Exporting
{
    public interface IManuFactoriesExcelExporter
    {
        FileDto ExportManuFactoriesToFile(List<ManuFactoriesListDto> ManuFactories);
    }
}
