using Pharmacy.Dto;
using Pharmacy.Employees.Dtos;
using System.Collections.Generic;
namespace Pharmacy.Employees.Exporting
{
   public interface IEmployeesExcelExporter
    {
        FileDto ExportToFile(List<EmployeeListDto> branches);
    }
}
