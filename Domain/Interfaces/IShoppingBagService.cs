using System;
using System.Collections.Generic;
using System.Text;

namespace Buisiness.Interfaces
{
    public interface IShoppingBagService
    {
        ICollection<ShoppingBag> GetAllShoppingBagsByCustomerId(string customerId);
        ShoppingBag GetBagById(int id);
        ShoppingItem GetShoppingItemById(int id);
        void UpdateShoppingItem(ShoppingItem shoppingItem);
        void UpdateShopingItemQuantity(int id, int quantity);
        void NewShoppingBag(ShoppingBag sb);
        void AddShoppingItem(int id, int addProductId, int addProductQty);
    }
}
