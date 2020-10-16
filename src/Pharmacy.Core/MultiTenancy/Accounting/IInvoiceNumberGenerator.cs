using System.Threading.Tasks;
using Abp.Dependency;

namespace Pharmacy.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}