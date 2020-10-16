using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.ItemClasses.Dtos
{
    public class ItemClassDto : EntityDto<int?>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
        public int ItemNumberStart { get; set; }
    }
}
