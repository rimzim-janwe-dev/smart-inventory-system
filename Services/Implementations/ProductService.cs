using Microsoft.EntityFrameworkCore;
using SmartInventory.API.Data;
using SmartInventory.API.Models;
using SmartInventory.API.Interfaces;

namespace SmartInventory.API.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        // Get all products
        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching products", ex);
            }
        }

        // Get product by Id
        public async Task<Product> GetProductById(int id)
        {
            try
            {
                return await _context.Products.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching product by id", ex);
            }
        }

        // Add new product
        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                product.CreatedAt = DateTime.Now;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding product", ex);
            }
        }

        // Update product
        public async Task<Product> UpdateProduct(int id, Product updatedProduct)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                    return null;

                product.Name = updatedProduct.Name;
                product.Quantity = updatedProduct.Quantity;
                product.Threshold = updatedProduct.Threshold;

                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating product", ex);
            }
        }

        // Delete product
        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                    return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting product", ex);
            }
        }

        // Get low stock products (Restock Alert)
        public async Task<List<Product>> GetLowStockProducts()
        {
            try
            {
                return await _context.Products
                    .Where(p => p.Quantity < p.Threshold)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching low stock products", ex);
            }
        }
    }
}