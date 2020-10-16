using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Pharmacy.MultiTenancy.Accounting.Dto;

namespace Pharmacy.MultiTenancy.Accounting
{
    public interface IAppInvoiceAppService
    {
        Task<AppInvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
