using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Pharmacy.Address;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.ManuFactories
{
    public class ManuFactory : FullAuditedEntity, IMustHaveTenant, IPassivable
    {

        public int TenantId { get; set; }
        public LocalizedText Name { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        // public string Logo { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
