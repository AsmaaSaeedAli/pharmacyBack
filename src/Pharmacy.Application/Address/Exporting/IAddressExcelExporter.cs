using Pharmacy.Dto;
using System.Collections.Generic;
using Pharmacy.Address.CityDtos;
using Pharmacy.Address.CountryDtos;
using Pharmacy.Address.RegionDtos;

namespace Pharmacy.Address.Exporting
{
  public  interface IAddressExcelExporter
    {
        FileDto ExportCountriesToFile(List<CountryListDto> countries);
        FileDto ExportRegionsToFile(List<RegionListDto> regions);
        FileDto ExportCitiesToFile(List<CityListDto> cities);
    }
}
