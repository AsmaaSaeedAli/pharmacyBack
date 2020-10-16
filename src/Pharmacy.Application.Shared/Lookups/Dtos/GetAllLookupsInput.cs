using Abp.Application.Services.Dto;
namespace Pharmacy.Lookups.Dtos
{
    public class GetAllLookupsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public int? LookupTypeId { get; set; }
    }
}
