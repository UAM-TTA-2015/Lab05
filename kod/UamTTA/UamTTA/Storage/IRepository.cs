using System.Linq;

namespace UamTTA.Storage
{
    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> Query { get; }

        T Persist(T item);

        void Remove(T item);
    }
}