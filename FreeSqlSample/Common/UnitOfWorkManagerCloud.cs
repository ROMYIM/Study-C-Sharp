using FreeSql;

namespace FreeSqlSample.Common;

public class UnitOfWorkManagerCloud<TKey> : IDisposable where TKey : notnull
{
    private readonly FreeSqlCloud<TKey> _freeSqlCloud;

    private readonly Dictionary<TKey, UnitOfWorkManager> _managers = new();

    public UnitOfWorkManagerCloud(FreeSqlCloud<TKey> freeSqlCloud)
    {
        _freeSqlCloud = freeSqlCloud;
    }

    public UniOfWorkCollection Begin(params TKey[] dbKeys)
    {
        var unitOfWorks = new UniOfWorkCollection(dbKeys.Length);
        
        foreach (var dbKey in dbKeys)
        {
            var freeSql = _freeSqlCloud.Change(dbKey);
            var manager = new UnitOfWorkManager(freeSql);
            _managers.TryAdd(dbKey, manager);

            var unitOfWork = manager.Begin();
            unitOfWorks.Add(unitOfWork);
        }

        return unitOfWorks;
    }

    public void Dispose()
    {
        foreach (var (_, manager) in _managers)
        {
            manager.Dispose();
        }
        _managers.Clear();
    }
}