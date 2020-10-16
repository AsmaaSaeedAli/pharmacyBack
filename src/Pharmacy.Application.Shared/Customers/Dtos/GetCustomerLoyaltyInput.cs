using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Pharmacy.Customers.Dtos
{
    public class GetCustomerLoyaltyInput : PagedAndSortedResultRequestDto
    {
        public int CustomerId { get; set; }
    }
}
