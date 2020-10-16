using Pharmacy.EntityFrameworkCore;
using Pharmacy.Items;
using Pharmacy.Items.ItemDtos;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Pharmacy.Web.Helpers
{
    public static class Search
    {
        public static IQueryable<Item> SearchItem(this IQueryable<Item> items, GetAllItemInput input)
        {
            // IQueryable<Item> itemCollection ;
            var filters = input.Filter.Split('-');

            Expression<Func<Item, bool>> predicate = p => p.IsActive == true;

            if (filters.Length > 0)
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
                items = items.Where(predicate);
            }
            else
            {
               // var items2 = ObjectMapper.Map<Item>(input);

                //throw new UserFriendlyException($"Search text area is empty ");
            }
            return items;

            // var predicat = PredicateBuilder.True<ite>();

            // predicate = predicate.And(m => m.ToString().StartsWith(filters[0]));

            //predicate = obj.Name2.Length > 0 ? predicate.And(m => m.Name.Contains(obj.Name) && m.Name.EndWith(obj.Name)) : predicate;
            //predicate = obj.Name3.Length > 0 ? predicate.And(m => m.Name.WndWith(obj.Name) && m.Name.EndWith(obj.Name) && m.Name.EndWith(obj.Name)) : predicate;

            //var filters = input.Filter.Split('%');
            //Expression<Func<Student, bool>> pridicate = p => p.IsActive == true;
            //if (filters.Length == 1)
            //{
            //    pridicate.And(p => p.FirstName.Contains(filters[0]));

            //}
            //if (filters.Length == 2)
            //{
            //    pridicate.And(p => p.FirstName.StartsWith(filters[0]));
            //    pridicate.And(p => p.FirstName.Contains(filters[1]));
            //}
            //if (filters.Length == 3)
            //{
            //    pridicate.And(p => p.FirstName.StartsWith(filters[0]));
            //    pridicate.And(p => p.FirstName.Contains(filters[1]));
            //    pridicate.And(p => p.FirstName.EndsWith(filters[2]));

            //}











        }
    }
}
