using Abp.Application.Services.Dto;
namespace Pharmacy.Branches.Dtos
{
    public class BranchDto : EntityDto<int?>
    {
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int? CityId { get; set; }
        public int? BranchTypeId { get; set; }
    }
}
