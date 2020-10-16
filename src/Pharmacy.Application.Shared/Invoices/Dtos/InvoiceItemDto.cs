using Abp.Application.Services.Dto;

namespace Pharmacy.Invoices.Dtos
{
    public class InvoiceItemDto : EntityDto<int?>
    {
        public int InvoiceId { get; set; }
        public string ItemNameAr { get; set; }
        public string ItemNameEn { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal Vat { get; set; }
        public int ItemId { get; set; }
        public double Quantity { get; set; }
        public int? UnitTypeId { get; set; }
        public double Amount { get; set; }
        public double Discount { get; set; }
        public double NetAmount { get; set; }
    }
}
