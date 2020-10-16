using Abp.Application.Services.Dto;


namespace Pharmacy.Items.ItemBarCodeDtos
{
    public class GetItemBarCodeForViewDto: EntityDto
    {
        public string ItemName { get; set; }
        public string BarCode { get; set; }
        public bool IsActive { get; set; }
    }
}
