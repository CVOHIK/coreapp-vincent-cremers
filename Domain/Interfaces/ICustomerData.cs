using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisiness.Interfaces
{
    public interface ICustomerData
    {
        Task<ICollection<Customer>> GetAllAsync();
        Customer GetById(string id);
        Customer GetByUsername(string id);
        bool CustomerExist(string id);
    }
}
