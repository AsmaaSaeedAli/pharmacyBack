using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Shared.SeedWork;
using Pharmacy.Categories;
using Pharmacy.SubCategories;
using Pharmacy.ItemClasses;
using Pharmacy.ManuFactories;
using Pharmacy.Corporates;
namespace Pharmacy.Items
{
   public class Item :FullAuditedEntity, IMayHaveTenant, IPassivable
    {
        public int? TenantId { get; set; }
        public bool IsActive { get; set; }
        public string ItemNumber { get; set; }
        public LocalizedText Name { get; set; }
        public LocalizedText Description { get; set; }
        public int ItemClassId { get; set; }
        public ItemClass ItemClass { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public int ManuFactoryId { get; set; }
        public ManuFactory ManuFactory { get; set; }
        public string BarCode { get; set; }
        public bool IsInsurance { get; set; }
        public bool HasVat { get; set; }
        public decimal Vat { get; set; }
        public int? PreferedStockId { get; set; }
        public int? ManufactureId { get; set; }
        public int? MarketId { get; set; }

        public int? CorporateFavoriteId { get; set; }
        public Corporate CorporateFavorite { get; set; }

    }
}
