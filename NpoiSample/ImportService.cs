using FreeSql;
using FreeSql.SqlServer;
using NPOI.XSSF.UserModel;
using NpoiSample.Models;

namespace NpoiSample;

public class ImportService
{
    private readonly IFreeSql _freeSql;

    private readonly string _filePath;

    private readonly string _sheetName;

    private readonly UnitOfWorkManager _unitOfWorkManager;

    public ImportService(IFreeSql freeSql, string filePath, string sheetName)
    {
        _freeSql = freeSql;
        _filePath = filePath;
        _sheetName = sheetName;
        _unitOfWorkManager = new UnitOfWorkManager(_freeSql);
    }

    public async Task ImportAsync(int totalCount)
    {
        var fileIds = new List<string>();
        await using var fileStream = File.OpenRead(_filePath);
        var workBook = new XSSFWorkbook(fileStream);
        var sheet = workBook.GetSheet(_sheetName);
        for (var i = 1; i <= totalCount; i++)
        {
            var row = sheet.GetRow(i);
            var volume = row.GetCell(2).StringCellValue;
            fileIds.AddRange(await _freeSql.Select<TmCaseNoticeContent>().WithLock()
                .From<TmCaseNoticeFenjian, CaseInfo>((content, fenjian, caseInfo) =>
                    content.InnerJoin(noticeContent => noticeContent.FileId == fenjian.FileId)
                        .InnerJoin(noticeContent => noticeContent.CaseId == caseInfo.CaseId))
                .Where(tuple => tuple.t3.Volume == volume 
                                && tuple.t2.IsQueshou == false 
                                && tuple.t2.FlowUser == "9b83e08a-f51e-40ed-b6ab-9bc99d6d2a97"
                                && tuple.t2.IsClose == true)
                .ToListAsync(tuple => tuple.t1.FileId));
        }

        var unitOfWork = _unitOfWorkManager.Begin();
        foreach (var fileId in fileIds)
        {
            await _freeSql.Update<TmCaseNoticeFenjian>(fileId).Set(fenjian => new TmCaseNoticeFenjian()
            {
                IsClose = false
            }).ExecuteAffrowsAsync();
        }
        unitOfWork.Commit();
    }
}