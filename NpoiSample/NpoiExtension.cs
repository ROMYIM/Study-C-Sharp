using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace NpoiSample;

public static class NpoiExtension
{
    public static void SetValue(this IRow row, ICellStyle cellStyle, int columnNumber, string? value)
    {
        var cell = row.GetCell(columnNumber) ?? row.CreateCell(columnNumber);
        cell.CellStyle = cellStyle;
        cell.SetCellValue(value);
    }
}