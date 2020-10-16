using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Pharmacy.Lookups;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Pharmacy.Jobs
{
    public class Job : FullAuditedEntity, IMustHaveTenant, IPassivable
    {

        public string Code { get; set; }
        public virtual LocalizedText Name { get; set; }
        public int? MaxNoOfPositions { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
    }
}
