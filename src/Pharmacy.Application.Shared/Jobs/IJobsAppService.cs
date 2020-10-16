using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Dto;
using Pharmacy.Jobs.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Jobs
{
    public interface IJobsAppService : IApplicationService
    {
        Task<PagedResultDto<JobsListDto>> GetAllJobs(GetAllJobInput input);
        Task CreateOrUpdateJob(JobDto input);
        Task<JobDto> GetJobForEdit(int id);
        Task DeleteJob(int? id);
        Task<FileDto> GetJobsToExcel(string filter);
    }
}
