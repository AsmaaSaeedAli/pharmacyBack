using Pharmacy.Dto;
using System.Collections.Generic;
using Pharmacy.Customers.Dtos;

namespace Pharmacy.Customers.Exporting
{
    public interface ICustomersExcelExporter
    {
        FileDto ExportCustomersToFile(List<CustomerListDto> branches);
    }
}
