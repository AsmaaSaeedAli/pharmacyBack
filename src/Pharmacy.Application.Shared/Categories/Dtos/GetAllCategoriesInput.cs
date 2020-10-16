using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Categories.Dtos
{
    public class GetAllCategoriesInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
