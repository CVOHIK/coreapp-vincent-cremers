using Buisiness;
using Buisiness.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{


    public class ProductData: IProductData
    {
        ShopContext _context;
        public ProductData(ShopContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public ICollection<Product> GetAllProducts()
        {
            List<Product> products = _context.Products.OrderBy(p => p.Name).ToList();
            return products;
        }

        public async Task<ICollection<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();

        }

        public Product GetProductById(int Id)
        {
            Product product = _context.Products.Where(p => p.Id == Id).SingleOrDefault();
            return product;
        }

        public async Task<Product> GetProductByIdAsync(int? id)
        {
            return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
