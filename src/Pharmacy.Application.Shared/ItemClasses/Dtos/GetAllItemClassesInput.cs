using Abp.Application.Services.Dto;

namespace Pharmacy.ItemClasses.Dtos
{
    public class GetAllItemClassesInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
