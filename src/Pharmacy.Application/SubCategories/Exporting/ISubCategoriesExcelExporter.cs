using Pharmacy.Dto;
using Pharmacy.SubCategories.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.SubCategories.Exporting
{
    public interface ISubCategoriesExcelExporter
    {
        FileDto ExportSubCategoriesToFile(List<SubCategoriesListDto> subCategories);
    }
}
