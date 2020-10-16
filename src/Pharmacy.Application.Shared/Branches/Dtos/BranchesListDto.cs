using Abp.Application.Services.Dto;

namespace Pharmacy.Branches.Dtos
{
    public class BranchesListDto : EntityDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string BranchTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
