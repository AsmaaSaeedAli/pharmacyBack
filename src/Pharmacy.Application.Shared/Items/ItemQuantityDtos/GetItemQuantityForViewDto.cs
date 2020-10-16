using Abp.Application.Services.Dto;

namespace Pharmacy.Items.ItemQuantityDtos
{
    public class GetItemQuantityForViewDto : EntityDto
    {
        public string BranchName { get; set; }
        public string ItemName { get; set; }
        public string ItemNameAr { get; set; }
        public string ItemNameEn { get; set; }
        public decimal Quantity { get; set; }
        public string UnitName { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal QuantityLimit { get; set; }

    }
}
