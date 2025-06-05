using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUBES_KPL.API.Controllers
{
    public static class ProductValidator
    {
        public static string ProductNotFoundMessage(string id) =>
            $"Produk dengan ID {id} tidak ditemukan.";

        public static string ProductAddedMessage(string name) =>
            $"Produk '{name}' berhasil ditambahkan.";

        public static string ProductUpdatedMessage(string name) =>
            $"Produk '{name}' berhasil diupdate.";

        public static string ProductDeletedMessage(string name) =>
            $"Produk '{name}' berhasil dihapus.";
    }
}
