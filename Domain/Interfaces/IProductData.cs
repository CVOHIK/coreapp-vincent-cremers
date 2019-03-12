using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisiness.Interfaces
{
    public interface IProductData
    {
        ICollection<Product> GetAllProducts();
        Product GetProductById(int Id);
        Task<ICollection<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int? id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Product product);
    }
}
