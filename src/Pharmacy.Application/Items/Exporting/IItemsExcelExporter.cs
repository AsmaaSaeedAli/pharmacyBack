using Pharmacy.Dto;
using System.Collections.Generic;
using Pharmacy.Items.ItemBarCodeDtos;
using Pharmacy.Items.ItemDtos;
using Pharmacy.Items.ItemPriceDtos;
using Pharmacy.Items.ItemQuantityDtos;

namespace Pharmacy.Items.Exporting
{
  public  interface IItemsExcelExporter
    {
        FileDto ExportItemsToFile(List<ItemListDto> items);
        FileDto ExportItemBarCodesToFile(List<ItemBarCodeListDto> itemBarCodes);
        FileDto ExportItemPricesToFile(List<ItemPriceListDto> itemPrices);
        FileDto ExportItemQuantitiesToFile(List<ItemQuantityListDto> itemQuantities);
    }
}
