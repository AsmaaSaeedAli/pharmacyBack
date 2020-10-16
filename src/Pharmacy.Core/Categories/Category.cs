using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Pharmacy.ItemClasses;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Categories
{
    public class Category : FullAuditedEntity, IPassivable
    {
        public string Code { get; set; }
        public LocalizedText Name { get; set; }
        public LocalizedText Description { get; set; }
        public bool IsActive { get; set; }
        public int? ItemClassId { get; set; }
        public ItemClass ItemClass { get; set; }

    }
}
