using FreeSql;

namespace FreeSqlSample.Common;

public class UnitOrWorkManager<TKey> : UnitOfWorkManager
{
    public UnitOrWorkManager(IFreeSql<TKey> freeSql) : base(freeSql)
    {
    }
}