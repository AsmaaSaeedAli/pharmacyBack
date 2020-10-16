using Abp.Application.Services.Dto;
namespace Pharmacy.Items.ItemBarCodeDtos
{
    public class ItemBarCodeListDto : EntityDto
    {
        public string ItemName { get; set; }
        public string BarCode { get; set; }
        public bool IsActive { get; set; }
    }
}
