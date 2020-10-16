using System;
using Abp.Application.Services.Dto;
namespace Pharmacy.Invoices.Dtos
{
    public class InvoiceListDto : EntityDto
    {
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceType { get; set; }
        public double NetAmount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Notes { get; set; }

    }
}
