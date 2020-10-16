using Abp.Application.Services.Dto;
namespace Pharmacy.Items.ItemDtos
{
    public class ItemDto : EntityDto<int?>
    {
        public string ItemNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemClassId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int ManuFactoryId { get; set; }
        public string BarCode { get; set; }
        public int CorporateFavoriteId { get; set; }
        public bool IsActive { get; set; }
        public bool IsInsurance { get; set; }
        public bool HasVat { get; set; }
        public decimal Vat { get; set; }
        public int? PreferedStockId { get; set; }
        public int? ManufactureId { get; set; }
        public int? MarketId { get; set; }
    }
}
