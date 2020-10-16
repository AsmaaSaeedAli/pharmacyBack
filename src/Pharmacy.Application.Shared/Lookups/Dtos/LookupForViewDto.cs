using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Pharmacy.Lookups.Dtos
{
    public class LookupForViewDto : EntityDto
    {
        public string LookupTypeName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
