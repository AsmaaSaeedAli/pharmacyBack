using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.SubCategories.Dtos
{
    public class SubCategoryDto: EntityDto<int?>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? CategoryId { get; set; }
    }
}
