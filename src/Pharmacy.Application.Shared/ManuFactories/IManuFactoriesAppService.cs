using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Dto;
using Pharmacy.ManuFactories.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.ManuFactories
{
    public interface IManuFactoriesAppService : IApplicationService
    {
        Task<PagedResultDto<ManuFactoriesListDto>> GetAllManuFactories(GetAllManuFactoriesInput input);
        Task CreateOrUpdateManuFactory(ManuFactoryDto input);
        Task<ManuFactoryDto> GetManuFactoryForEdit(int id);
        Task<GetManuFactoryForViewDto> GetManuFactoryForView(int id);
        Task DeleteManuFactory(int? id);
        Task<FileDto> GetManuFactoriesToExcel(string filter);
    }
    
}
