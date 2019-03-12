using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Buisiness.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Buisiness.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerData _customerData;
        private readonly UserManager<Customer> _userManager;


        public CustomerService(ICustomerData customerData, UserManager<Customer> usermanager)
        {
            _customerData = customerData;
            _userManager = usermanager;
        }



        public ICollection<Customer> GetAll()
        {
            return (ICollection<Customer>)_userManager.Users;
        }

        public async Task<ICollection<Customer>> GetAllAsync()
        {
            return await _customerData.GetAllAsync();
        }

        public Customer GetById(string id)
        {
            return _customerData.GetById(id);
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            Customer customer = await _userManager.FindByIdAsync(id);
            return customer;
        }

        public Customer GetByUsername(string id)
        {
            return _customerData.GetByUsername(id);
        }

        public async Task updateUserAsync(Customer customer)
        {
            await _userManager.UpdateAsync(customer);
        }


        public async Task AddCustomerAsync(Customer customer)
        {
            await _userManager.CreateAsync(customer);
        }

        public async Task DeleteUserAsync(Customer customer)
        {

                await _userManager.DeleteAsync(customer);


        }

        public bool CustomerExist(string id)
        {
            return _customerData.CustomerExist(id);
        }
    }
}
