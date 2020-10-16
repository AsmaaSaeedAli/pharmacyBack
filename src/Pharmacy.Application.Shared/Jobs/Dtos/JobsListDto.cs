using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Jobs.Dtos
{
    public class JobsListDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? MaxNoOfPositions { get; set; }
        public bool IsActive { get; set; }

    }
}
