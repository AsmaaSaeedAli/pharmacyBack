using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Authorization.Permissions.Dto;

namespace Pharmacy.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
