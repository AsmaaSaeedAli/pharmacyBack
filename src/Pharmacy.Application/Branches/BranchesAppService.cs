using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Address;
using Pharmacy.Authorization;
using Pharmacy.Branches.Dtos;
using Pharmacy.Branches.Exporting;
using Pharmacy.Dto;
using Pharmacy.Lookups;
namespace Pharmacy.Branches
{
    public class BranchesAppService : PharmacyAppServiceBase, IBranchesAppService
    {
        private readonly IRepository<Lookup> _lookupRepository;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IBranchesExcelExporter _branchExcelExporter;
        public BranchesAppService(IRepository<Lookup> lookupRepository, IRepository<Branch> branchRepository, 
            IBranchesExcelExporter branchExcelExporter, IRepository<City> cityRepository)
        {
            _lookupRepository = lookupRepository;
            _branchRepository = branchRepository;
            _branchExcelExporter = branchExcelExporter;
            _cityRepository = cityRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Branches_Manage)]
        public async Task CreateOrUpdateBranch(BranchDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Branches_Manage)]
        public async Task DeleteBranch(int? id)
        {
            if (id.HasValue)
                await _branchRepository.DeleteAsync(id.Value);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Branches)]
        public async Task<PagedResultDto<BranchesListDto>> GetAllBranches(GetAllBranchesInput input)
        {
            var filteredBranches = _branchRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));

            var query = from branch in filteredBranches
                        join lookup in _lookupRepository.GetAll().AsNoTracking() on branch.BranchTypeId equals lookup.Id into branchTypes
                        from lookup in branchTypes.DefaultIfEmpty()
                        join city in _cityRepository.GetAll().AsNoTracking() on branch.CityId equals city.Id into cities
                        from city in cities.DefaultIfEmpty()
                        select new BranchesListDto
                        {
                            Id = branch.Id,
                            Name = branch.Name.CurrentCultureText,
                            Address = branch.Address,
                            PhoneNumber = branch.PhoneNumber,
                            BranchTypeName = lookup == null ? "" : lookup.Name.CurrentCultureText,
                            CityName = city == null ? "" : city.Name.CurrentCultureText,
                            IsActive = branch.IsActive
                        };
            var totalCount = await query.CountAsync();
            var branches = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<BranchesListDto>(totalCount, branches);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Branches_Manage)]
        public async Task<BranchDto> GetBranchForEdit(int id)
        {
            var branch = await _branchRepository.GetAsync(id);
            return ObjectMapper.Map<BranchDto>(branch);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Branches_Manage)]
        public async Task<GetBranchForViewDto> GetBranchForView(int id)
        {
            var branch = await _branchRepository.GetAllIncluding(b => b.BranchType, b => b.City).FirstOrDefaultAsync(b => b.Id == id);
            if (branch == null)
                throw new UserFriendlyException($"No Branch With Id {id}");
            
            var output = new GetBranchForViewDto
            {
                Id = branch.Id,
                Code = branch.Code,
                Name = branch.Name.CurrentCultureText,
                IsActive = branch.IsActive,
                Address = branch.Address,
                PhoneNumber = branch.PhoneNumber,
                BranchTypeName = branch.BranchType.Name.CurrentCultureText,
                CityName = branch.City.Name.CurrentCultureText
            };
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Branches_Export)]
        public async Task<FileDto> GetBranchesToExcel(string filter)
        {
            var filteredBranches = _branchRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.Name.StringValue.ToLower().Contains(filter.ToLower().Trim())
                                                                        || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(filter.ToLower().Trim()));

            var query = from branch in filteredBranches
                join lookup in _lookupRepository.GetAll().AsNoTracking() on branch.BranchTypeId equals lookup.Id into branchTypes
                from lookup in branchTypes.DefaultIfEmpty()
                join city in _cityRepository.GetAll().AsNoTracking() on branch.CityId equals city.Id into cities
                from city in cities.DefaultIfEmpty()
                select new BranchesListDto
                {
                    Id = branch.Id,
                    Name = branch.Name.CurrentCultureText,
                    Address = branch.Address,
                    PhoneNumber = branch.PhoneNumber,
                    BranchTypeName = lookup == null ? "" : lookup.Name.CurrentCultureText,
                    CityName = city == null ? "" : city.Name.CurrentCultureText,
                    IsActive = branch.IsActive
                };
            var branches = await query.ToListAsync();
            return _branchExcelExporter.ExportBranchesToFile(branches);
        }

        private async Task CreateAsync(BranchDto input)
        {
            var branch = ObjectMapper.Map<Branch>(input);
            await _branchRepository.InsertAsync(branch);
        }
        private async Task UpdateAsync(BranchDto input)
        {
            if (input.Id != null)
            {
                var branch = await _branchRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, branch);
            }
        }
    }
}
