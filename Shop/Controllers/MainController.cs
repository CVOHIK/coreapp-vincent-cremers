
using System.Collections.Generic;
using System.Threading.Tasks;
using Buisiness;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Buisiness.Interfaces;
using Buisiness.Enum;

namespace Shop.Controllers
{

    public class MainController : Controller
    {
        ICustomerService _customerService;
        private readonly SignInManager<Customer> _signInManager;

        public MainController(ICustomerService customerService, SignInManager<Customer> signInManager)
        {
            _customerService = customerService;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> Index()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            if (_signInManager.IsSignedIn(currentUser))
            {
                if (currentUser.IsInRole(Roles.Admin.ToString()))
                {
                    ICollection<Customer> Customers = await _customerService.GetAllAsync() ;
                    return View(Customers);
                }
                else
                {
                    string username = currentUser.Identity.Name;
                    Customer customer = _customerService.GetByUsername(username);

                    return RedirectToAction("DetailCustomer", "Shop", new {id = customer.Id });
                }
                
            }
            else
            {
                return RedirectToAction("LoginFirst");
            }
        }


       public IActionResult LoginFirst()
        {
            return View();
        }

        public IActionResult ErrorMessage(string message, string returnController, string returnAction)
        {
            ViewData["message"] = message;
            ViewData["controller"] = returnController;
            ViewData["action"] = returnAction;
            return View();
        }

    }
}