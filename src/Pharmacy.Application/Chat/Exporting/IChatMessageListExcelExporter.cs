using System.Collections.Generic;
using Pharmacy.Chat.Dto;
using Pharmacy.Dto;

namespace Pharmacy.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(List<ChatMessageExportDto> messages);
    }
}
