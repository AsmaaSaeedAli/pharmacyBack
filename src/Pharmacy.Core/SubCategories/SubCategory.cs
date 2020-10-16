using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Pharmacy.Categories;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.SubCategories
{
    public class SubCategory : FullAuditedEntity, IPassivable
    {
        public string Code { get; set; }
        public LocalizedText Name { get; set; }
        public bool IsActive { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
