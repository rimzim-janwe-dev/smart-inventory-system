using SmartInventory.API.Models;

namespace SmartInventory.API.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(int id, Product product);
        Task<bool> DeleteProduct(int id);
        Task<List<Product>> GetLowStockProducts();
    }
}