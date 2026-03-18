using Microsoft.AspNetCore.Mvc;
using SmartInventory.API.Models;
using SmartInventory.API.Interfaces;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region CRUD

        // Gets all products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        // Adds product
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (product == null)
                return BadRequest("Product cannot be null");

            var result = await _productService.AddProduct(product);
            return Ok(result);
        }

        // Get product by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
                return NotFound($"Product with id {id} not found");

            return Ok(product);
        }

        // Update product by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            if (updatedProduct == null)
                return BadRequest("Invalid data");

            var result = await _productService.UpdateProduct(id, updatedProduct);

            if (result == null)
                return NotFound($"Product with id {id} not found");

            return Ok(result);
        }

        // Delete product by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProduct(id);

            if (!deleted)
                return NotFound($"Product with id {id} not found");

            return Ok("Deleted Successfully");
        }

        #endregion CRUD

        #region RESTOCK ALERT

        [HttpGet("restock-alert")]
        public async Task<IActionResult> GetRestockAlert()
        {
            var lowStockProducts = await _productService.GetLowStockProducts();

            if (lowStockProducts == null || !lowStockProducts.Any())
                return Ok("No products need restocking");

            return Ok(lowStockProducts);
        }

        #endregion RESTOCK ALERT
    }
}