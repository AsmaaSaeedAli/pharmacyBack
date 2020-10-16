using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Address;
using Pharmacy.Branches;
using Pharmacy.Categories;
using Pharmacy.Common.Dto;
using Pharmacy.Corporates;
using Pharmacy.Editions;
using Pharmacy.Editions.Dto;
using Pharmacy.ItemClasses;
using Pharmacy.Items;
using Pharmacy.Jobs;
using Pharmacy.Lookups;
using Pharmacy.ManuFactories;
using Pharmacy.SubCategories;

namespace Pharmacy.Common
{
    [AbpAuthorize]
    public class CommonLookupAppService : PharmacyAppServiceBase, ICommonLookupAppService
    {
        private readonly EditionManager _editionManager;
        private readonly IRepository<Lookup> _lookupRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<ItemClass> _itemClassRepository;
        private readonly IRepository<SubCategory> _subCategoryRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<Corporate> _corporateRepository;
        private readonly IRepository<ItemPrice> _itemPriceRepository;
        private readonly IRepository<Job> _jobRepository;
        private readonly IRepository<ManuFactory> _manuFactoryRepository;

        public CommonLookupAppService(EditionManager editionManager, IRepository<Lookup> lookupRepository,
            IRepository<Country> countryRepository, IRepository<Region> regionRepository, IRepository<City> cityRepository,
            IRepository<Branch> branchRepository, IRepository<Category> categoryRepository,
            IRepository<ItemClass> itemClassRepository, IRepository<SubCategory> subCategoryRepository,
            IRepository<ItemPrice> itemPriceRepository, IRepository<Item> itemRepository,
            IRepository<Corporate> corporateRepository,
             IRepository<Job> jobRepository,
            IRepository<ManuFactory> manuFactoryRepository)
        {
            _editionManager = editionManager;
            _lookupRepository = lookupRepository;
            _countryRepository = countryRepository;
            _regionRepository = regionRepository;
            _cityRepository = cityRepository;
            _branchRepository = branchRepository;
            _categoryRepository = categoryRepository;
            _itemClassRepository = itemClassRepository;
            _subCategoryRepository = subCategoryRepository;
            _itemPriceRepository = itemPriceRepository;
            _itemRepository = itemRepository;
            _corporateRepository = corporateRepository;
            _jobRepository = jobRepository;
            _manuFactoryRepository = manuFactoryRepository;

        }

        public async Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false)
        {
            var subscribableEditions = (await _editionManager.Editions.Cast<SubscribableEdition>().ToListAsync())
                .WhereIf(onlyFreeItems, e => e.IsFree)
                .OrderBy(e => e.MonthlyPrice);

            return new ListResultDto<SubscribableEditionComboboxItemDto>(
                subscribableEditions.Select(e => new SubscribableEditionComboboxItemDto(e.Id.ToString(), e.DisplayName, e.IsFree)).ToList()
            );
        }

