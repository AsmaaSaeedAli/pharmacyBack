using Abp.Application.Services.Dto;

namespace Pharmacy.Lookups.Dtos
{
   public class LookupDto : EntityDto<int?>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int LookupTypeId { get; set; }
    }
}
