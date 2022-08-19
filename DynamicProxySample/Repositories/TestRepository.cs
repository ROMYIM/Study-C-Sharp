using DynamicProxySample.Models;
using FreeSql;

namespace DynamicProxySample.Repositories;

public class TestRepository : DefaultRepository<Test, long>
{
    public TestRepository(IFreeSql freeSql, UnitOfWorkManager uowManger) : base(freeSql, uowManger)
    {
    }
}