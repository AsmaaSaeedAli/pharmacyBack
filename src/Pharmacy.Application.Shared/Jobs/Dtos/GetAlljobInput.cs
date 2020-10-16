using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Jobs.Dtos
{
    public class GetAllJobInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
