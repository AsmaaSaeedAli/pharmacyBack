using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
namespace Pharmacy.Items.ItemDtos
{
    public class GetAllItemInput :PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public IList<int> ClassIds { get; set; }
        public IList<int> CategoryIds { get; set; }
        public IList<int> SubCategoryIds { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public IList<int> ManuFactoryIds { get; set; }

    }
}
