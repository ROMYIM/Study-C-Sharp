using System.Collections;
using FreeSql;

namespace FreeSqlSample.Common;

public class UniOfWorkCollection : ICollection<IUnitOfWork>, IDisposable
{
    private readonly List<IUnitOfWork> _unitOfWorks;

    public UniOfWorkCollection(int capacity)
    {
        _unitOfWorks = new List<IUnitOfWork>(capacity);
    }

    public void Commit() => _unitOfWorks.ForEach(unitOfWork => unitOfWork.Commit());

    public void Rollback() => _unitOfWorks.ForEach(unitOfWork => unitOfWork.Rollback());
    
    public void Dispose()
    {
        Rollback();
        Clear();
    }

    public IEnumerator<IUnitOfWork> GetEnumerator()
    {
        return _unitOfWorks.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(IUnitOfWork item)
    {
        _unitOfWorks.Add(item);
    }

    public void Clear()
    {
        _unitOfWorks.Clear();
    }

    public bool Contains(IUnitOfWork item)
    {
        return _unitOfWorks.Contains(item);
    }

    public void CopyTo(IUnitOfWork[] array, int arrayIndex)
    {
        _unitOfWorks.CopyTo(array, arrayIndex);
    }

    public bool Remove(IUnitOfWork item)
    {
        return _unitOfWorks.Remove(item);
    }

    public int Count => _unitOfWorks.Count;
    public bool IsReadOnly => false;
    
}