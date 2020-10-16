using System;
using Abp.Application.Services.Dto;


namespace Pharmacy.Items.ItemPriceDtos
{
    public class ItemPriceListDto : EntityDto
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsActive { get; set; }
    }
}
