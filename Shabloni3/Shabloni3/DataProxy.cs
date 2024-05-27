using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shabloni3
{
    class DataProxy
    {
        private Cache cache = new Cache();

        // Метод получения данных через прокси
        public string GetData(string key)
        {
            string data = cache.GetData(key);
            if (data == null)
            {
                data = DataSource.GetData();
                cache.AddData(key, data);
            }
            return data;
        }
    }
}
