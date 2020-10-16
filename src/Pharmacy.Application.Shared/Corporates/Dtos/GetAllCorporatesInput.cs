using Abp.Application.Services.Dto;
namespace Pharmacy.Corporates.Dtos
{
    public class GetAllCorporatesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
