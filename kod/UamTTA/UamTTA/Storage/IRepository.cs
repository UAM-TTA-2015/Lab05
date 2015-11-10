using System.Collections.Generic;

namespace UamTTA.Storage
{
    public interface IRepository<T> where T : class, IEntity
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Take(int count);

        T FindById(int id);

        T Persist(T item);

        void Remove(T item);
    }
}