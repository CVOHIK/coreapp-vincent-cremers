using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Buisiness.Interfaces;

namespace Buisiness.Service
{

    public class ProductService:IProductService
    {
        private readonly IProductData _productData;
    
        public ProductService(IProductData productData)
        {
            _productData = productData;
        }

        public async Task AddAsync(Product product)
        {
            await _productData.AddAsync(product);
        }

        public ICollection<Product> GetAllProducts()
        {
            return _productData.GetAllProducts();
        }

        public async Task<ICollection<Product>> GetAllProductsAsync()
        {
            return await  _productData.GetAllProductsAsync();
        }

        public Product GetProductById(int Id)
        {
            return _productData.GetProductById(Id);
        }

        public async Task<Product> GetProductByIdAsync(int? id)
        {
            return await _productData.GetProductByIdAsync(id);
        }

        public async Task RemoveAsync(Product product)
        {
            await _productData.RemoveAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _productData.UpdateAsync(product);
        }
    }
}
