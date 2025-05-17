using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUBES_KPL.API.Services
{
    public class GenericService<T> : IGenericService<T>
    {
        private List<T> items;

        public GenericService(List<T> initialItems)
        {
            items = initialItems;
        }

        public List<T> GetAll() => items;

        public T? GetById(int id)
        {
            var idProp = typeof(T).GetProperty("Id");
            return items.FirstOrDefault(item => (int?)idProp?.GetValue(item) == id);
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Update(int id, T item)
        {
            var idProp = typeof(T).GetProperty("Id");
            var index = items.FindIndex(existing => (int?)idProp?.GetValue(existing) == id);
            if (index != -1)
            {
                items[index] = item;
            }
        }

        public bool Remove(int id)
        {
            var item = GetById(id);
            if (item != null)
            {
                items.Remove(item);
                return true;
            }
            return false;
        }
    }
}
