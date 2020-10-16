using Pharmacy.Items.ItemBarCodeDtos;
using Pharmacy.Items.ItemDtos;
using Pharmacy.Items.ItemPriceDtos;
using Pharmacy.Items.ItemQuantityDtos;
using Pharmacy.DataExporting.Excel.EpPlus;
using Pharmacy.Dto;
using Pharmacy.Storage;
using System.Collections.Generic;
namespace Pharmacy.Items.Exporting
{
    public class ItemsExcelExporter : EpPlusExcelExporterBase, IItemsExcelExporter
    {
        public ItemsExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        public FileDto ExportItemsToFile(List<ItemListDto> items)
        {
            return CreateExcelPackage(
                L("Items") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Items"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("ItemNumber"), L("NameAr"),L("NameEn"), L("Description"), L("ItemClassName"), L("CategoryName"), L("SubCategoryName"),L("ManuFactoryName"), L("BarCode"), L("IsActive"));
                    AddObjects(sheet, 2, items, _ => _.ItemNumber, _ => _.NameAr,_=>_.NameEn, _ => _.Description, _ => _.ItemClassName, _ => _.CategoryName, _ => _.SubCategoryName,_=>_.ManuFactoryName, _ => _.BarCode, _ => _.IsActive);
                    for (int i = 1; i <=9; i++)
                        sheet.Column(i).AutoFit();
                });
        }
        public FileDto ExportItemBarCodesToFile(List<ItemBarCodeListDto> itemBarCodes)
        {
            return CreateExcelPackage(
                L("ItemBarCodes") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ItemBarCodes"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet,L("ItemName"),L("BarCode"),L("IsActive"));
                    AddObjects(sheet, 2, itemBarCodes, _ => _.ItemName, _ => _.BarCode,_ => _.IsActive);
                    for (int i = 1; i <= 3; i++)
                        sheet.Column(i).AutoFit();
                });
        }

        public FileDto ExportItemPricesToFile(List<ItemPriceListDto> itemPrices)
        {
            return CreateExcelPackage(
                L("ItemPrices") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ItemPrices"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("ItemName"), L("ProductionDate"), L("ExpiringDate"), L("Price"), L("CorporateName"), L("IsActive"));
                    AddObjects(sheet, 2, itemPrices, _ => _.ItemName, _ => _.Price, _ => _.IsActive);
                    for (int i = 1; i <= 6; i++)
                        sheet.Column(i).AutoFit();
                });
        }

        public FileDto ExportItemQuantitiesToFile(List<ItemQuantityListDto> itemQuantities)
        {
            return CreateExcelPackage(
                L("ItemQuantities") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ItemQuantities"));
                    sheet.OutLineApplyStyle = true;
                    AddHeader(sheet, L("BranchName"), L("ItemName"), L("Quantity"), L("UnitName"));
                    AddObjects(sheet, 2, itemQuantities, _ => _.BranchName, _ => _.ItemName, _ => _.Quantity, _ => _.UnitName);
                    for (int i = 1; i <= 4; i++)
                        sheet.Column(i).AutoFit();
                });
        }
    }
}
