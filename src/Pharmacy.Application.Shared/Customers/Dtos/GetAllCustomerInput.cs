using Abp.Application.Services.Dto;
namespace Pharmacy.Customers.Dtos
{
    public class GetAllCustomerInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public int GenderId { get; set; }
        public int NationalityId { get; set; }

    }
}
