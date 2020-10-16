using Abp.Application.Services.Dto;

namespace Pharmacy.ManuFactories.Dtos
{
    public class ManuFactoryDto : EntityDto<int?>
    {
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
