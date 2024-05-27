using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shabloni3
{
    class Cache
    {
        private Dictionary<string, string> cache = new Dictionary<string, string>();

        // Метод добавления данных в кэш
        public void AddData(string key, string data)
        {
            cache[key] = data;
        }

        // Метод извлечения данных из кэша
        public string GetData(string key)
        {
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            return null;
        }

        // Метод удаления данных из кэша
        public void RemoveData(string key)
        {
            if (cache.ContainsKey(key))
            {
                cache.Remove(key);
            }
        }
    }
}
