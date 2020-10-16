using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pharmacy.Invoices.Dtos;

namespace Pharmacy.Invoices
{
    public interface IInvoiceAppService : IApplicationService
    {
        Task<PagedResultDto<InvoiceListDto>> GetAllInvoices(GetAllInvoiceInput input);
        Task CreateInvoice(InvoiceDto input);
        Task<InvoiceItemDto> GetItemDetails(GetItemForInvoiceInput filter);
    }
}
