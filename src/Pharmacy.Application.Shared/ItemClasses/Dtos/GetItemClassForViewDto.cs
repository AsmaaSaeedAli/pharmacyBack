using Abp.Application.Services.Dto;


namespace Pharmacy.ItemClasses.Dtos
{
    public class GetItemClassForViewDto : EntityDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
        public int ItemNumberStart { get; set; }
    }
}
