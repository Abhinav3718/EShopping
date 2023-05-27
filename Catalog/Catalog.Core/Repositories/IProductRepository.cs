using Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductAsync(string id);
        Task<IEnumerable<Product>> GetProductsByNameAsync(string productName);
        Task<IEnumerable<Product>> GetProductsByBrandAsync(string brandName);
        Task<Product> CreateProductAsync(Product product);
        Task<bool> DeleteProductAsync(string id);
        Task<bool> UpdateProductAsync(Product product);
    }
}
