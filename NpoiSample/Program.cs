// See https://aka.ms/new-console-template for more information

using FreeSql;
using NpoiSample;

const string sheetName = "Sheet3";
const string filePath = "误处理无期限官文-44份-丘婷婷.xlsx";
const string connectionString = "";


var freeSql = new FreeSqlBuilder().UseConnectionString(connectionString: connectionString, dataType: DataType.SqlServer)
    .Build();
freeSql.Aop.CurdBefore += (_, eventArgs) => Console.WriteLine(eventArgs.Sql);

var importService = new ImportService(freeSql, filePath, sheetName);
await importService.ImportAsync(44);




    

// var customerIds = await freeSql.Select<CusCustomer>().WithLock().ToListAsync(customer => customer.CustomerId);
// if (customerIds.Any())
// {
//
//
//     await using var file = File.OpenRead(filePath);
//     var workbook = new XSSFWorkbook(file);
//     var sheet = workbook.GetSheet(sheetName) ?? workbook.GetSheetAt(0);
//
//     for (var i = 0; i < customerIds.Count; i++)
//     {
//         var row = sheet.GetRow(i + 3) ?? sheet.CreateRow(i + 3);
//         var cell = row.Cells.Count <= 0 ? row.CreateCell(0) : row.Cells[0];
//         cell.SetCellValue(customerIds[i]);
//     }
//     
//     await using var fileWrite = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);
//     workbook.Write(fileWrite);
// }

