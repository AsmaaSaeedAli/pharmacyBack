using System.Threading.Tasks;
using Abp.Application.Services;
using Pharmacy.Editions.Dto;
using Pharmacy.MultiTenancy.Dto;

namespace Pharmacy.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}