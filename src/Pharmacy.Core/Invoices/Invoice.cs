using Abp.Domain.Entities.Auditing;

namespace Pharmacy.Invoices
{
    public class Invoice : FullAuditedEntity
    {
        public string InvoiceNo { get; set; }
        public int? CustomerId { get; set; }
        public int InvoiceTypeId { get; set; }
        public int StatusId { get; set; }
        public double TotalAmount { get; set; }
        public double Discount { get; set; }
        public double NetAmount { get; set; }
        public string Notes { get; set; }
    }
}
