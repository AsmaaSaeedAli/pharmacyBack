using Shared.Abstractions;
using System.Collections.Generic;

namespace Shared.Helpers
{
    public class LookupTypeTypes : BaseEnumeration
    {
        public static LookupTypeTypes Gender = new LookupTypeTypes(1, "Gender");
        public static LookupTypeTypes Currency = new LookupTypeTypes(2, "Currency");
        public static LookupTypeTypes BranchTypes = new LookupTypeTypes(3, "BranchTypes");
        public static LookupTypeTypes MaritalStatuses = new LookupTypeTypes(4, "Marital Statuses");
        public static LookupTypeTypes Unit = new LookupTypeTypes(5, "Unit");
        public static LookupTypeTypes InvoiceTypes = new LookupTypeTypes(6, "Invoice Types");
        public static LookupTypeTypes InvoiceStatus = new LookupTypeTypes(7, "Invoice Statuses");
        public static LookupTypeTypes Markets = new LookupTypeTypes(8, "Markets");
        protected LookupTypeTypes()
        {
        }

        protected LookupTypeTypes(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<LookupTypeTypes> List()
        {
            return new[]
            {
                Gender, Currency, BranchTypes,
                MaritalStatuses,Unit,InvoiceTypes,InvoiceStatus,
                Markets
            };
        }
    }
}
