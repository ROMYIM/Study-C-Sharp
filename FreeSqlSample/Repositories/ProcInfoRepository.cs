using FreeSql;
using FreeSqlSample.Common;
using FreeSqlSample.Models;

namespace FreeSqlSample.Repositories;

public class ProcInfoRepository : DefaultRepository<CaseProcInfo, string>
{
    public ProcInfoRepository(IFreeSql<TestDbKey> freeSql, UnitOrWorkManager<TestDbKey> unitOrWorkManager) : base(freeSql, unitOrWorkManager)
    {
    }
}