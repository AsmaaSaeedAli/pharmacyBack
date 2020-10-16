using System;
using Abp.Application.Services.Dto;
namespace Pharmacy.Items.ItemDtos
{
    public class ItemListDto : EntityDto
    {
        public string ItemNumber { get; set; }
        public string InternationalBarCode { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string ItemClassName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ManuFactoryName { get; set; }
        public string CorporateFavoriteName { get; set; }
        public string BarCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsInsurance { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
