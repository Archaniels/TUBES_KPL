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

        public T? GetById(int id) =>
            items.Find(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id);

        public void Update(int id, T item)
        {
            var index = items.FindIndex(existing =>
                (int)existing.GetType().GetProperty("Id")?.GetValue(existing) == id);

            if (index != -1)
                items[index] = item;
        }

    }
