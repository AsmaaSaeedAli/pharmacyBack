using Abp.Application.Services.Dto;

namespace Pharmacy.Branches.Dtos
{
    public class GetBranchForViewDto : EntityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string CityName { get; set; }
        public string BranchTypeName { get; set; }

    }
}
