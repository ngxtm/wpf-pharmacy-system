using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Entities;

namespace BLL.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
        Task<bool> UpdateStockAsync(int productId, int quantity);
    }
} 