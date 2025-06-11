using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TUBES_KPL.API.Models;

namespace TUBES_KPL.API.Services

{
    public class GenericService<T> : IGenericService<T>
    {
        private readonly List<T> _items = new List<T>();
        private List<Product> productsList;
        private List<Donation> donationList;
        private List<Pembelian> purchaseList;
        private readonly string _filePath;

        public List<T> GetAll()
        {
            return _items;
        }

        public T? GetById(int id)
        {
            if (id < 0 || id >= _items.Count)
                return default;

            return _items[id];
        }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _items.Add(item);
        }

        public void Update(int id, T item)
        {
            if (id < 0 || id >= _items.Count)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _items[id] = item;
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= _items.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            _items.RemoveAt(index);
        }

        public void UpdateByString(string id, T item)
        {
            var existing = GetByIdByString(id);
            if (existing != null)
            {
                _items.Remove(existing);
                _items.Add(item);
                SaveToFile();
            }
        }

        public void DeleteByString(string id)
        {
            var existing = GetByIdByString(id);
            if (existing != null)
            {
                _items.Remove(existing);
                SaveToFile();
            }
        }

        public T GetByIdByString(string id)
        {
            // Asumsi setiap T punya property "Id" bertipe string
            return _items.FirstOrDefault(item =>
                item.GetType().GetProperty("Id")?.GetValue(item)?.ToString() == id);
        }

        private List<T> LoadFromFile()
        {
            if (!File.Exists(_filePath))
                return new List<T>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

    //    public GenericService(List<Product> products)
    //    {
    //        this.productsList = products;
    //        if (typeof(T) == typeof(Product) && products != null)
    //        {
    //            _items.AddRange(products.Cast<T>());
    //        }
    //    }

    //    public GenericService(List<Donation> donationList)
    //    {
    //        this.donationList = donationList;
    //    if (typeof(T) == typeof(Donation) && donationList != null)
    //    {
    //        _items.AddRange(donationList.Cast<T>());
    //    }
    //    }

    //    public GenericService(List<Pembelian> purchaseList)
    //{
    //    this.purchaseList = purchaseList;
    //    if (typeof(T) == typeof(Pembelian) && purchaseList != null)
    //    {
    //        _items.AddRange(purchaseList.Cast<T>());
    //    }
    //}
        public GenericService(List<T> items)
        {
            if (items != null)
            {
                _items.AddRange(items);
            }
        }

    }
}