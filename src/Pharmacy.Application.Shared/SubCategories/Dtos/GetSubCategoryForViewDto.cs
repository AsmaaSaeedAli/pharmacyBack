using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.SubCategories.Dtos
{
    public class GetSubCategoryForViewDto: EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
