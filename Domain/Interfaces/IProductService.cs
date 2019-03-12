using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisiness.Interfaces
{
    public interface IProductService
    {
        ICollection<Product> GetAllProducts();

        Task<ICollection<Product>> GetAllProductsAsync();

        Product GetProductById(int Id);
        Task<Product> GetProductByIdAsync(int? id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Product product);
    }
}
