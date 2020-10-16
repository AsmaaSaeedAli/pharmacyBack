using Pharmacy.EntityFrameworkCore;

namespace Pharmacy.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly PharmacyDbContext _context;

        public InitialHostDbBuilder(PharmacyDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
