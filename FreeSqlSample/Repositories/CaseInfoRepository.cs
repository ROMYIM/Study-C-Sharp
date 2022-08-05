using FreeSql;
using FreeSqlSample.Common;
using FreeSqlSample.Models;

namespace FreeSqlSample.Repositories;

public class CaseInfoRepository : DefaultRepository<CaseInfo, string>
{
    public CaseInfoRepository(UnitOrWorkManager<DefaultDbKey> unitOrWorkManager, IFreeSql<DefaultDbKey> freeSql) : base(freeSql, unitOrWorkManager)
    {
    }
}