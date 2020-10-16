using System.Collections.Generic;
using Pharmacy.Authorization.Users.Importing.Dto;
using Pharmacy.Dto;

namespace Pharmacy.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
