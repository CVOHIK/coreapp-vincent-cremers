using Buisiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Buisiness.Interfaces;

namespace DataAccess
{
    
   
    public class ShoppingBagData : IShoppingBagData
    {
        ShopContext _context;
        Random r = new Random();

        public ShoppingBagData(ShopContext context)
        {
            _context = context;
            Random r = new Random();
        }
        public ICollection<ShoppingBag> GetAll()
        {
            return _context.ShoppingBags.ToList();
        }

        public ICollection<ShoppingBag> GetByCustomerId(string customerId)
        {
            ICollection<ShoppingBag> sbList = _context.ShoppingBags
                .Where(s => s.Customer.Id == customerId)
                .Include(si=> si.Items)
                .ToList();

            return sbList;
        }

        public ShoppingBag GetById(int id)
        {
            ShoppingBag sb = _context.ShoppingBags
                .Include(c => c.Customer)
                .Include(si=> si.Items)
                .ThenInclude(sp => sp.Product)
                .Where(s => s.Id == id).Single();
            return sb;
        }


        public async Task<ShoppingBag> GetByIdAsync(int id)
        {
           ShoppingBag sb = await _context.ShoppingBags
                .Include(c => c.Customer)
                .Include(si => si.Items)
                .ThenInclude(sp => sp.Product)
                .Where(s => s.Id == id).SingleOrDefaultAsync();

            return sb;
        }

        public ShoppingItem GetShoppingItemById(int id)
        {
            ShoppingItem si = _context.ShoppingItems
                .Include(sp => sp.Product)
                .Include(sb => sb.Bag)
                .Where(i => i.Id==id)
                .Single();
            return si;
        }

        public void RecalcShoppingBag(int shoppingBagId)
        {
            ShoppingBag sb = GetById(shoppingBagId);
            double total = 0;
            foreach (ShoppingItem shoppingItem in sb.Items)
            {
                total += shoppingItem.Quantity * shoppingItem.Product.Price;
            }
            sb.TotalPrice = total;
            _context.ShoppingBags.Update(sb);
            _context.SaveChanges();
        }

        public void UpdateShoppingItem(ShoppingItem item)
        {

            if (item!=null)
            {
                _context.ShoppingItems.Update(item);
            }
            _context.SaveChanges();

        }

        public void AddRandomData()
        {
            ShoppingBag newBag = new ShoppingBag();


            newBag.Customer = PickRandomCustomer();
            newBag.Date = DateTime.Now;

            int randomshoppingitems = r.Next(1, 5);
            for (int i = 0; i < randomshoppingitems; i++)
            {
                Product p;

                do
                {
                    p = PickRandomProduct();
                } while (newBag.Items!=null &&
                         newBag.Items.Where(item => item.Product.Id==p.Id).Any());

                
                int randomQty = r.Next(1, 11);
                ShoppingItem s = new ShoppingItem
                {
                    Bag = newBag,
                    Product = p,
                    Quantity = randomQty
                };
                _context.ShoppingItems.Add(s);
                newBag.Items.Add(s);
            }

            foreach (ShoppingItem item in newBag.Items)
            {
                newBag.TotalPrice += item.Quantity * item.Product.Price;
            }

            _context.ShoppingBags.Add(newBag);
            _context.SaveChanges();            

        }

        private Customer PickRandomCustomer()
        {
            int totalCustomers = _context.Customers.Count();
            int  SkipRandomCustomerRecords = r.Next(0, totalCustomers);
            Customer customer =
                _context.Customers.OrderBy(c => c.Id)
                .Skip(SkipRandomCustomerRecords)
                .FirstOrDefault();
          

            return customer;
            
        }
        private  Product PickRandomProduct()
        {
            int totalProducts = _context.Products.Count();
            int skipRandomProductRecords = r.Next(0, totalProducts);

            Product product = _context.Products.OrderBy(p => p.Id)
                .Skip(skipRandomProductRecords)
                .FirstOrDefault();

            return product;

        }

        public void NewShoppingBag(ShoppingBag shoppingBag)
        {
            this._context.ShoppingBags.Add(shoppingBag);
            _context.SaveChanges();
        }

        public void AddShoppingItem(ShoppingItem item)
        {
            _context.ShoppingItems.Add(item);
            _context.SaveChanges();
        }

        public void RemoveShoppingItemDoubles() {

            List<ShoppingItem> doubleItems = _context.ShoppingItems.GroupBy(i => new { i.Bag, i.Product }).SelectMany(item => item.Skip(1)).ToList();
            foreach (ShoppingItem item in doubleItems)
            {
                _context.ShoppingItems.Remove(item);
            }
            _context.SaveChanges();
            

        }

        public void UpdateShoppingBag(ShoppingBag shoppingBag)
        {
            _context.ShoppingBags.Update(shoppingBag);
            _context.SaveChanges();
        }
    }
}