        public async Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input)
        {
            if (AbpSession.TenantId != null)
            {
                //Prevent tenants to get other tenant's users.
                input.TenantId = AbpSession.TenantId;
            }

            using (CurrentUnitOfWork.SetTenantId(input.TenantId))
            {
                var query = UserManager.Users
                    .WhereIf(
                        !input.Filter.IsNullOrWhiteSpace(),
                        u =>
                            u.Name.Contains(input.Filter) ||
                            u.Surname.Contains(input.Filter) ||
                            u.UserName.Contains(input.Filter) ||
                            u.EmailAddress.Contains(input.Filter)
                    );

                var userCount = await query.CountAsync();
                var users = await query
                    .OrderBy(u => u.Name)
                    .ThenBy(u => u.Surname)
                    .PageBy(input)
                    .ToListAsync();

                return new PagedResultDto<NameValueDto>(
                    userCount,
                    users.Select(u =>
                        new NameValueDto(
                            u.FullName + " (" + u.EmailAddress + ")",
                            u.Id.ToString()
                            )
                        ).ToList()
                    );
            }
        }

        public GetDefaultEditionNameOutput GetDefaultEditionName()
        {
            return new GetDefaultEditionNameOutput
            {
                Name = EditionManager.DefaultEditionName
            };
        }

        public async Task<List<NameValueDto>> GetLookupsByLookupTypeId(GetLookupInput input)
        {
            var lookups = await _lookupRepository.GetAll().AsNoTracking()
                .Where(l => l.IsActive && l.LookupTypeId == (int)input.LookupType)
                .WhereIf(!string.IsNullOrEmpty(input.Filter), l=>l.Name.StringValue.Contains(input.Filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }

        public async Task<List<NameValueDto>> GetCountriesForComboBox(string filter)
        {
            var lookups = await _countryRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
                .WhereIf(!string.IsNullOrEmpty(filter), c => c.Name.StringValue.Contains(filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }
        public async Task<List<NameValueDto>> GetNationalitiesForComboBox(string filter)
        {
            var lookups = await _countryRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
                .WhereIf(!string.IsNullOrEmpty(filter), c => c.Nationality.StringValue.Contains(filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Nationality.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }
        public async Task<List<NameValueDto>> GetRegionsForComboBox(GetAllEntityByParentIdInput input)
        {
            var lookups = await _regionRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
                .WhereIf(input.ParentId.HasValue && input.ParentId > 0, r => r.CountryId == input.ParentId)
                .WhereIf(!string.IsNullOrEmpty(input.Filter), c => c.Name.StringValue.Contains(input.Filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }
        public async Task<List<NameValueDto>> GetCitiesForComboBox(GetAllEntityByParentIdInput input)
        {
            var lookups = await _cityRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
                .WhereIf(input.ParentId.HasValue && input.ParentId > 0, r => r.RegionId == input.ParentId)
                .WhereIf(!string.IsNullOrEmpty(input.Filter), c => c.Name.StringValue.Contains(input.Filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }

        public async Task<List<NameValueDto>> GetBranchesForComboBox(string filter)
        {
            var lookups = await _branchRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
                .WhereIf(!string.IsNullOrEmpty(filter), c => c.Name.StringValue.Contains(filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }

        public async Task<List<NameValueDto>> GetJobsForComboBox(string filter)
        {
            var lookups = await _jobRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
                .WhereIf(!string.IsNullOrEmpty(filter), c => c.Name.StringValue.Contains(filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }


        public async Task<List<NameValueDto>> GetCategoriesForComboBox(string filter)
        {
            var lookups = await _categoryRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
                .WhereIf(!string.IsNullOrEmpty(filter), c => c.Name.StringValue.Contains(filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }

        public async Task<List<NameValueDto>> GetItemClassesForComboBox(string filter)
        {
            var lookups = await _itemClassRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
                .WhereIf(!string.IsNullOrEmpty(filter), c => c.Name.StringValue.Contains(filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }

        public async Task<List<NameValueDto>> GetSubCategoriesForComboBox(string filter)
        {
            var lookups = await _subCategoryRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
                .WhereIf(!string.IsNullOrEmpty(filter), c => c.Name.StringValue.Contains(filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }

        public async Task<List<NameValueDto>> GetItemsForComboBox(string filter)
        {
            var lookups = await _itemRepository.GetAll().IgnoreQueryFilters().Where(i => !i.IsDeleted).AsNoTracking().Where(l => l.IsActive)
                .WhereIf(!string.IsNullOrEmpty(filter), c => c.Name.StringValue.Contains(filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }

        public async Task<List<NameValueDto>> GetItemspricesbyitemForComboBox(int itemid)
        {
            var lookups = await _itemPriceRepository.GetAll().IgnoreQueryFilters().AsNoTracking().Where(l => l.ItemId == itemid)
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Price.ToString(),
                    }).ToListAsync();
            return lookups;
        }


        public async Task<List<NameValueDto>> GetItemPricesForComboBox(string filter)
        {
            //decimal ????
            var lookups = await _itemPriceRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
               .WhereIf(!string.IsNullOrEmpty(filter), c => c.Price.ToString().Contains(filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Price.ToString(),
                    }).ToListAsync();
            return lookups;
        }
        public async Task<List<NameValueDto>> GetCorporatesForComboBox(string filter)
        {
            var lookups = await _corporateRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
                .WhereIf(!string.IsNullOrEmpty(filter), c => c.Name.StringValue.Contains(filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }

        public async Task<List<NameValueDto>> GetManuFactoriesForComboBox(string filter)
        {
            var lookups = await _manuFactoryRepository.GetAll().AsNoTracking().Where(l => l.IsActive)
                .WhereIf(!string.IsNullOrEmpty(filter), c => c.Name.StringValue.Contains(filter))
                .Select(l =>
                    new NameValueDto
                    {
                        Value = l.Id.ToString(),
                        Name = l.Name.CurrentCultureText
                    }).ToListAsync();
            return lookups;
        }
    }
}
