using Abp.Application.Services.Dto;

namespace Pharmacy.Items.ItemBarCodeDtos
{
    public class ItemBarCodeDto : EntityDto<int?>
    {
        public int ItemId { get; set; }
        public string BarCode { get; set; }
        public bool IsActive { get; set; }
       
    }
}
