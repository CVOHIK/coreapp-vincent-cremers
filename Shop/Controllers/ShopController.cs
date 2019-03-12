using System;
using Microsoft.AspNetCore.Mvc;
using Buisiness;
using Microsoft.AspNetCore.Mvc.Rendering;
using Buisiness.Interfaces;
using Buisiness.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {

        private readonly IProductService _productService;
        private readonly IShoppingBagService _ShoppingBagService;
        private readonly ICustomerService _customerService;

        public ShopController( 
            IProductService productService, 
            IShoppingBagService ShoppingBagService,
            ICustomerService customerService)
        {
            _productService = productService;
            _ShoppingBagService = ShoppingBagService;
            _customerService = customerService;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult DetailCustomer(string id)
        {


            DetailCustomerVM detailCustomerVM = new DetailCustomerVM
            {
                Customer = _customerService.GetById(id),
                ShoppingBags = _ShoppingBagService.GetAllShoppingBagsByCustomerId(id)
            };
            detailCustomerVM.recalculateBags();
            return View(detailCustomerVM);

        }
        [Authorize]
        public IActionResult DetailBag(int id)
        {
            DetailBagVM detailBagVM = new DetailBagVM
            {
                ShoppingBag = _ShoppingBagService.GetBagById(id),
                Products = new SelectList(_productService.GetAllProducts(), "Id", "Name")

            };
            detailBagVM.Customer = _customerService.GetById(detailBagVM.ShoppingBag.Customer.Id);
            detailBagVM.ShoppingBag.Recalculate();

            return View(detailBagVM);
        }

        [Authorize]
        public IActionResult EditItem(int id)
        {
            ShoppingItem shoppingItem = _ShoppingBagService.GetShoppingItemById(id);
            return View(shoppingItem);
        }
        [Authorize]
        [HttpPost]
        public  IActionResult EditItem([Bind("Id,Quantity,Bag")] ShoppingItem shoppingItem)

        {
            _ShoppingBagService.UpdateShopingItemQuantity(shoppingItem.Id, shoppingItem.Quantity);
            return RedirectToAction("DetailBag", new { id = shoppingItem.Bag.Id });
        }
        [Authorize]
        public IActionResult NewShoppingBag(string id)
        {
            Customer c = _customerService.GetById(id);
            if (c!=null)
            {
                NewShoppingBagVM newShoppingBagVM = new NewShoppingBagVM() {
                    Customer = c,
                    Date = DateTime.Now
                };
                return View(newShoppingBagVM);
            }
            else
            {
                return RedirectToAction("Index","Main");
            }

        }
        [Authorize]
        [HttpPost]
        public IActionResult NewShoppingBag(NewShoppingBagVM newShoppingBagVM)
        {

            ShoppingBag sb = new ShoppingBag()
            {
                Customer = _customerService.GetById(newShoppingBagVM.Customer.Id),
                Date = newShoppingBagVM.Date
            };
            _ShoppingBagService.NewShoppingBag(sb);


            return RedirectToAction("DetailBag", new { id = sb.Id });
        }
        [Authorize]
        [HttpPost]
        public IActionResult NewShoppingBagItem(DetailBagVM detailBagVM)
        {

            _ShoppingBagService.AddShoppingItem(detailBagVM.ShoppingBag.Id, detailBagVM.AddProductId, detailBagVM.AddProductQty);
            return RedirectToAction("DetailBag", new { id = detailBagVM.ShoppingBag.Id });

        }

        //public IActionResult DeleteDouble()
        //{
        //    shoppingBagData.RemoveShoppingItemDoubles();
        //    return RedirectToAction("Index", "Main");
        //}
    }
}