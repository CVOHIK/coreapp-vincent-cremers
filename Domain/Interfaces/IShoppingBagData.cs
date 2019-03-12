using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisiness.Interfaces
{
    public interface IShoppingBagData
    {
        ICollection<ShoppingBag> GetAll();
        ShoppingBag GetById(int id);
        Task<ShoppingBag> GetByIdAsync(int id);

        ICollection<ShoppingBag> GetByCustomerId(string customerId);

        ShoppingItem GetShoppingItemById(int Id);


        void UpdateShoppingItem(ShoppingItem item);

        void AddShoppingItem(ShoppingItem item);

        void RecalcShoppingBag(int shoppingBagId);

        void AddRandomData();

        void NewShoppingBag(ShoppingBag shoppingBag);
        void RemoveShoppingItemDoubles();
        void UpdateShoppingBag(ShoppingBag shoppingBag);
    }
}
