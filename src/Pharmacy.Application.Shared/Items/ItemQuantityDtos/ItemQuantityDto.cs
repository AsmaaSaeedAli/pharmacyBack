using Abp.Application.Services.Dto;
namespace Pharmacy.Items.ItemQuantityDtos
{
    public class ItemQuantityDto : EntityDto<int?>
    {
        public int BranchId { get; set; }
        public int ItemPriceId { get; set; }
        public decimal Quantity { get; set; }
        public int UnitId { get; set; }
        public decimal QuantityLimit { get; set; }

    }
}
