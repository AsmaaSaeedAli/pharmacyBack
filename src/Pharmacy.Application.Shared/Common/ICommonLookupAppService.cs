
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Common.Dto;
using Pharmacy.Editions.Dto;

namespace Pharmacy.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
        Task<List<NameValueDto>> GetLookupsByLookupTypeId(GetLookupInput input);
        Task<List<NameValueDto>> GetCountriesForComboBox(string filter);
        Task<List<NameValueDto>> GetNationalitiesForComboBox(string filter);
        Task<List<NameValueDto>> GetRegionsForComboBox(GetAllEntityByParentIdInput filter);
        Task<List<NameValueDto>> GetCitiesForComboBox(GetAllEntityByParentIdInput filter);
        Task<List<NameValueDto>> GetBranchesForComboBox(string filter);
        Task<List<NameValueDto>> GetCategoriesForComboBox(string filter);
        Task<List<NameValueDto>> GetSubCategoriesForComboBox(string filter);
        Task<List<NameValueDto>> GetItemClassesForComboBox(string filter);
        Task<List<NameValueDto>> GetItemsForComboBox(string filter);
        Task<List<NameValueDto>> GetItemPricesForComboBox(string filter);
        Task<List<NameValueDto>> GetCorporatesForComboBox(string filter);
        Task<List<NameValueDto>> GetItemspricesbyitemForComboBox(int itemid);
        Task<List<NameValueDto>> GetJobsForComboBox(string filter);
        Task<List<NameValueDto>> GetManuFactoriesForComboBox(string filter);
    }
}