using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using TUBES_KPL.API.Models;
using TUBES_KPL.API.Services;
using TUBES_KPL.API.Controllers;

namespace TUBES_KPL.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        // Penggunaan Generics
        private readonly IGenericService<Product> _productService;

        public ProductController(IGenericService<Product> productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        //[HttpGet("{id}")]
        //// Penggunaan Parameterization
        //public IActionResult GetById(string id)
        //{
        //    var product = _productService.GetAll().FirstOrDefault(p =>   p.Id == id);
        //    if (product == null) return NotFound();
        //    return Ok(product);
        //}

        //[HttpPost]
        //[HttpPost]
        //public IActionResult Create([FromBody] Product product)
        //{
        //    _productService.Add(product);
        //    return Ok(new { message = ProductValidator.ProductAddedMessage(product.ProductName), product });
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(string id, [FromBody] Product updatedProduct)
        //{
        //    var products = _productService.GetAll();
        //    var product = products.FirstOrDefault(p => p.Id == id);
        //    if (product == null) return NotFound(new { message = ProductValidator.ProductNotFoundMessage(id) });

        //    product.Name = updatedProduct.ProductName;
        //    product.Price = updatedProduct.Price;
        //    product.Stock = updatedProduct.Stock;

        //    return Ok(new { message = ProductValidator.ProductUpdatedMessage(product.Name), product });
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(string id)
        //{
        //    var products = _productService.GetAll();
        //    var product = products.FirstOrDefault(p => p.Id == id);
        //    if (product == null) return NotFound(new { message = ProductValidator.ProductNotFoundMessage(id) });

        //    products.Remove(product);
        //    return Ok(new { message = ProductValidator.ProductDeletedMessage(product.Name) });
        //}

    }
}
