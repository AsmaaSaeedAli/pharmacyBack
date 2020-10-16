using Abp.Application.Services.Dto;

namespace Pharmacy.Jobs.Dtos
{
    public class JobDto : EntityDto<int?>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? MaxNoOfPositions { get; set; }
        public bool IsActive { get; set; }
    }
}
