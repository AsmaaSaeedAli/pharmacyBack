using System;
using System.Collections.Generic;

namespace Pharmacy.MultiTenancy.Accounting.Dto
{
    public class AppInvoiceDto
    {
        public decimal Amount { get; set; }

        public string EditionDisplayName { get; set; }
        
        public string InvoiceNo { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string TenantLegalName { get; set; }

        public List<string> TenantAddress { get; set; }

        public string TenantTaxNo { get; set; }

        public string HostLegalName { get; set; }

        public List<string> HostAddress { get; set; }
    }
}