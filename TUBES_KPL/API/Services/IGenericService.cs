using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUBES_KPL.API.Services
{
    public interface IGenericService<T>
    {
        List<T> GetAll();
        T? GetById(int id);
        void Add(T item);
        void Update(int id, T item);
        void Remove(int index);
        void UpdateByString(string id, T item); // Tambahkan ini
        void DeleteByString(string id); // Tambahkan ini
        T GetByIdByString(string id); // Tambahkan ini
    }
}
