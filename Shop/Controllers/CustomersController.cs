using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Buisiness;
using Buisiness.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Shop
{
    public class CustomersController : Controller
    {

        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [Authorize(Roles = "Admin")]
        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _customerService.GetAllAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [Authorize(Roles = "Admin")]
        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FirstName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);  
        }

        [Authorize(Roles = "Admin")]
        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = new Customer();
            customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(String id, [Bind("Id,Name,FirstName,Email")] Customer customer)
        {
            if (!id.Equals(customer.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                    Customer NewCustomer = await _customerService.GetByIdAsync(customer.Id);
                    NewCustomer.Email = customer.Email;
                    NewCustomer.UserName = customer.Email;
                    NewCustomer.FirstName = customer.FirstName;
                    NewCustomer.Name = customer.Name;
                    await _customerService.updateUserAsync(NewCustomer);


                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                await _customerService.DeleteUserAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {


                return RedirectToAction("ErrorMessage", "Main", new { message=ex.InnerException.Message, returnController = "Customers", returnAction="Index"});

            }

        }

        private bool CustomerExists(string id)
        {
            bool customerExists = _customerService.CustomerExist(id);
            return customerExists;
        }
    }
}
