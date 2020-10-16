using Abp.Application.Services.Dto;
using System;
namespace Pharmacy.Items.ItemPriceDtos
{
    public class ItemPriceDto : EntityDto<int?>
    {
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool IsActive { get; set; }
    }
}
