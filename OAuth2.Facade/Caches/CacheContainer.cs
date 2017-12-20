using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Facade.Caches
{
    public abstract class CacheContainer<T>
    {
        private static Dictionary<string, List<T>> _dictionary = new Dictionary<string, List<T>>();
        public T Find(Predicate<T> match)
        {
            string cacheKey = typeof(T).FullName;
            if (!_dictionary.ContainsKey(cacheKey))
            {
                FillData();
            }
            var list = _dictionary[cacheKey];
            if (list == null || list.Count <= 0)
            {
                FillData(true);
                list = _dictionary[cacheKey];
            }
            T obj = list.Find(match);
            if (object.Equals(obj, default(T)))
            {
                FillData(true);
                list = _dictionary[cacheKey];
            }
            obj = list.Find(match);
            return obj;
        }
        protected abstract List<T> LoadData();
        private void FillData(bool force = false)
        {
            string cacheKey = typeof(T).FullName;
            if (!_dictionary.ContainsKey(cacheKey))
            {
                lock (_dictionary)
                {
                    if (!_dictionary.ContainsKey(cacheKey))
                    {
                        var list = LoadData();
                        _dictionary.Add(cacheKey, list);
                    }
                }
            }
            else
            {
                if (force)
                {
                    _dictionary.Remove(cacheKey);
                    FillData(false);
                }
            }

        }
    }
}
