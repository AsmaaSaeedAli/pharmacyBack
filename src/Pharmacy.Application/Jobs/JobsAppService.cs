using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Authorization;
using Pharmacy.Dto;
using Pharmacy.Jobs.Dtos;
using Pharmacy.Jobs.Exporting;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Pharmacy.Jobs
{
    public class JobsAppService : PharmacyAppServiceBase, IJobsAppService
    {

        private readonly IRepository<Job> _jobRepository;
        private readonly IJobExcelExporter _jobExcelExporter;
        public JobsAppService(IJobExcelExporter jobExcelExporter,
            IRepository<Job> jobRepository)
        {
            _jobExcelExporter = jobExcelExporter;
            _jobRepository = jobRepository;

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Jobs_Manage)]
        public async Task CreateOrUpdateJob(JobDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);

        }
        [AbpAuthorize(AppPermissions.Pages_Administration_Jobs_Manage)]
        public async Task DeleteJob(int? id)
        {
            if (id.HasValue)
                await _jobRepository.DeleteAsync(id.Value);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Jobs)]
        public async Task<PagedResultDto<JobsListDto>> GetAllJobs(GetAllJobInput input)
        {
            var filteredJobs = _jobRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e =>
                    e.Name.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()))

                .Select(job => new JobsListDto
                {
                    Id = job.Id,
                    Code = job.Code,
                    Name = job.Name.CurrentCultureText,
                    MaxNoOfPositions = job.MaxNoOfPositions,
                    IsActive = job.IsActive,

                });
            var totalCount = await filteredJobs.CountAsync();
            var jobs = await filteredJobs.OrderBy(input.Sorting ?? "id desc").ToListAsync();
            return new PagedResultDto<JobsListDto>(totalCount, jobs);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Jobs_Manage)]
        public async Task<JobDto> GetJobForEdit(int id)
        {
            var job = await _jobRepository.GetAsync(id);
            return ObjectMapper.Map<JobDto>(job);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Jobs_Export)]
        public async Task<FileDto> GetJobsToExcel(string filter)
        {
            var filteredJobs = await _jobRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(filter), e =>
                    e.Name.StringValue.ToLower().Contains(filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(filter.ToLower().Trim()))

                .Select(job => new JobsListDto
                {
                    Id = job.Id,
                    Code = job.Code,
                    Name = job.Name.CurrentCultureText,
                    MaxNoOfPositions = job.MaxNoOfPositions,
                    IsActive = job.IsActive,

                }).ToListAsync();

            return _jobExcelExporter.ExportJobsToFile(filteredJobs);
        }

        private async Task CreateAsync(JobDto input)
        {
            var job = ObjectMapper.Map<Job>(input);
            await _jobRepository.InsertAsync(job);
        }


        private async Task UpdateAsync(JobDto input)
        {
            if (input.Id != null)
            {
                var region = await _jobRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, region);
            }
        }
    }
}
