using Pharmacy.EntityFrameworkCore;
using Pharmacy.Items;
using Pharmacy.Items.ItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pharmacy.Web.Helpers
{
    public static class Search
    {
        public static IQueryable<Item> SearchItemName(this IQueryable<Item> items, GetAllItemInput input)
        {
            var filters = input.Filter?.Split('-');

            Expression<Func<Item, bool>> predicate = p => p.IsActive == true;

            if (filters != null && filters.Length > 0)
            {
                if (filters.Length == 1)
                {
                    predicate.And(i => i.Name.CurrentCultureText.Contains(filters[0]));

                }
                if (filters.Length == 2)
                {
                    predicate.And(i => i.Name.CurrentCultureText.StartsWith(filters[0]));
                    predicate.And(i => i.Name.CurrentCultureText.Contains(filters[1]));


                }
                if (filters.Length == 3)
                {
                    predicate.And(i => i.Name.CurrentCultureText.StartsWith(filters[0]));
                    predicate.And(i => i.Name.CurrentCultureText.Contains(filters[1]));
                    predicate.And(i => i.Name.CurrentCultureText.EndsWith(filters[2]));

                }

            }
            items = items.Where(predicate);
            return items;
        }
    }
}
