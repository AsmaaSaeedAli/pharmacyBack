using System.Collections.Generic;
using Pharmacy.Authorization.Users.Dto;
using Pharmacy.Dto;

namespace Pharmacy.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}