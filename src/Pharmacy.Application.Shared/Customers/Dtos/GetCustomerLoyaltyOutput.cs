using System;

namespace Pharmacy.Customers.Dtos
{
    public class GetCustomerLoyaltyOutput
    {
        public string CustomerName { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double Amount { get; set; }
        public string InvoiceType { get; set; }

    }
}