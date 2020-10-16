using Microsoft.EntityFrameworkCore;
using Pharmacy.EntityFrameworkCore;
using Pharmacy.Lookups;
using Shared.Helpers;
using Shared.SeedWork;
using System.Linq;

namespace Pharmacy.Migrations.Seed.Host
{
    public class DefaultLookupCreator
    {
        private readonly PharmacyDbContext _context;

        public DefaultLookupCreator(PharmacyDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLookups();
        }

        void CreateLookups()
        {
            #region Genders

            CreateLookupIfNotExist(new Lookup
            {
                Id = 1,
                Name = new LocalizedText("{\"en\":\"Male\",\"ar\":\"ذكر\"}"),
                Description = "Gender",
                Code = "M",
                LookupTypeId = LookupTypeTypes.Gender.Id,
                IsActive = true
            });

            CreateLookupIfNotExist(new Lookup
            {
                Id = 2,
                Name = new LocalizedText("{\"en\":\"Female\",\"ar\":\"أنثى\"}"),
                Description = "Gender",
                Code = "F",
                LookupTypeId = LookupTypeTypes.Gender.Id,
                IsActive = true
            });
            #endregion

            #region Currencies
            CreateLookupIfNotExist(new Lookup
            {
                Id = 10,
                Name = new LocalizedText("{\"en\":\"Rial\",\"ar\":\"ريال سعودى\"}"),
                Description = "Rial",
                Code = "SAR",
                LookupTypeId = LookupTypeTypes.Currency.Id,
                IsActive = true
            });
            CreateLookupIfNotExist(new Lookup
            {
                Id = 11,
                Name = new LocalizedText("{\"en\":\"Egyptian Pound\",\"ar\":\"جنيه مصرى\"}"),
                Description = "Egyptian Pound",
                Code = "L.E",
                LookupTypeId = LookupTypeTypes.Currency.Id,
                IsActive = true
            });
            #endregion

            #region BranchTypes
            CreateLookupIfNotExist(new Lookup
            {
                Id = 50,
                Name = new LocalizedText("{\"en\":\"Pharmacy\",\"ar\":\"صيدلية\"}"),
                Description = "Pharmacy",
                Code = "PH",
                LookupTypeId = LookupTypeTypes.BranchTypes.Id,
                IsActive = true
            });
            CreateLookupIfNotExist(new Lookup
            {
                Id = 51,
                Name = new LocalizedText("{\"en\":\"Stock\",\"ar\":\"مخزن\"}"),
                Description = "Stock",
                Code = "ST",
                LookupTypeId = LookupTypeTypes.BranchTypes.Id,
                IsActive = true
            });
            #endregion

            #region MaritalStatuses
            CreateLookupIfNotExist(new Lookup
            {
                Id = 100,
                Name = new LocalizedText("{\"en\":\"Single\",\"ar\":\"أعزب\"}"),
                Description = "Single",
                Code = "S",
                LookupTypeId = LookupTypeTypes.MaritalStatuses.Id,
                IsActive = true
            });
            CreateLookupIfNotExist(new Lookup
            {
                Id = 101,
                Name = new LocalizedText("{\"en\":\"Married\",\"ar\":\"متزوج\"}"),
                Description = "Married",
                Code = "M",
                LookupTypeId = LookupTypeTypes.MaritalStatuses.Id,
                IsActive = true
            });
            #endregion

            #region Units
            CreateLookupIfNotExist(new Lookup
            {
                Id = 200,
                Name = new LocalizedText("{\"en\":\"Box\",\"ar\":\"علبة\"}"),
                Description = "Box",
                Code = "Box",
                LookupTypeId = LookupTypeTypes.Unit.Id,
                IsActive = true
            });
            CreateLookupIfNotExist(new Lookup
            {
                Id = 201,
                Name = new LocalizedText("{\"en\":\"Bottle\",\"ar\":\"زجاجة\"}"),
                Description = "Bottle",
                Code = "Bottle",
                LookupTypeId = LookupTypeTypes.Unit.Id,
                IsActive = true
            });
            #endregion

            #region InvoiceTypes
            CreateLookupIfNotExist(new Lookup
            {
                Id = 300,
                Name = new LocalizedText("{\"en\":\"Normal\",\"ar\":\"عادية\"}"),
                Description = "Normal",
                Code = "Normal",
                LookupTypeId = LookupTypeTypes.InvoiceTypes.Id,
                IsActive = true
            });
            CreateLookupIfNotExist(new Lookup
            {
                Id = 301,
                Name = new LocalizedText("{\"en\":\"Insurance\",\"ar\":\"تأمين\"}"),
                Description = "Insurance",
                Code = "Insurance",
                LookupTypeId = LookupTypeTypes.InvoiceTypes.Id,
                IsActive = true
            });
            #endregion

            #region InvoiceStatus
            CreateLookupIfNotExist(new Lookup
            {
                Id = 400,
                Name = new LocalizedText("{\"en\":\"New\",\"ar\":\"جديدة\"}"),
                Description = "New",
                Code = "New",
                LookupTypeId = LookupTypeTypes.InvoiceStatus.Id,
                IsActive = true
            });
            CreateLookupIfNotExist(new Lookup
            {
                Id = 401,
                Name = new LocalizedText("{\"en\":\"Pending\",\"ar\":\"معلقة\"}"),
                Description = "Pending",
                Code = "Pending",
                LookupTypeId = LookupTypeTypes.InvoiceStatus.Id,
                IsActive = true
            });
            #endregion

            #region Market
            CreateLookupIfNotExist(new Lookup
            {
                Id = 500,
                Name = new LocalizedText("{\"en\":\"Saudi\",\"ar\":\"السعودى\"}"),
                Description = "Saudi",
                Code = "Saudi",
                LookupTypeId = LookupTypeTypes.Markets.Id,
                IsActive = true
            });
            CreateLookupIfNotExist(new Lookup
            {
                Id = 501,
                Name = new LocalizedText("{\"en\":\"Egyptian\",\"ar\":\"المصرى\"}"),
                Description = "Egyptian",
                Code = "Egyptian",
                LookupTypeId = LookupTypeTypes.Markets.Id,
                IsActive = true
            });
            #endregion
        }

        private void CreateLookupIfNotExist(Lookup lookup)
        {
            // check if lookup with the lookup type id does exist, and add if it didn't exist.
            var defaultLookupType = _context.Lookups.IgnoreQueryFilters().FirstOrDefault(t => t.Id == lookup.Id);
            if (defaultLookupType != null)
                return;

            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Lookups ON");

            _context.Lookups.Add(lookup);
            _context.SaveChanges();

            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Lookups OFF");
            _context.Database.CloseConnection();
        }
    }
}
