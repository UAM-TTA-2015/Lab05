using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UamTTA.Storage;

namespace UamTTA.Tests
{
    public class InMemoryRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IEnumerator<int> _idGenerator;
        private IDictionary<int, T> _storage = new Dictionary<int, T>();

        public InMemoryRepository()
        {
            _idGenerator = Enumerable.Range(1, Int32.MaxValue).GetEnumerator();
        }

        public IQueryable<T> Query => _storage.Values.AsQueryable();

        public T Persist(T item)
        {
            var copy = (T)item.Clone();
            copy.Id = copy.Id.HasValue ? copy.Id : GetNextId();
            _storage[copy.Id.Value] = copy;

            return copy;
        }

        private int GetNextId()
        {
            _idGenerator.MoveNext();
            return _idGenerator.Current;
        }

        public void Remove(T item)
        {
            if (item.Id.HasValue && _storage.ContainsKey(item.Id.Value))
            {
                _storage.Remove(item.Id.Value);
            }
        }
    }
}