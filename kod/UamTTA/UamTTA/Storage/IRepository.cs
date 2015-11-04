using System.Linq;

namespace UamTTA.Storage
{
    public interface IRepository<T> where T : class, IEntity
    {
        T FindById(int id);

        T Persist(T item);

        void Remove(T item);
    }
}