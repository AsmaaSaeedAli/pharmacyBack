using Abp.Dependency;
using GraphQL;
using GraphQL.Types;
using Pharmacy.Queries.Container;

namespace Pharmacy.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IDependencyResolver resolver) :
            base(resolver)
        {
            Query = resolver.Resolve<QueryContainer>();
        }
    }
}