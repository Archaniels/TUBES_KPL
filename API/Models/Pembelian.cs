using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TUBES_KPL.API.Models
{
    class Pembelian
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }  // Username pembeli
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }

        public static List<Pembelian> LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Pembelian>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Pembelian>>(json) ?? new List<Pembelian>();
        }

        public static void SaveToFile(string filePath, List<Pembelian> purchases)
        {
            var json = JsonSerializer.Serialize(purchases, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

    }
}
