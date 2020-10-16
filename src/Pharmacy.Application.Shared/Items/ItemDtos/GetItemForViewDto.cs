using Abp.Application.Services.Dto;

namespace Pharmacy.Items.ItemDtos
{
    public class GetItemForViewDto : EntityDto
    {
        public string ItemNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ItemClassName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ManuFactoryName { get; set; }
        public string BarCode { get; set; }
        public string CorporateFavoriteName { get; set; }
        public bool IsActive { get; set; }
        public bool IsInsurance { get; set; }

        public bool HasVat { get; set; }
        public decimal Vat { get; set; }

    }
}
