using Abp.Domain.Entities.Auditing;

namespace Pharmacy.Invoices
{
    public class InvoiceItem : FullAuditedEntity
    {
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public double Quantity { get; set; }
        public int? UnitTypeId { get; set; }
        public decimal Vat { get; set; }

        public double Amount { get; set; }
        public double Discount { get; set; }
        public double NetAmount { get; set; }
    }
}
