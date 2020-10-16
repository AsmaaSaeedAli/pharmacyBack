using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.ManuFactories.Dtos
{
   public class GetManuFactoryForViewDto : EntityDto
    {
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }

    }
    
}
