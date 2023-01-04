using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingData
{
    public class RepoExeption : Exception
    {
        public RepoExeption(string message) : base(message) { }
    }

    public class MaxItemsIsTooSmall : RepoExeption
    {
        public MaxItemsIsTooSmall(Type type) : base($"The maximum number of {type.Name} cannot be less than the existing one")
        { }
    }

    public abstract class Repo<T> : IEnumerable<T> where T : RepoItem
    {

        private List<T> _items = new();
        private int _maxItems = 0;

        public int Count { get => _items.Count; }
        public int MaxItems
        {
            get => _maxItems; set
            {
                if (value < Count)
                {
                    throw new MaxItemsIsTooSmall(typeof(T));
                }
                _maxItems = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        public void Add(T item)
        {
            item.Id = Guid.NewGuid();
            _items.Add(item);
        }

        public IEnumerable<T> Find(Predicate<T> comparator)
        {
            return _items.FindAll(comparator);
        }

        public T? FindById(Guid guid)
        {
            return _items.Find((item) => item.Id == guid);
        }

        public bool Delete(Guid guid)
        {
            T? itemToDelete = FindById(guid);
            if (itemToDelete == null) return false;
            return Delete(itemToDelete);
        }

        public bool Delete(T item)
        {
            return _items.Remove(item);
        }

    }
}
