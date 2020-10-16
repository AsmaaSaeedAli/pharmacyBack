using Pharmacy.ItemClasses.Dtos;
using Pharmacy.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.ItemClasses.Exporting
{
    public interface IItemClassesExcelExporter
    {
        FileDto ExportItemClassesToFile(List<ItemClassesListDto> ItemClasses);
    }
}
