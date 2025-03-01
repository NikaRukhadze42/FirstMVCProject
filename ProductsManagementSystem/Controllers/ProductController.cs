using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.Models.Entities;

namespace ProductsManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return View(product);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            var productFromDb = await _context.Products.FirstOrDefaultAsync(p=> p.Id == product.Id);
            if(productFromDb != null)
            {
                productFromDb.Name = product.Name;
                productFromDb.Description = product.Description;
                productFromDb.Price = product.Price;
            }

            _context.Products.Update(productFromDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Add()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}