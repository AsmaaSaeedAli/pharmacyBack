using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.SubCategories.Dtos
{
    public class GetAllSubCategoriesInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
