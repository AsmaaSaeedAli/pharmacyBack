using Abp.Application.Services.Dto;
namespace Pharmacy.Lookups.Dtos
{
   public class LookupListDto : EntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }

    }
}
