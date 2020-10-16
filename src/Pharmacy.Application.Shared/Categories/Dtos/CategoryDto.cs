using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Categories.Dtos
{
    public class CategoryDto : EntityDto<int?>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
        public int? ItemClassId { get; set; }
    }
}
