
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Authorization;
using Pharmacy.Dto;
using Pharmacy.ManuFactories.Dtos;
using Pharmacy.ManuFactories.Exporting;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Pharmacy.ManuFactories
{
    public class ManuFactoriesAppService : PharmacyAppServiceBase, IManuFactoriesAppService
    {

        private readonly IRepository<ManuFactory> _manuFactoryRepository;

        private readonly IManuFactoriesExcelExporter _manuFactoryExcelExporter;
        public ManuFactoriesAppService(IRepository<ManuFactory> manuFactoryRepository,
            IManuFactoriesExcelExporter manuFactoryExcelExporter)
        {

            _manuFactoryRepository = manuFactoryRepository;
            _manuFactoryExcelExporter = manuFactoryExcelExporter;

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_ManuFactories_Manage)]
        public async Task CreateOrUpdateManuFactory(ManuFactoryDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_ManuFactories_Manage)]
        public async Task DeleteManuFactory(int? id)
        {
            if (id.HasValue)
                await _manuFactoryRepository.DeleteAsync(id.Value);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_ManuFactories)]
        public async Task<PagedResultDto<ManuFactoriesListDto>> GetAllManuFactories(GetAllManuFactoriesInput input)
        {
            var query = _manuFactoryRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e =>
                    e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                    )

                .Select(manuFactory => new ManuFactoriesListDto
                {
                    Id = manuFactory.Id,
                    Name = manuFactory.Name.CurrentCultureText,
                    ContactEmail = manuFactory.ContactEmail,
                    ContactName = manuFactory.ContactName,
                    ContactPhone = manuFactory.ContactPhone,
                    Notes = manuFactory.Notes,
                    IsActive = manuFactory.IsActive,

                });
            var totalCount = await query.CountAsync();
            var manuFactories = await query.OrderBy(input.Sorting ?? "id desc").ToListAsync();
            return new PagedResultDto<ManuFactoriesListDto>(totalCount, manuFactories);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_ManuFactories_Manage)]
        public async Task<ManuFactoryDto> GetManuFactoryForEdit(int id)
        {
            var ManuFactory = await _manuFactoryRepository.GetAsync(id);
            return ObjectMapper.Map<ManuFactoryDto>(ManuFactory);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_ManuFactories_Manage)]
        public async Task<GetManuFactoryForViewDto> GetManuFactoryForView(int id)
        {
            var manuFactory = await _manuFactoryRepository.FirstOrDefaultAsync(b => b.Id == id);
            if (manuFactory == null)
                throw new UserFriendlyException($"No ManuFactory With Id {id}");

            var output = new GetManuFactoryForViewDto
            {
                Id = manuFactory.Id,
                Name = manuFactory.Name.CurrentCultureText,
                IsActive = manuFactory.IsActive,
                ContactName = manuFactory.ContactName,
                ContactPhone = manuFactory.ContactPhone,
                ContactEmail = manuFactory.ContactEmail,
                Notes = manuFactory.Notes,

            };
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_ManuFactories_Export)]
        public async Task<FileDto> GetManuFactoriesToExcel(string filter)
        {
            var query = await _manuFactoryRepository.GetAll().AsNoTracking()
               .WhereIf(!string.IsNullOrWhiteSpace(filter), e =>
                   e.Name.StringValue.ToLower().Contains(filter.ToLower().Trim())
                   )

               .Select(manFactory => new ManuFactoriesListDto
               {
                   Id = manFactory.Id,
                   Name = manFactory.Name.CurrentCultureText,
                   ContactPhone = manFactory.ContactPhone,
                   ContactName = manFactory.ContactName,
                   ContactEmail = manFactory.ContactEmail,
                   Notes = manFactory.Notes,
                   IsActive = manFactory.IsActive,

               }).ToListAsync();

            return _manuFactoryExcelExporter.ExportManuFactoriesToFile(query);

        }

        private async Task CreateAsync(ManuFactoryDto input)
        {
            var manuFactory = ObjectMapper.Map<ManuFactory>(input);
            await _manuFactoryRepository.InsertAsync(manuFactory);
        }
        private async Task UpdateAsync(ManuFactoryDto input)
        {
            if (input.Id != null)
            {
                var manuFactory = await _manuFactoryRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, manuFactory);
            }
        }
    }

}
