using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Buisiness.Interfaces;

namespace Buisiness.Service
{
    public class ShoppingBagService:IShoppingBagService
    {
        private readonly IShoppingBagData _shoppingBagData;
        private readonly IProductData _productData;
        public ShoppingBagService(IShoppingBagData shoppingBagData, IProductData productData)
        {
            _shoppingBagData = shoppingBagData;
            _productData = productData;
        }

        public void AddShoppingItem(int id, int productId, int addProductQty)
        {
            ShoppingBag shoppingBag = _shoppingBagData.GetById(id);
            Product P = _productData.GetProductById(productId);
            ShoppingItem shoppingItem = shoppingBag.Items
                .Where(p => p.Product.Id == P.Id).FirstOrDefault();

            if (shoppingItem!=null)
            {
                int newQty = shoppingItem.Quantity + addProductQty;
                UpdateShopingItemQuantity(shoppingItem.Id, newQty);
            }
            else
            {
                ShoppingItem si = new ShoppingItem
                {
                    Product = P,
                    Quantity = addProductQty,
                    Bag = shoppingBag
                };

                _shoppingBagData.AddShoppingItem(si);
                UpdateShoppingBagTotal(shoppingBag.Id);
            }
        }

        public ICollection<ShoppingBag> GetAllShoppingBagsByCustomerId(string customerId)
        {
            ICollection<ShoppingBag> shoppingBags = _shoppingBagData.GetByCustomerId(customerId);
            return shoppingBags;
        }

        public ShoppingBag GetBagById(int id)
        {
            ShoppingBag sb = _shoppingBagData.GetById(id);
            return sb;
        }

        public ShoppingItem GetShoppingItemById(int id)
        {
            return _shoppingBagData.GetShoppingItemById(id);
        }

        public void NewShoppingBag(ShoppingBag sb)
        {
            _shoppingBagData.NewShoppingBag(sb);
        }

        public void UpdateShopingItemQuantity(int id, int quantity)
        {
            ShoppingItem shoppingItem= _shoppingBagData.GetShoppingItemById(id);
            shoppingItem.Quantity = quantity;
            _shoppingBagData.UpdateShoppingItem(shoppingItem);
            UpdateShoppingBagTotal(shoppingItem.Bag.Id);
        }

        public void UpdateShoppingItem(ShoppingItem shoppingItem)
        {
            _shoppingBagData.UpdateShoppingItem(shoppingItem);

        }

        private void UpdateShoppingBagTotal(int id)
        {
            ShoppingBag shoppingBag = _shoppingBagData.GetById(id);
            double totalPrice = 0;
            foreach (ShoppingItem item in shoppingBag.Items)
            {
                totalPrice+=item.Quantity * item.Product.Price;
            }
            shoppingBag.TotalPrice = totalPrice;
            _shoppingBagData.UpdateShoppingBag(shoppingBag);
        }

    }
}
