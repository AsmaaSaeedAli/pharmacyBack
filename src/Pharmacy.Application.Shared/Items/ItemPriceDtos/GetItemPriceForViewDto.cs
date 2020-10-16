using System;
using Abp.Application.Services.Dto;


namespace Pharmacy.Items.ItemPriceDtos
{
    public class GetItemPriceForViewDto : EntityDto
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public bool IsActive { get; set; }
        
    }
}
