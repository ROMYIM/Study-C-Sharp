// See https://aka.ms/new-console-template for more information

using NPOI.XSSF.UserModel;

const string sheetName = "Sheet1";
const string filePath = "";

using var file = File.OpenRead(filePath);
var workbook = new XSSFWorkbook(file);
var sheet = workbook.GetSheet(sheetName);
foreach (XSSFRow row in sheet)
{
    
}