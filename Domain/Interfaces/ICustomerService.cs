
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buisiness.Interfaces
{
    public interface  ICustomerService
    {
        Task<ICollection<Customer>> GetAllAsync();
        ICollection<Customer> GetAll();
         Task<Customer> GetByIdAsync(string id);
        Customer GetById(string id);
        Customer GetByUsername(string id);
        Task AddCustomerAsync(Customer customer);
        Task updateUserAsync(Customer customer);
        Task DeleteUserAsync(Customer customer);
        bool CustomerExist(string id);
    }
}
