using System;
using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace Pharmacy.Invoices.Dtos
{
    public class GetAllInvoiceInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public IList<int> StatusIds { get; set; }
        public IList<int> TypeIds { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}
