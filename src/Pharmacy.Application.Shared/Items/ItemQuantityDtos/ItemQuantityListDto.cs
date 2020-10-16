using Abp.Application.Services.Dto;
namespace Pharmacy.Items.ItemQuantityDtos
{
    public class ItemQuantityListDto : EntityDto
    {
        public string BranchName { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public string UnitName { get; set; }
        public decimal QuantityLimit { get; set; }

    }
}
