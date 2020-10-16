using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Pharmacy.Address;
using Shared.SeedWork;

namespace Pharmacy.Corporates
{
    public class Corporate : FullAuditedEntity , IMustHaveTenant, IPassivable
    {
        public int TenantId { get; set; }
        public LocalizedText Name { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Logo { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
