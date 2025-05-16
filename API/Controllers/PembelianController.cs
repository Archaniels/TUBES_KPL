using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUBES_KPL.API.Models;
using TUBES_KPL.API.Services;

namespace TUBES_KPL.API.Controllers
{
    [ApiController]
    [Route("api/pembelian")]
    public class PembelianController : ControllerBase
    {
        private readonly IGenericService<Product> _productService;
        private const string FilePath = "product.json";

        public PembelianController()
        {
            var products = Product.LoadFromFile(FilePath);
            _productService = new GenericService<Product>(products);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return _productService.GetAll();
        }

        [HttpPost("{id}/{quantity}")]
        public ActionResult Pembelian(int id, int quantity)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound("Product tidak ditemukkan");

            if (product.Stock < quantity)
                return BadRequest("Stok tidak cukup");

            product.Stock -= quantity;
            _productService.Update(id, product);

            // Save updated products to file
            var updatedProducts = _productService.GetAll();
            Product.SaveToFile(FilePath, updatedProducts);

            return Ok(new { message = "Pembelian berhasil", product, quantity });
        }
    }
}
