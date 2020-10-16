using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Pharmacy.Invoices.Dtos
{
    public class InvoiceDto : EntityDto<int?>
    {
        public string InvoiceNo { get; set; }
        public int? CustomerId { get; set; }
        public int InvoiceTypeId { get; set; }
        public int StatusId { get; set; }
        public double TotalAmount { get; set; }
        public double Discount { get; set; }
        public double NetAmount { get; set; }
        public string Notes { get; set; }

        public IList<InvoiceItemDto> InvoiceItems { get; set; }
    }
}
