using NPOI.SS.UserModel;
using NpoiSample.Models;

namespace NpoiSample;

public static class CustomerFollowUserExtension
{
    public static void SetExcelFollowUserValue(this CustomerFollowUserInfoDto dto, IRow row, ICellStyle cellStyle, int columnIndex)
    {
        if (dto.CaseDirection == "II")
        {
            
            if (string.Equals("ay", dto.CustomerUserType, StringComparison.OrdinalIgnoreCase))
            {
                row.SetValue(cellStyle, columnIndex, dto.GetCaseSourceClass);
                row.SetValue(cellStyle, columnIndex + 1, dto.UserName);
            }

            else if (string.Equals("ga", dto.CustomerUserType, StringComparison.OrdinalIgnoreCase))
            {
                row.SetValue(cellStyle, columnIndex + 2, dto.UserName);
            }
        }
        
        else if (dto.CaseDirection == "IO")
        {

            if (string.Equals("ay", dto.CustomerUserType, StringComparison.OrdinalIgnoreCase))
            {
                row.SetValue(cellStyle, columnIndex + 3, dto.GetCaseSourceClass);
                row.SetValue(cellStyle, columnIndex + 4, dto.UserName);
            }

            else if (string.Equals("ga", dto.CustomerUserType, StringComparison.OrdinalIgnoreCase))
            {
                row.SetValue(cellStyle, columnIndex + 5, dto.UserName);
            }
        }
        
        else if (dto.CaseDirection == "OI")
        {
            if (string.Equals("ay", dto.CustomerUserType, StringComparison.OrdinalIgnoreCase))
            {
                row.SetValue(cellStyle, columnIndex + 6, dto.GetCaseSourceClass);
                row.SetValue(cellStyle, columnIndex + 7, dto.UserName);
            }

            else if (string.Equals("ga", dto.CustomerUserType, StringComparison.OrdinalIgnoreCase))
            {
                row.SetValue(cellStyle, columnIndex + 8, dto.UserName);
            }
        }
        
        else if (dto.CaseDirection == "OO")
        {

            if (string.Equals("ay", dto.CustomerUserType, StringComparison.OrdinalIgnoreCase))
            {
                row.SetValue(cellStyle, columnIndex + 9, dto.GetCaseSourceClass);
                row.SetValue(cellStyle, columnIndex + 10, dto.UserName);
            }

            else if (string.Equals("ga", dto.CustomerUserType, StringComparison.OrdinalIgnoreCase))
            {
                row.SetValue(cellStyle, columnIndex + 11, dto.UserName);
            }
        }
    }
}