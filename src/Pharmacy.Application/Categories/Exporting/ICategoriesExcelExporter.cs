using Pharmacy.Categories.Dtos;
using Pharmacy.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Categories.Exporting
{
    public interface ICategoriesExcelExporter
    {
        FileDto ExportCategoriesToFile(List<CategoriesListDto> categories);
    }
}
