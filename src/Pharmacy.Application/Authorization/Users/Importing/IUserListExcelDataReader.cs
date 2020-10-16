using System.Collections.Generic;
using Pharmacy.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Pharmacy.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
