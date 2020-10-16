using Pharmacy.EntityFrameworkCore;
using Pharmacy.Lookups;
using Shared.Helpers;
using Shared.SeedWork;

namespace Pharmacy.Migrations.Seed.Host
{
    public class DefaultLookupTypeCreator
    {
        private readonly PharmacyDbContext _context;

        public DefaultLookupTypeCreator(PharmacyDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLookupTypes();
        }

        void CreateLookupTypes()
        {
            Seed(new LookupType { Id = LookupTypeTypes.Gender.Id, Name = new LocalizedText("{\"en\":\"Genders\",\"ar\":\"الأنواع\"}"), Description = "Gender", IsSystem = true });
            Seed(new LookupType { Id = LookupTypeTypes.Currency.Id, Name = new LocalizedText("{\"en\":\"Currencies\",\"ar\":\"العملات\"}"), Description = "Currencies", IsSystem = false });
            Seed(new LookupType { Id = LookupTypeTypes.BranchTypes.Id, Name = new LocalizedText("{\"en\":\"Branch Types\",\"ar\":\"أنواع الفروع\"}"), Description = "Branch Types", IsSystem = false });
            Seed(new LookupType { Id = LookupTypeTypes.MaritalStatuses.Id, Name = new LocalizedText("{\"en\":\"Marital Statuses\",\"ar\":\"الحالات الاجتماعية\"}"), Description = "Marital Statuses", IsSystem = false });
            Seed(new LookupType { Id = LookupTypeTypes.Unit.Id, Name = new LocalizedText("{\"en\":\"Units\",\"ar\":\"الوحدات\"}"), Description = "Units", IsSystem = false });
            Seed(new LookupType { Id = LookupTypeTypes.InvoiceTypes.Id, Name = new LocalizedText("{\"en\":\"Invoice Types\",\"ar\":\"انواع الفواتير\"}"), Description = "Invoice Types", IsSystem = false });
            Seed(new LookupType { Id = LookupTypeTypes.InvoiceStatus.Id, Name = new LocalizedText("{\"en\":\"Invoice Statuses\",\"ar\":\"حالات الفواتير\"}"), Description = "Invoice Statuses", IsSystem = false });
            Seed(new LookupType { Id = LookupTypeTypes.Markets.Id, Name = new LocalizedText("{\"en\":\"Markets\",\"ar\":\"الأسواق\"}"), Description = "Markets", IsSystem = false });
        }

        private void Seed(LookupType lookupType)
        {
            if (_context.LookupTypes.Find(lookupType.Id) == null)
                _context.LookupTypes.Add(lookupType);
        }
    }
}
