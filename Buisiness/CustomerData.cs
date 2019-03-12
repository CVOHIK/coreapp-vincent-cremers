using Buisiness;
using Buisiness.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{

    public class CustomerData : ICustomerData
    {

        ShopContext _context;
        
        public CustomerData(ShopContext context)
        {
            _context = context;
        }

        public bool CustomerExist(string id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        public async Task<ICollection<Customer>> GetAllAsync()
        {
            ICollection<Customer> customers = await _context.Users.ToListAsync();
            return customers;
        }

        public Customer GetById(string id)
        {
            Customer customer = _context.Customers.Where(c => c.Id == id).SingleOrDefault();
            return customer;
        }

        public Customer GetByUsername(string username)
        {
            Customer customer = _context.Customers.Where(c => c.UserName == username).SingleOrDefault();
            return customer;
        }
    }
}
 