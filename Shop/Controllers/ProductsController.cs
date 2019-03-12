using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Buisiness;
using Microsoft.AspNetCore.Authorization;
using Buisiness.Interfaces;

namespace Shop
{
    public class ProductsController : Controller
    {
        //private readonly ShopContext _context;
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ICollection<Product> products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [Authorize]
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }

        [Authorize(Roles ="Admin")]
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _productService.UpdateAsync(product);

                }
                catch (Exception ex)
                {
                    return RedirectToAction("ErrorMessage", "Main", new { message = ex.InnerException.Message, returnController = "Customers", returnAction = "Index" });

                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);

            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorMessage", "Main", new { message = ex.InnerException.Message, returnController = "Customers", returnAction = "Index" });

            }
   
        }

        public IActionResult NoDeletePosible()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                await _productService.RemoveAsync(product);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorMessage", "Main", new { message = ex.InnerException.Message, returnController = "Products", returnAction = "Index" });

            }


            return RedirectToAction(nameof(Index));
        }

    }
}
