using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TUBES_KPL.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public static List<Product> LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Product>();
            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Product>>(jsonData) ?? new List<Product>();
        }

        public static void SaveToFile(string filePath, List<Product> products)
        {
            var jsonData = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonData);
        }
    }
}
