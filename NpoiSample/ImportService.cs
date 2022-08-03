using FreeSql.SqlServer;

namespace NpoiSample;

public class ImportService
{
    private readonly IFreeSql _freeSql;

    private readonly string _filePath;

    private readonly string _sheetName;

    public ImportService(IFreeSql freeSql, string filePath, string sheetName)
    {
        _freeSql = freeSql;
        _filePath = filePath;
        _sheetName = sheetName;
    }

    public async Task ImportAsync()
    {
        
    }
}